using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEventController : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyController;

    public GameObject[] meleeAttackTriggers;
    [HideInInspector] public bool castingMultipleSpells;

    public virtual void AttackTriggerOn()
    {
        meleeAttackTriggers[enemyController.animationController.currentAttack].SetActive(true);
    }

    public virtual void CastingSpellEffect()
    {
        enemyController.enemyAttack.castingSpellEffect.SetActive(true);
    }

    public virtual void CastingMultipleSpells()
    {
        castingMultipleSpells = true;
        enemyController.enemyAttack.castingSpellEffect.SetActive(true);
    }

    public virtual void TurnOffCastingEffect()
    {
        castingMultipleSpells = false;
        enemyController.enemyAttack.castingSpellEffect.SetActive(false);
    }

    public virtual void FireProjectile()
    {
        enemyController.enemyAttack.FireProjectile();
        if (!castingMultipleSpells) { enemyController.enemyAttack.castingSpellEffect.SetActive(false); }
    }

    public virtual void AttackDone()
    {
        enemyController.enemyAttack.castingSpellEffect.SetActive(false);
        castingMultipleSpells = false;
        if (meleeAttackTriggers.Length > 0) { foreach (GameObject obj in meleeAttackTriggers) { obj.SetActive(false); } }
        if (!enemyController.otherPlayerSpawned) { enemyController.animationController.ChangeAnimation(EnemyAnimationController.AnimationState.idle); }
        enemyController.enemyAttack.ResetAttackCooldown();
        enemyController.enemyAttack.ResetAttack();
        enemyController.stopMovement = false;
    }

    public virtual void EnemyDead()
    {
        enemyController.DestroyEnemy();
    }
}
