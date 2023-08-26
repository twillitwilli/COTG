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
        _totalStats = LocalGameManager.Instance.GetTotalStats();
    }

    private void OnEnable()
    {
        switch (enemyType)
        {
            case Enemy.bunny:
                if (_totalStats.bunniesKilled > 0) { UpdateTrophyStand("Bunnies", _totalStats.bunniesKilled); }
                break;

            case Enemy.bat:
                if (_totalStats.batsKilled > 0) { UpdateTrophyStand("Bats", _totalStats.batsKilled); }
                break;

            case Enemy.plant:
                if (_totalStats.plantsKilled > 0) { UpdateTrophyStand("Plants", _totalStats.plantsKilled); }
                break;

            case Enemy.mushroom:
                if (_totalStats.mushroomsKilled > 0) { UpdateTrophyStand("Mushrooms", _totalStats.mushroomsKilled); }
                break;

            case Enemy.bee:
                if (_totalStats.beesKilled > 0) { UpdateTrophyStand("Bees", _totalStats.beesKilled); }
                break;

            case Enemy.goblin:
                if (_totalStats.goblinsKilled > 0) { UpdateTrophyStand("Goblins", _totalStats.goblinsKilled); }
                break;

            case Enemy.wolf:
                if (_totalStats.wolvesKilled > 0) { UpdateTrophyStand("Wolves", _totalStats.wolvesKilled); }
                break;
            case Enemy.golem:
                if (_totalStats.golemsKilled > 0) { UpdateTrophyStand("Golems", _totalStats.golemsKilled); }
                break;

            case Enemy.treant:
                if (_totalStats.treantGuardsKilled > 0) { UpdateTrophyStand("Treants", _totalStats.treantGuardsKilled); }
                break;

            case Enemy.dragon:
                if (_totalStats.dragonsKilled > 0) { UpdateTrophyStand("Dragons", _totalStats.dragonsKilled); }
                break;

            case Enemy.babyReaper:
                if (_totalStats.babyReapersKilled > 0) { UpdateTrophyStand("Baby Reapers", _totalStats.babyReapersKilled); }
                break;

            case Enemy.princeReaper:
                if (_totalStats.princeReapersKilled > 0) { UpdateTrophyStand("Prince Reapers", _totalStats.princeReapersKilled); }
                break;

            case Enemy.godReaper:
                if (_totalStats.godReapersKilled > 0) { UpdateTrophyStand("God Reapers", _totalStats.godReapersKilled); }
                break;
        }
    }

    private void UpdateTrophyStand(string whichEnemy, int howManyKilled)
    {
        enemyModel.SetActive(true);
        signDisplay.text = whichEnemy + " Killed\n" + howManyKilled;
    }
}
