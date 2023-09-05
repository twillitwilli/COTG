using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTrophyDisplay : MonoBehaviour
{
    public enum Enemy 
    { 
        bunny, 
        bat, 
        plant, 
        mushroom, 
        bee, 
        goblin, 
        wolf, 
        golem, 
        treant, 
        dragon, 
        babyReaper, 
        princeReaper, 
        godReaper 
    }

    public Enemy enemyType;
    
    public GameObject enemyModel;
    public Text signDisplay;

    private void OnEnable()
    {
        PlayerTotalStats totalStats = PlayerTotalStats.Instance;

        switch (enemyType)
        {
            case Enemy.bunny:
                if (totalStats.bunniesKilled > 0) { UpdateTrophyStand("Bunnies", totalStats.bunniesKilled); }
                break;

            case Enemy.bat:
                if (totalStats.batsKilled > 0) { UpdateTrophyStand("Bats", totalStats.batsKilled); }
                break;

            case Enemy.plant:
                if (totalStats.plantsKilled > 0) { UpdateTrophyStand("Plants", totalStats.plantsKilled); }
                break;

            case Enemy.mushroom:
                if (totalStats.mushroomsKilled > 0) { UpdateTrophyStand("Mushrooms", totalStats.mushroomsKilled); }
                break;

            case Enemy.bee:
                if (totalStats.beesKilled > 0) { UpdateTrophyStand("Bees", totalStats.beesKilled); }
                break;

            case Enemy.goblin:
                if (totalStats.goblinsKilled > 0) { UpdateTrophyStand("Goblins", totalStats.goblinsKilled); }
                break;

            case Enemy.wolf:
                if (totalStats.wolvesKilled > 0) { UpdateTrophyStand("Wolves", totalStats.wolvesKilled); }
                break;
            case Enemy.golem:
                if (totalStats.golemsKilled > 0) { UpdateTrophyStand("Golems", totalStats.golemsKilled); }
                break;

            case Enemy.treant:
                if (totalStats.treantGuardsKilled > 0) { UpdateTrophyStand("Treants", totalStats.treantGuardsKilled); }
                break;

            case Enemy.dragon:
                if (totalStats.dragonsKilled > 0) { UpdateTrophyStand("Dragons", totalStats.dragonsKilled); }
                break;

            case Enemy.babyReaper:
                if (totalStats.babyReapersKilled > 0) { UpdateTrophyStand("Baby Reapers", totalStats.babyReapersKilled); }
                break;

            case Enemy.princeReaper:
                if (totalStats.princeReapersKilled > 0) { UpdateTrophyStand("Prince Reapers", totalStats.princeReapersKilled); }
                break;

            case Enemy.godReaper:
                if (totalStats.godReapersKilled > 0) { UpdateTrophyStand("God Reapers", totalStats.godReapersKilled); }
                break;
        }
    }

    private void UpdateTrophyStand(string whichEnemy, int howManyKilled)
    {
        enemyModel.SetActive(true);
        signDisplay.text = whichEnemy + " Killed\n" + howManyKilled;
    }
}
