using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyAttack : MonoBehaviour
{
    public bool moveWhileAttacking;

    [Header("Melee Attack Settings")]
    public bool hasMeleeAttack;
    public float meleeAttackRange;

    [Header("Ranged Attack Settings")]
    public bool hasRangedAttack;
    public float rangedAttackRange;
    public Transform rangedSpawnLocation;
    public GameObject castingSpellEffect;
    public GameObject[] projectileAttack;

    protected EnemyController enemyController;
    protected float cooldownTimer;
    protected bool nextUseAvailable;

    [HideInInspector] public bool meleeAttack, isAttacking, attackStarted, playAttackAnim;

    public virtual void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        ResetAttackCooldown();
    }

    public virtual void AttackController(float distanceFromPlayer)
    {
        if (!isAttacking)
        {
            if (nextUseAvailable && InAttackRange(distanceFromPlayer))
            {
                enemyController.animationController.ChangeAnimation(EnemyAnimationController.AnimationState.idle);
                enemyController.currentTarget = enemyController.playerTarget;
                ResetAttackCooldown();
                isAttacking = true;
            }
            else { CooldownForAttacking(); }
        }
        else { Attacking(); }
    }

    public virtual void Attacking()
    {
        if (!attackStarted) { StartAttack(); }
        else if (attackStarted && !playAttackAnim)
        {
            playAttackAnim = true;
            if (meleeAttack) { enemyController.animationController.ChangeAnimation(EnemyAnimationController.AnimationState.meleeAttack); }
            else enemyController.animationController.ChangeAnimation(EnemyAnimationController.AnimationState.rangedAttack);
        }
    }

    public virtual bool InAttackRange(float distanceFromTarget)
    {
        if (!nextUseAvailable) { return false; }
        else
        {
            if (hasMeleeAttack && MeleeAttackRange(distanceFromTarget)) { meleeAttack = true; return true; }
            else if (hasRangedAttack && RangedAttackRange(distanceFromTarget) && !MeleeAttackRange(distanceFromTarget)) { meleeAttack = false; return true; }
        }
        return false;
    }

    public virtual void CooldownForAttacking()
    {
        if (cooldownTimer > 0) { cooldownTimer -= Time.deltaTime; }
        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            nextUseAvailable = true;
        }
    }

    public virtual bool MeleeAttackRange(float distanceFromTarget)
    {
        if ((distanceFromTarget - meleeAttackRange) <= enemyController.agent.stoppingDistance) { return true; }
        else return false;
    }

    public virtual bool RangedAttackRange(float distanceFromTarget)
    {
        if ((distanceFromTarget - rangedAttackRange) <= enemyController.agent.stoppingDistance) { return true; }
        else return false;
    }

    public virtual void StartAttack()
    {
        if (enemyController.FacingTarget()) { attackStarted = true; }
    }

    public virtual void FireProjectile()
    {
        GameObject projectile = Instantiate(projectileAttack[enemyController.animationController.currentAttack], rangedSpawnLocation.position, rangedSpawnLocation.rotation);
        projectile.transform.SetParent(null);
        projectile.transform.localScale = new Vector3(1, 1, 1);
    }

    public virtual void ResetAttackCooldown()
    {
        nextUseAvailable = false;
        cooldownTimer = Random.Range(2f, 4f);
    }

    public virtual void ResetAttack()
    {
        isAttacking = false;
        attackStarted = false;
        playAttackAnim = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
    }
}
