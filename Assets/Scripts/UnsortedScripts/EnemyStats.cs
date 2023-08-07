using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int enemyLevel;

    [Header("Enemy Movement Stats")]
    public float movementSpeed;
    public float acceleration, stoppingDistanceFromTarget, rotationSpeed, detectPlayerRange, willWalkRange, distanceToAvoidPlayer, fleeSpeed, chaseSpeed;

    [Header("Enemy Attack Stats")]
    public float maxHealthMax;
    public float maxHealthMin, currentHealth, attackDamageMax, attackDamageMin, defense, meleeAttackRange, rangedAttackRange;
}
