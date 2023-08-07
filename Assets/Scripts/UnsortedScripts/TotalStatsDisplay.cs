using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalStatsDisplay : MonoBehaviour
{
    [SerializeField] private Text textBox;

    public enum TotalStats { completedRuns, bestRunStreak, deaths, goldCollected, soulsCollected, runesUsed, scrollsAbsorbed, itemsBought,
        potionsDrank, chestsOpened, roomsExplored, puzzlesCompleted, enemiesKilled, bossesKilled, reapersKilled, jarsBroken, rocksBroken, 
        totalPlayTime, batsKilled, beesKilled, bunniesKilled, goblinsKilled, mushroomsKilled, plantsKilled, wolvesKilled, golemsKilled, 
        treantGuardsKilled, dragonsKilled, babyReapersKilled, princeReapersKilled, godReapersKilled }

    public TotalStats statToDisplay;

    public void Start()
    {
        PlayerTotalStats playerStats = LocalGameManager.instance.GetTotalStats();

        switch (statToDisplay)
        {
            case TotalStats.completedRuns:
                textBox.text = "Completed Runs: " + playerStats.completedRuns;
                break;

            case TotalStats.bestRunStreak:
                textBox.text = "Best Run Streak: " + playerStats.bestRunStreak;
                break;

            case TotalStats.deaths:
                textBox.text = "Total Deaths: " + playerStats.deaths;
                break;

            case TotalStats.goldCollected:
                textBox.text = "Gold Collected: " + playerStats.goldCollected;
                break;

            case TotalStats.soulsCollected:
                textBox.text = "Souls Collected: " + playerStats.soulsCollected;
                break;

            case TotalStats.runesUsed:
                textBox.text = "Runes Used: " + playerStats.runesUsed;
                break;

            case TotalStats.scrollsAbsorbed:
                textBox.text = "Scrolls Absorbed: " + playerStats.scrollsAbsorbed;
                break;

            case TotalStats.itemsBought:
                textBox.text = "Items Bought: " + playerStats.itemsBought;
                break;

            case TotalStats.potionsDrank:
                textBox.text = "Potions Used: " + playerStats.potionsDrank;
                break;

            case TotalStats.chestsOpened:
                textBox.text = "Chests Opened: " + playerStats.chestsOpened;
                break;

            case TotalStats.roomsExplored:
                textBox.text = "Rooms Explored: " + playerStats.roomsExplored;
                break;

            case TotalStats.puzzlesCompleted:
                textBox.text = "Puzzles Completed: " + playerStats.puzzlesCompleted;
                break;

            case TotalStats.enemiesKilled:
                textBox.text = "Enemies Killed: " + playerStats.enemiesKilled;
                break;

            case TotalStats.bossesKilled:
                textBox.text = "Bosses Killed: " + playerStats.bossesKilled;
                break;

            case TotalStats.reapersKilled:
                textBox.text = "Reapers Killed: " + playerStats.reapersKilled;
                break;

            case TotalStats.jarsBroken:
                textBox.text = "Jars Broken: " + playerStats.jarsBroken;
                break;

            case TotalStats.rocksBroken:
                textBox.text = "Rocks Broken: " + playerStats.rocksBroken;
                break;

            case TotalStats.totalPlayTime:
                SetTotalPlayTime(playerStats.totalPlayTime);
                break;

            case TotalStats.batsKilled:
                textBox.text = "Bats Killed: " + playerStats.batsKilled;
                break;

            case TotalStats.beesKilled:
                textBox.text = "Bees Killed: " + playerStats.beesKilled;
                break;

            case TotalStats.bunniesKilled:
                textBox.text = "Bunnies Killed: " + playerStats.bunniesKilled;
                break;

            case TotalStats.goblinsKilled:
                textBox.text = "Goblins Killed: " + playerStats.goblinsKilled;
                break;

            case TotalStats.mushroomsKilled:
                textBox.text = "Mushrooms Killed: " + playerStats.mushroomsKilled;
                break;

            case TotalStats.plantsKilled:
                textBox.text = "Plants Killed: " + playerStats.plantsKilled;
                break;

            case TotalStats.wolvesKilled:
                textBox.text = "Wolves Killed: " + playerStats.wolvesKilled;
                break;

            case TotalStats.golemsKilled:
                textBox.text = "Golems Killed: " + playerStats.golemsKilled;
                break;

            case TotalStats.treantGuardsKilled:
                textBox.text = "Treant Guards Killed: " + playerStats.treantGuardsKilled;
                break;

            case TotalStats.dragonsKilled:
                textBox.text = "Dragons Killed: " + playerStats.dragonsKilled;
                break;

            case TotalStats.babyReapersKilled:
                textBox.text = "Baby Reapers Killed: " + playerStats.babyReapersKilled;
                break;

            case TotalStats.princeReapersKilled:
                textBox.text = "Prince Reapers Killed: " + playerStats.princeReapersKilled;
                break;

            case TotalStats.godReapersKilled:
                textBox.text = "God Reapers Killed: " + playerStats.godReapersKilled;
                break;
        }
    }

    private void SetTotalPlayTime(double totalTime)
    {
        textBox.text = "Play Time: " + totalTime;
    }
}
