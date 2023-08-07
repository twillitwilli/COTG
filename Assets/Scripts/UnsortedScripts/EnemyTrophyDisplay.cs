using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTrophyDisplay : MonoBehaviour
{
    public enum Enemy { bunny, bat, plant, mushroom, bee, goblin, wolf, golem, treant, dragon, babyReaper, princeReaper, godReaper }
    public Enemy enemyType;
    public GameObject enemyModel;
    public Text signDisplay;
    private PlayerTotalStats _totalStats;

    private void Awake()
    {
        _totalStats = LocalGameManager.instance.GetTotalStats();
    }

    private void OnEnable()
    {
        switch (enemyType)
        {
            case Enemy.bunny:
                if (_totalStats.bunniesKilled > 0) { UpdateTrophyStand(true, "Bunnies", _totalStats.bunniesKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.bat:
                if (_totalStats.batsKilled > 0) { UpdateTrophyStand(true, "Bats", _totalStats.batsKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.plant:
                if (_totalStats.plantsKilled > 0) { UpdateTrophyStand(true, "Plants", _totalStats.plantsKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.mushroom:
                if (_totalStats.mushroomsKilled > 0) { UpdateTrophyStand(true, "Mushrooms", _totalStats.mushroomsKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.bee:
                if (_totalStats.beesKilled > 0) { UpdateTrophyStand(true, "Bees", _totalStats.beesKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.goblin:
                if (_totalStats.goblinsKilled > 0) { UpdateTrophyStand(true, "Goblins", _totalStats.goblinsKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.wolf:
                if (_totalStats.wolvesKilled > 0) { UpdateTrophyStand(true, "Wolves", _totalStats.wolvesKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.golem:
                if (_totalStats.golemsKilled > 0) { UpdateTrophyStand(true, "Golems", _totalStats.golemsKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.treant:
                if (_totalStats.treantGuardsKilled > 0) { UpdateTrophyStand(true, "Treants", _totalStats.treantGuardsKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.dragon:
                if (_totalStats.dragonsKilled > 0) { UpdateTrophyStand(true, "Dragons", _totalStats.dragonsKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.babyReaper:
                if (_totalStats.babyReapersKilled > 0) { UpdateTrophyStand(true, "Baby Reapers", _totalStats.babyReapersKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.princeReaper:
                if (_totalStats.princeReapersKilled > 0) { UpdateTrophyStand(true, "Prince Reapers", _totalStats.princeReapersKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
            case Enemy.godReaper:
                if (_totalStats.godReapersKilled > 0) { UpdateTrophyStand(true, "God Reapers", _totalStats.godReapersKilled); }
                else { UpdateTrophyStand(false, "", 0); }
                break;
        }
    }

    private void UpdateTrophyStand(bool enemyKilled, string whichEnemy, int howManyKilled)
    {
        if (enemyKilled)
        {
            enemyModel.SetActive(true);
            signDisplay.text = whichEnemy + " Killed\n" + howManyKilled;
        }
        else
        {
            enemyModel.SetActive(false);
            signDisplay.text = null;
        }
    }
}
