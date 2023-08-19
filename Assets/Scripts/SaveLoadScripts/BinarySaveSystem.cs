using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySaveSystem
{
    public static void SavePlayerProgressStats(PlayerProgressSaveData saveData, int fileIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string fileName = Application.persistentDataPath + "/" + "player.ProgressStats" + fileIndex;
        FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
        formatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public static PlayerProgressSaveData LoadPlayerProgressStats(int fileIndex)
    {
        string fileName = Application.persistentDataPath + "/" + "player.ProgressStats" + fileIndex;
        if (File.Exists(fileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            PlayerProgressSaveData loadedData = formatter.Deserialize(fileStream) as PlayerProgressSaveData;
            fileStream.Close();
            return loadedData;
        }
        else return null; //file not found
    }

    public static void SaveDungeon(PlayerDungeonData dungeonSaveData, int fileIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string fileName = Application.persistentDataPath + "/" + "player.DungeonData" + fileIndex;
        FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
        formatter.Serialize(fileStream, dungeonSaveData);
        fileStream.Close();
    }

    public static PlayerDungeonData LoadDungeon(int fileIndex)
    {
        string fileName = Application.persistentDataPath + "/" + "player.DungeonData" + fileIndex;
        if (File.Exists(fileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            PlayerDungeonData loadedData = formatter.Deserialize(fileStream) as PlayerDungeonData;
            fileStream.Close();
            return loadedData;
        }
        else return null; //file not found
    }

    public static void DeleteFile(string fileName, int fileIndex)
    {
        string file = Application.persistentDataPath + "/player." + fileName + fileIndex;
        if (File.Exists(file)) { File.Delete(file); }
    }
}
