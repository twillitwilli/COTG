using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyPools/CreateNewEnemyStats")]
public class EnemyStatObject : ScriptableObject
{
    public EnemyController.Enemy typeOfEnemy;
    public EnemyController.EnemyType movementType;
    public int enemyLevel, enemySkinIdx;

    [Header("Enemy Movement Stats")]
    public float movementSpeed;
    public float acceleration, stoppingDistanceFromTarget, rotationSpeed, detectPlayerRange, willWalkRange, distanceToAvoidPlayer, fleeSpeed, chaseSpeed;

    [Header("Enemy Attack Stats")]
    public float maxHealthMax;
    public float maxHealthMin, attackDamageMax, attackDamageMin, defense, meleeAttackRange, rangedAttackRange;
}
