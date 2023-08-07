using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTotalStats : MonoBehaviour
{
    private LocalGameManager _gameManager;

    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private ChatManager _chatManager;

    public enum StatType { totalRuns, completedRuns, bestStreak, deaths, totalGold, totalSouls, runesUsed, scrollsAbsorbed, itemsBought, 
        potionsDrank, chestsOpened, roomsExplored, puzzlesCompleted, enemiesKilled, bossesKilled, reapersKilled, jarsBroken, rocksBroken, batsKilled, 
        beesKilled, bunniesKilled, goblinsKilled, mushroomsKilled, plantsKilled, wolvesKilled, golemsKilled, treantsKilled, dragonsKilled, 
        babyReaperKills, princeReapersKilled, godReapersKilled, magicSealsBroken }
    public PlayerProgressStats progressStats;
    [HideInInspector] public double totalPlayTime;
    [HideInInspector] public int totalRuns, completedRuns, bestRunStreak, deaths, goldCollected, soulsCollected, runesUsed, scrollsAbsorbed, itemsBought, 
        potionsDrank, chestsOpened, roomsExplored, puzzlesCompleted, enemiesKilled, bossesKilled, reapersKilled, jarsBroken, rocksBroken, 
        magicSealsBroken;
    [HideInInspector] public int batLevel, beeLevel, bunnyLevel, goblinLevel, mushroomLevel, plantLevel, wolfLevel;
    [HideInInspector] public int batsKilled, beesKilled, bunniesKilled, goblinsKilled, mushroomsKilled, plantsKilled, wolvesKilled;
    [HideInInspector] public int golemsKilled, treantGuardsKilled, dragonsKilled, babyReapersKilled, princeReapersKilled, godReapersKilled;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
    }

    public void AdjustStat(StatType statType, int value)
    {
        if (!_gameManager.inTutorial)
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

    public void Save()
    {
        Debug.Log("Game Saving...");
        totalPlayTime = _gameTimer.currentTime;
        BinarySaveSystem.SaveTotalStats(_gameManager);
    }

    public void Load(int saveFile)
    {
        _chatManager.DebugMessage("Save File " + saveFile + " Loading");

        PlayerTotalStatsData stats = BinarySaveSystem.LoadTotalStats(saveFile);
        totalPlayTime = stats.totalPlayTime; //not implemented
        totalRuns = stats.totalRuns; //not implemented
        completedRuns = stats.completedRuns; //not implemented
        bestRunStreak = stats.bestRunStreak; //not implemented
        deaths = stats.deaths;
        goldCollected = stats.goldCollected;
        soulsCollected = stats.soulsCollected;
        runesUsed = stats.runesUsed;
        scrollsAbsorbed = stats.scrollsAbsorbed;
        itemsBought = stats.itemsBought;
        potionsDrank = stats.potionsDrank;
        chestsOpened = stats.chestsOpened;
        roomsExplored = stats.roomsExplored;
        puzzlesCompleted = stats.puzzlesCompleted; //not implemented
        enemiesKilled = stats.enemiesKilled;
        bossesKilled = stats.bossesKilled;
        reapersKilled = stats.reapersKilled;
        jarsBroken = stats.jarsBroken;
        rocksBroken = stats.rocksBroken;
        batsKilled = stats.batsKilled;
        beesKilled = stats.beesKilled;
        bunniesKilled = stats.bunniesKilled;
        goblinsKilled = stats.goblinsKilled;
        mushroomsKilled = stats.mushroomsKilled;
        plantsKilled = stats.plantsKilled;
        wolvesKilled = stats.wolvesKilled;
        golemsKilled = stats.golemsKilled;
        treantGuardsKilled = stats.treantGuardsKilled;
        dragonsKilled = stats.dragonsKilled;
        babyReapersKilled = stats.babyReapersKilled;
        princeReapersKilled = stats.princeReapersKilled;
        godReapersKilled = stats.godReapersKilled;

        CheckProgressUnlocks();
        _chatManager.DebugMessage("Game Loaded");
    }

    public void CheckProgressUnlocks()
    {
        progressStats.CheckUnlocks(this);
    }
}
