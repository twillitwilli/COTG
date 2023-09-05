using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalStatsDisplay : MonoBehaviour
{
    [SerializeField] private Text textBox;

    public enum TotalStats 
    { 
        completedRuns, 
        bestRunStreak, 
        deaths, 
        goldCollected, 
        soulsCollected, 
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
        totalPlayTime, 
        batsKilled, 
        beesKilled, 
        bunniesKilled, 
        goblinsKilled, 
        mushroomsKilled, 
        plantsKilled, 
        wolvesKilled, 
        golemsKilled, 
        treantGuardsKilled, 
        dragonsKilled, 
        babyReapersKilled, 
        princeReapersKilled, 
        godReapersKilled 
    }

    public TotalStats statToDisplay;

    public void Start()
    {
        PlayerTotalStats totalStats = PlayerTotalStats.Instance;

        switch (statToDisplay)
        {
            case TotalStats.completedRuns:
                textBox.text = "Completed Runs: " + totalStats.completedRuns;
                break;

            case TotalStats.bestRunStreak:
                textBox.text = "Best Run Streak: " + totalStats.bestRunStreak;
                break;

            case TotalStats.deaths:
                textBox.text = "Total Deaths: " + totalStats.deaths;
                break;

            case TotalStats.goldCollected:
                textBox.text = "Gold Collected: " + totalStats.goldCollected;
                break;

            case TotalStats.soulsCollected:
                textBox.text = "Souls Collected: " + totalStats.soulsCollected;
                break;

            case TotalStats.runesUsed:
                textBox.text = "Runes Used: " + totalStats.runesUsed;
                break;

            case TotalStats.scrollsAbsorbed:
                textBox.text = "Scrolls Absorbed: " + totalStats.scrollsAbsorbed;
                break;

            case TotalStats.itemsBought:
                textBox.text = "Items Bought: " + totalStats.itemsBought;
                break;

            case TotalStats.potionsDrank:
                textBox.text = "Potions Used: " + totalStats.potionsDrank;
                break;

            case TotalStats.chestsOpened:
                textBox.text = "Chests Opened: " + totalStats.chestsOpened;
                break;

            case TotalStats.roomsExplored:
                textBox.text = "Rooms Explored: " + totalStats.roomsExplored;
                break;

            case TotalStats.puzzlesCompleted:
                textBox.text = "Puzzles Completed: " + totalStats.puzzlesCompleted;
                break;

            case TotalStats.enemiesKilled:
                textBox.text = "Enemies Killed: " + totalStats.enemiesKilled;
                break;

            case TotalStats.bossesKilled:
                textBox.text = "Bosses Killed: " + totalStats.bossesKilled;
                break;

            case TotalStats.reapersKilled:
                textBox.text = "Reapers Killed: " + totalStats.reapersKilled;
                break;

            case TotalStats.jarsBroken:
                textBox.text = "Jars Broken: " + totalStats.jarsBroken;
                break;

            case TotalStats.rocksBroken:
                textBox.text = "Rocks Broken: " + totalStats.rocksBroken;
                break;

            case TotalStats.totalPlayTime:
                SetTotalPlayTime(totalStats.totalPlayTime);
                break;

            case TotalStats.batsKilled:
                textBox.text = "Bats Killed: " + totalStats.batsKilled;
                break;

            case TotalStats.beesKilled:
                textBox.text = "Bees Killed: " + totalStats.beesKilled;
                break;

            case TotalStats.bunniesKilled:
                textBox.text = "Bunnies Killed: " + totalStats.bunniesKilled;
                break;

            case TotalStats.goblinsKilled:
                textBox.text = "Goblins Killed: " + totalStats.goblinsKilled;
                break;

            case TotalStats.mushroomsKilled:
                textBox.text = "Mushrooms Killed: " + totalStats.mushroomsKilled;
                break;

            case TotalStats.plantsKilled:
                textBox.text = "Plants Killed: " + totalStats.plantsKilled;
                break;

            case TotalStats.wolvesKilled:
                textBox.text = "Wolves Killed: " + totalStats.wolvesKilled;
                break;

            case TotalStats.golemsKilled:
                textBox.text = "Golems Killed: " + totalStats.golemsKilled;
                break;

            case TotalStats.treantGuardsKilled:
                textBox.text = "Treant Guards Killed: " + totalStats.treantGuardsKilled;
                break;

            case TotalStats.dragonsKilled:
                textBox.text = "Dragons Killed: " + totalStats.dragonsKilled;
                break;

            case TotalStats.babyReapersKilled:
                textBox.text = "Baby Reapers Killed: " + totalStats.babyReapersKilled;
                break;

            case TotalStats.princeReapersKilled:
                textBox.text = "Prince Reapers Killed: " + totalStats.princeReapersKilled;
                break;

            case TotalStats.godReapersKilled:
                textBox.text = "God Reapers Killed: " + totalStats.godReapersKilled;
                break;
        }
    }

    private void SetTotalPlayTime(double totalTime)
    {
        textBox.text = "Play Time: " + totalTime;
    }
}
