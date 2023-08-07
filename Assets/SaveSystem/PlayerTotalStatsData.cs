using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerTotalStatsData
{
    public double totalPlayTime;
    public int totalRuns, completedRuns, bestRunStreak, deaths, goldCollected, soulsCollected, runesUsed, scrollsAbsorbed, itemsBought,
        potionsDrank, chestsOpened, roomsExplored, puzzlesCompleted, enemiesKilled, bossesKilled, reapersKilled, jarsBroken, rocksBroken, 
        batsKilled, beesKilled, bunniesKilled, goblinsKilled, mushroomsKilled, plantsKilled, wolvesKilled, golemsKilled, treantGuardsKilled, 
        dragonsKilled, babyReapersKilled, princeReapersKilled, godReapersKilled, magicSealsBroken;

    public PlayerTotalStatsData(LocalGameManager gameManager)
    {
        PlayerTotalStats stats = gameManager.GetTotalStats();
        totalPlayTime = stats.totalPlayTime;
        totalRuns = stats.totalRuns;
        completedRuns = stats.completedRuns;
        bestRunStreak = stats.bestRunStreak;
        deaths = stats.deaths;
        goldCollected = stats.goldCollected;
        soulsCollected = stats.soulsCollected;
        runesUsed = stats.runesUsed;
        scrollsAbsorbed = stats.scrollsAbsorbed;
        itemsBought = stats.itemsBought;
        potionsDrank = stats.potionsDrank;
        chestsOpened = stats.chestsOpened;
        roomsExplored = stats.roomsExplored;
        puzzlesCompleted = stats.puzzlesCompleted;
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
        magicSealsBroken = stats.magicSealsBroken;
    }
}
