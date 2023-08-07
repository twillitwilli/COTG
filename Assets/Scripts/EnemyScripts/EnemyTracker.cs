using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    private EnemyTrackerController _enemyTrackerController;

    public bool isBoss;
    [HideInInspector] public bool otherPlayerSpawned;
    [HideInInspector] public int spawnCount;

    private void Awake()
    {
        _enemyTrackerController = LocalGameManager.instance.GetEnemyTrackerController();
    }

    public void Start()
    {
        if (!otherPlayerSpawned)
        {
            if (isBoss)
            {
                _enemyTrackerController.UpdateBossTracker(1);
                _enemyTrackerController.spawnedBoss = gameObject;
            }
            else
            {
                _enemyTrackerController.UpdateEnemyTracker(this, 1);
                _enemyTrackerController.spawnedEnemies.Add(gameObject);
            }
        }
    }

    public void OnDestroy()
    {
        if (!otherPlayerSpawned)
        {
            if (isBoss)
            {
                _enemyTrackerController.UpdateBossTracker(-1);
                _enemyTrackerController.spawnedBoss = null;
            }
            else
            {
                _enemyTrackerController.UpdateEnemyTracker(this, -1);
                _enemyTrackerController.spawnedEnemies.Remove(gameObject);
            }
        }
    }
}
