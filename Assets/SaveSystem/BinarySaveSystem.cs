using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySaveSystem
{
    public static void SavePlayer(PlayerStats playerStats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + playerStats.GetSaveFileIndex() + "player.savedDungeon";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        PlayerSavedStats stats = new PlayerSavedStats(playerStats);

        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public static PlayerSavedStats LoadPlayerStats(int playerFile)
    {
        string path = Application.persistentDataPath + "/" + playerFile + "player.savedDungeon";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSavedStats stats = formatter.Deserialize(stream) as PlayerSavedStats;
            stream.Close();

            return stats;
        }
        else return null; //file not found
    }

    public static void SaveTotalStats(LocalGameManager gameManager)
    {
        Debug.Log("Saving Binary Total Stats");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + gameManager.GetPlayerStats().GetSaveFileIndex() + "player.totalStats";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        PlayerTotalStatsData stats = new PlayerTotalStatsData(gameManager);

        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public static PlayerTotalStatsData LoadTotalStats(int playerFile)
    {
        Debug.Log("Loading Binary Total Stats");
        string path = Application.persistentDataPath + "/" + playerFile + "player.totalStats";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerTotalStatsData stats = formatter.Deserialize(stream) as PlayerTotalStatsData;
            stream.Close();

            return stats;
        }
        else return null; //file not found
    }
}
