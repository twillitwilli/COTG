using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCollider : MonoBehaviour
{
    public MinionPetController minionController;

    public void HitPet()
    {
        minionController.closestEnemy.currentEnemyTarget = null;
        minionController.minionStoppingDistance = minionController.defaultStoppingDistance;
        minionController.hasTarget = false;
        minionController.minionAttacking = false;
        minionController.setAttackCooldown = true;
        minionController.FollowPlayer();
    }
}
