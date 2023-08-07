using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyPools/CreateEnemyStatManager")]
public class EnemyStatManger : ScriptableObject
{
    public EnemyController.Enemy enemyType;
    public List<EnemyStatObject> enemyStatObjects;
}
