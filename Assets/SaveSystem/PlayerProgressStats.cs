using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class PlayerProgressStats : MonoSingleton<PlayerProgressStats>
{
    public float totalPlayTimeLevel;

    public int totalRunsLevel, completedRunsLevel, bestRunStreakLevel, deathLevel, goldLevel, soulLevel, runeLevel, scrollLevel, itemLevel,
        potionLevel, chestLevel, roomLevel, puzzleLevel, enemyLevel, bossLevel, reaperLevel, jarLevel, rockLevel,
        batLevel, beeLevel, bunnyLevel, goblinLevel, mushroomLevel, plantLevel, wolfLevel, golemLevel, treantGuardLevel,
        dragonLevel, babyReaperLevel, princeReaperLevel, godReaperLevel;

    public void SaveCurrentStatsAndReset()
    {
        
    }

    public void CheckUnlocks(PlayerTotalStats totalStats)
    {
        TotalPlayTime(totalStats.totalPlayTime);
        TotalRuns(totalStats.totalRuns);
        CompletedRuns(totalStats.completedRuns);
        RunStreak(totalStats.bestRunStreak);
        Deaths(totalStats.deaths);
        GoldCollected(totalStats.goldCollected);
        SoulsCollected(totalStats.soulsCollected);
        RunesUsed(totalStats.runesUsed);
        ScrollsAbsorbed(totalStats.scrollsAbsorbed);
        ItemsBought(totalStats.itemsBought);
        PotionsDrank(totalStats.potionsDrank);
        ChestsOpened(totalStats.chestsOpened);
        RoomsExplored(totalStats.roomsExplored);
        PuzzlesCompleted(totalStats.puzzlesCompleted);
        EnemiesKilled(totalStats.enemiesKilled);
        BossesKilled(totalStats.bossesKilled);
        ReapersKilled(totalStats.reapersKilled);
        JarsBroken(totalStats.jarsBroken);
        RocksBroken(totalStats.rocksBroken);
        BatsKilled(totalStats.batsKilled);
        BeesKilled(totalStats.beesKilled);
        BunniesKilled(totalStats.bunniesKilled);
        GoblinsKilled(totalStats.goblinsKilled);
        MushroomsKilled(totalStats.mushroomsKilled);
        PlantsKilled(totalStats.plantsKilled);
        WolvesKilled(totalStats.wolvesKilled);
        GolemsKilled(totalStats.golemsKilled);
        TreantsKilled(totalStats.treantGuardsKilled);
        DragonsKilled(totalStats.dragonsKilled);
        BabyReapersKilled(totalStats.babyReapersKilled);
        PrinceReapersKilled(totalStats.princeReapersKilled);
        GodReapersKilled(totalStats.godReapersKilled);
    }

    private void TotalPlayTime(double playTime)
    {

    }

    private void TotalRuns(int totalRuns)
    {

    }

    private void CompletedRuns(int completedRuns)
    {

    }

    private void RunStreak(int bestStreak)
    {

    }

    private void Deaths(int deaths)
    {

    }

    private void GoldCollected(int goldCollected)
    {

    }

    private void SoulsCollected(int soulsCollected)
    {

    }

    private void RunesUsed(int runesUsed)
    {

    }

    private void ScrollsAbsorbed(int scrollsAbsorbed)
    {

    }

    private void ItemsBought(int itemsBought)
    {

    }

    private void PotionsDrank(int potionsDrank)
    {

    }

    private void ChestsOpened(int chestsOpened)
    {

    }

    private void RoomsExplored(int roomsExplored)
    {

    }

    private void PuzzlesCompleted(int puzzlesCompleted)
    {

    }

    private void EnemiesKilled(int enemiesKilled)
    {

    }

    private void BossesKilled(int bossesKilled)
    {

    }

    private void ReapersKilled(int reapersKilled)
    {

    }

    private void JarsBroken(int jarsBroken)
    {

    }

    private void RocksBroken(int rocksBroken)
    {

    }

    private void BatsKilled(int batsKilled)
    {
        batLevel = SpecificEnemyLevelCheck(batsKilled);
    }

    private void BeesKilled(int beesKilled)
    {
        beeLevel = SpecificEnemyLevelCheck(beesKilled);
    }

    private void BunniesKilled(int bunniesKilled)
    {
        bunnyLevel = SpecificEnemyLevelCheck(bunniesKilled);
    }

    private void GoblinsKilled(int goblinsKilled)
    {
        goblinLevel = SpecificEnemyLevelCheck(goblinsKilled);
    }

    private void MushroomsKilled(int mushroomsKilled)
    {
        mushroomLevel = SpecificEnemyLevelCheck(mushroomsKilled);
    }

    private void PlantsKilled(int plantsKilled)
    {
        plantLevel = SpecificEnemyLevelCheck(plantsKilled);
    }

    private void WolvesKilled(int wolvesKilled)
    {
        wolfLevel = SpecificEnemyLevelCheck(wolvesKilled);
    }

    private void GolemsKilled(int golemsKilled)
    {
        golemLevel = BossLevelCheck(golemsKilled);
    }

    private void TreantsKilled(int treantsKilled)
    {
        treantGuardLevel = BossLevelCheck(treantsKilled);
    }

    private void DragonsKilled(int dragonsKilled)
    {
        dragonLevel = BossLevelCheck(dragonsKilled);
    }

    private void BabyReapersKilled(int babyReapersKilled)
    {

    }

    private void PrinceReapersKilled(int princeReapersKilled)
    {

    }   
    
    private void GodReapersKilled(int godReapersKilled)
    {

    }

    private int SpecificEnemyLevelCheck(int enemiesKilled)
    {
        if (enemiesKilled >= 25 && enemiesKilled < 50) { return 1; }
        else if (enemiesKilled >= 50 && enemiesKilled < 100) { return 2; }
        else if (enemiesKilled >= 100 && enemiesKilled < 250) { return 3; }
        else if (enemiesKilled >= 250) { return 4; }
        return 0;
    }

    private int BossLevelCheck(int bossesKilled)
    {
        if (bossesKilled >= 5 && bossesKilled < 15) { return 1; }
        else if (bossesKilled >= 15 && bossesKilled < 30) { return 2; }
        else if (bossesKilled >= 30 && bossesKilled < 100) { return 3; }
        else if (bossesKilled >= 100) { return 4; }
        return 0;
    }
}
