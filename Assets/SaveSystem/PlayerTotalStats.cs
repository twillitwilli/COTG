using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class PlayerTotalStats : MonoSingleton<PlayerTotalStats>
{
    public enum StatType 
    { 
        totalRuns, 
        completedRuns, 
        bestStreak, 
        deaths, 
        totalGold, 
        totalSouls, 
        runesUsed, 
        scrollsAbsorbed, 
        itemsBought, 
        potionsDrank, 
        chestsOpened, 
        roomsExplored, 
        puzzlesCompleted, 
        enemiesKilled, 
        bossesKilled, 
        reapersKilled, 
        jarsBroken, 
        rocksBroken, 
        batsKilled, 
        beesKilled, 
        bunniesKilled, 
        goblinsKilled, 
        mushroomsKilled, 
        plantsKilled, 
        wolvesKilled, 
        golemsKilled, 
        treantsKilled, 
        dragonsKilled, 
        babyReaperKills, 
        princeReapersKilled, 
        godReapersKilled, 
        magicSealsBroken 
    }

    // Player Stats
    public float totalPlayTime { get; private set; }
    public int totalRuns { get; private set; }
    public int completedRuns { get; private set; }
    public int bestRunStreak { get; private set; }
    public int deaths { get; private set; }
    public int goldCollected { get; private set; }
    public int soulsCollected { get; private set; }
    public int runesUsed { get; private set; }
    public int scrollsAbsorbed { get; private set; }
    public int itemsBought { get; private set; }
    public int potionsDrank { get; private set; }
    public int chestsOpened { get; private set; }
    public int roomsExplored { get; private set; }
    public int puzzlesCompleted { get; private set; }
    public int jarsBroken { get; private set; }
    public int rocksBroken { get; private set; }
    public int magicSealsBroken { get; private set; }


    // Total Enemy Kills
    public int enemiesKilled { get; private set; }
    public int bossesKilled { get; private set; }
    public int reapersKilled { get; private set; }


    // Enemies Killed
    public int batsKilled { get; private set; }
    public int beesKilled { get; private set; }
    public int bunniesKilled { get; private set; }
    public int goblinsKilled { get; private set; }
    public int mushroomsKilled { get; private set; }
    public int plantsKilled { get; private set; }
    public int wolvesKilled { get; private set; }


    // Boss / Reapers Killed
    public int golemsKilled { get; private set; }
    public int treantGuardsKilled { get; private set; }
    public int dragonsKilled { get; private set; }
    public int babyReapersKilled { get; private set; }
    public int princeReapersKilled { get; private set; }
    public int godReapersKilled { get; private set; }

    public void AdjustStats(StatType statType, int value = 0)
    {
        if (LocalGameManager.Instance.currentGameMode != LocalGameManager.GameMode.tutorial || LocalGameManager.Instance.currentGameMode != LocalGameManager.GameMode.inLobby)
        {
            Debug.Log("Adjusting Progress Stat");

            switch (statType)
            {
                case StatType.totalRuns:
                    totalRuns++;
                    break;

                case StatType.completedRuns: //not implemented
                    completedRuns++;
                    break;

                case StatType.bestStreak: //not implemented
                    bestRunStreak++;
                    break;

                case StatType.deaths:
                    deaths++;
                    break;

                case StatType.totalGold:
                    goldCollected += value;
                    break;

                case StatType.totalSouls:
                    soulsCollected++;
                    break;

                case StatType.runesUsed:
                    runesUsed++;
                    break;

                case StatType.scrollsAbsorbed:
                    scrollsAbsorbed++;
                    break;

                case StatType.itemsBought:
                    itemsBought++;
                    break;

                case StatType.potionsDrank:
                    potionsDrank++;
                    break;

                case StatType.chestsOpened:
                    chestsOpened++;
                    break;

                case StatType.roomsExplored:
                    roomsExplored++;
                    break;

                case StatType.puzzlesCompleted:
                    puzzlesCompleted++;
                    break;

                case StatType.enemiesKilled:
                    enemiesKilled++;
                    break;

                case StatType.bossesKilled:
                    bossesKilled++;
                    break;

                case StatType.reapersKilled:
                    reapersKilled++;
                    break;

                case StatType.jarsBroken:
                    jarsBroken++;
                    break;

                case StatType.rocksBroken:
                    rocksBroken++;
                    break;

                case StatType.batsKilled:
                    batsKilled++;
                    break;

                case StatType.beesKilled:
                    beesKilled++;
                    break;

                case StatType.bunniesKilled:
                    bunniesKilled++;
                    break;

                case StatType.goblinsKilled:
                    goblinsKilled++;
                    break;

                case StatType.mushroomsKilled:
                    mushroomsKilled++;
                    break;

                case StatType.plantsKilled:
                    plantsKilled++;
                    break;

                case StatType.wolvesKilled:
                    wolvesKilled++;
                    break;

                case StatType.golemsKilled:
                    golemsKilled++;
                    break;

                case StatType.treantsKilled:
                    treantGuardsKilled++;
                    break;

                case StatType.dragonsKilled:
                    dragonsKilled++;
                    break;

                case StatType.babyReaperKills:
                    babyReapersKilled++;
                    break;

                case StatType.princeReapersKilled:
                    princeReapersKilled++;
                    break;

                case StatType.godReapersKilled:
                    godReapersKilled++;
                    break;

                case StatType.magicSealsBroken:
                    magicSealsBroken++;
                    break;
            }
        }
    }

    public void SavePlayerProgress(int saveFileIndex)
    {
        ChatManager.Instance.DebugMessage("Saving File: " + saveFileIndex);

        BinarySaveSystem.SavePlayerProgressStats(CreateNewSaveData(), saveFileIndex);
    }

    private PlayerProgressSaveData CreateNewSaveData()
    {
        PlayerProgressSaveData newData = new PlayerProgressSaveData();

        // Player Totals
        newData.totalPlayTime = totalPlayTime;
        newData.totalRuns = totalRuns;
        newData.completedRuns = completedRuns;
        newData.bestRunStreak = bestRunStreak;
        newData.deaths = deaths;
        newData.goldCollected = goldCollected;
        newData.soulsCollected = soulsCollected;
        newData.runesUsed = runesUsed;
        newData.scrollsAbsorbed = scrollsAbsorbed;
        newData.itemsBought = itemsBought;
        newData.potionsDrank = potionsDrank;
        newData.chestsOpened = chestsOpened;
        newData.roomsExplored = roomsExplored;
        newData.puzzlesCompleted = puzzlesCompleted;
        newData.jarsBroken = jarsBroken;
        newData.rocksBroken = rocksBroken;

        // Total Enemy Kills
        newData.enemiesKilled = enemiesKilled;
        newData.bossesKilled = bossesKilled;
        newData.reapersKilled = reapersKilled;

        // Enemies Killed
        newData.batsKilled = batsKilled;
        newData.beesKilled = beesKilled;
        newData.bunniesKilled = bunniesKilled;
        newData.goblinsKilled = goblinsKilled;
        newData.mushroomsKilled = mushroomsKilled;
        newData.plantsKilled = plantsKilled;
        newData.wolvesKilled = wolvesKilled;

        // Bosses / Elites Killed
        newData.golemsKilled = golemsKilled;
        newData.treantGuardsKilled = treantGuardsKilled;
        newData.dragonsKilled = dragonsKilled;
        newData.babyReapersKilled = babyReapersKilled;
        newData.princeReapersKilled = princeReapersKilled;
        newData.godReapersKilled = godReapersKilled;

        return newData;
    }

    public void LoadPlayerProgress(int saveFileIndex)
    {
        ChatManager.Instance.DebugMessage("Save File " + saveFileIndex + " Loading");

        PlayerProgressSaveData loadedData = BinarySaveSystem.LoadPlayerProgressStats(saveFileIndex);

        // Player Totals
        totalPlayTime = loadedData.totalPlayTime; //not implemented
        totalRuns = loadedData.totalRuns; //not implemented
        completedRuns = loadedData.completedRuns; //not implemented
        bestRunStreak = loadedData.bestRunStreak; //not implemented
        deaths = loadedData.deaths;
        goldCollected = loadedData.goldCollected;
        soulsCollected = loadedData.soulsCollected;
        runesUsed = loadedData.runesUsed;
        scrollsAbsorbed = loadedData.scrollsAbsorbed;
        itemsBought = loadedData.itemsBought;
        potionsDrank = loadedData.potionsDrank;
        chestsOpened = loadedData.chestsOpened;
        roomsExplored = loadedData.roomsExplored;
        puzzlesCompleted = loadedData.puzzlesCompleted; //not implemented
        jarsBroken = loadedData.jarsBroken;
        rocksBroken = loadedData.rocksBroken;

        // Total Enemy Kills
        enemiesKilled = loadedData.enemiesKilled;
        bossesKilled = loadedData.bossesKilled;
        reapersKilled = loadedData.reapersKilled;
        
        // Enemies Killed
        batsKilled = loadedData.batsKilled;
        beesKilled = loadedData.beesKilled;
        bunniesKilled = loadedData.bunniesKilled;
        goblinsKilled = loadedData.goblinsKilled;
        mushroomsKilled = loadedData.mushroomsKilled;
        plantsKilled = loadedData.plantsKilled;
        wolvesKilled = loadedData.wolvesKilled;

        // Bosses / Elites Killed
        golemsKilled = loadedData.golemsKilled;
        treantGuardsKilled = loadedData.treantGuardsKilled;
        dragonsKilled = loadedData.dragonsKilled;
        babyReapersKilled = loadedData.babyReapersKilled;
        princeReapersKilled = loadedData.princeReapersKilled;
        godReapersKilled = loadedData.godReapersKilled;

        CheckProgressUnlocks();

        ChatManager.Instance.DebugMessage("Player Progress Stats Loaded");
    }

    public void CheckProgressUnlocks()
    {
        PlayerProgressStats.Instance.CheckUnlocks(this);
    }
}
