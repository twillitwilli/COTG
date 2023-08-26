[System.Serializable]
public class PlayerProgressSaveData
{
    public float totalPlayTime;

    // Total Stats
    public int totalRuns, completedRuns, bestRunStreak, deaths, goldCollected, soulsCollected, runesUsed, scrollsAbsorbed, itemsBought,
        potionsDrank, chestsOpened, roomsExplored, puzzlesCompleted, jarsBroken, rocksBroken,
        magicSealsBroken;

    // Total Enemy Kills
    public int enemiesKilled, bossesKilled, reapersKilled;

    // Enemies Killed
    public int batsKilled, beesKilled, bunniesKilled, goblinsKilled, mushroomsKilled, plantsKilled, wolvesKilled;

    // Bosses / Reapers Killed
    public int golemsKilled, treantGuardsKilled, dragonsKilled, babyReapersKilled, princeReapersKilled, godReapersKilled;
}
