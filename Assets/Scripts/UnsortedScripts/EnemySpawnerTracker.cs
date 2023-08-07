using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTracker : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    public void CheckSpawners()
    {
        for (int i = 0; i < enemySpawners.Count; i++) { enemySpawners[i].CheckSpawnLocations(); }
    }
}
