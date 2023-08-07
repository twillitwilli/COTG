using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyPools/CreateNewEnemyPoolManager")]
public class EnemyPools : ScriptableObject
{
    public List<GameObject> spawnableEnemies;
    public List<EnemyStatManger> enemyStatManagers;
    public List<EnemySkins> enemySkins;
    public List<BossList> bossLists;
}
