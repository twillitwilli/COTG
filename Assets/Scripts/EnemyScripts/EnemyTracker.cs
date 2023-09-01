using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public bool isBoss;

    [HideInInspector] 
    public bool otherPlayerSpawned;

    [HideInInspector] 
    public int spawnCount;

    public void Start()
    {
        if (!otherPlayerSpawned)
        {
            if (isBoss)
            {
                EnemyTrackerController.Instance.UpdateBossTracker(1);
                EnemyTrackerController.Instance.spawnedBoss = gameObject;
            }

            else
            {
                EnemyTrackerController.Instance.UpdateEnemyTracker(this, 1);
                EnemyTrackerController.Instance.spawnedEnemies.Add(gameObject);
            }
        }
    }

    public void OnDestroy()
    {
        if (!otherPlayerSpawned)
        {
            if (isBoss)
            {
                EnemyTrackerController.Instance.UpdateBossTracker(-1);
                EnemyTrackerController.Instance.spawnedBoss = null;
            }

            else
            {
                EnemyTrackerController.Instance.UpdateEnemyTracker(this, -1);
                EnemyTrackerController.Instance.spawnedEnemies.Remove(gameObject);
            }
        }
    }
}
