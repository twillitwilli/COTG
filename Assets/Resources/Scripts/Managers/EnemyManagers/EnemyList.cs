using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyPools/CreateNewEnemyList")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> enemies;
}
