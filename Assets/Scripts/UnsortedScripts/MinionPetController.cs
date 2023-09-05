using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPetController : MonoBehaviour
{
    public enum MinionState 
    { 
        idle, 
        followingPlayer, 
        chasingEnemy, 
        attacking, 
        isDead 
    }

    private MinionState _currentMinionState;

    public int minionStage;

    public GetClosestEnemy closestEnemy;
    public MinionMovementController minion;
    public GameObject minionModel;
    public Transform spellSpawnLocation;
    public ParticleSystem magicFocusEffect;
    public AimAtEnemy faceObject;

    public float distanceBeforeMinionWillFollow, minionStoppingDistance, minionSpeed;

    [HideInInspector] 
    public bool setAttackCooldown, moveMinion, minionAttacking, isDead, hasTarget, setLifeDrainCD;
    
    [HideInInspector] 
    public float attackCooldown;
    
    [HideInInspector] 
    public float cooldownTimer, defaultStoppingDistance, targetStoppingDistance, lifeDrainTimer, minionHealth;

    private void Start()
    {
        if (MagicController.Instance.currentMinion != null)
        {
            setAttackCooldown = true;
            setLifeDrainCD = true;

            LocalGameManager.Instance.player.GetPlayerComponents().minionSpawnLocation.currentMinion = gameObject;

            defaultStoppingDistance = minionStoppingDistance;
            targetStoppingDistance = minionStoppingDistance + 12;

            minionModel.transform.SetParent(null);

            FollowPlayer();

            minionHealth = PlayerStats.Instance.GetMagicFocus() * 60;

            SetMagicFocus();
        }

        else
            Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (!isDead)
        {
            LifeDrain();

            if (!minionAttacking)
            {
                if (!moveMinion)
                {
                    minion.minionAnimator.ChangeAnimation(MinionAnimator.minionState.idle);

                    if (Vector3.Distance(transform.position, minion.transform.position) > distanceBeforeMinionWillFollow)
                        moveMinion = true;
                }

                else if (moveMinion)
                {
                    minion.MoveMinion(transform);
                    if (Vector3.Distance(transform.position, minion.transform.position) > 50)
                    {
                        minion.transform.position = transform.position;
                        moveMinion = false;
                    }

                    if (Vector3.Distance(transform.position, minion.transform.position) < minionStoppingDistance)
                        moveMinion = false;
                }

                if (closestEnemy.currentEnemyTarget != null)
                {
                    if (!hasTarget) 
                    {
                        if (closestEnemy.currentEnemyTarget != null)
                            faceObject.currentEnemy = closestEnemy.currentEnemyTarget;

                        hasTarget = true; 
                    }
                    ChaseTarget(closestEnemy.currentEnemyTarget.transform);

                    if (AttackCooldown())
                    {
                        minion.minionAnimator.ChangeAnimation(MinionAnimator.minionState.attacking);
                        setAttackCooldown = true;
                        minionAttacking = true;
                    }
                }

                if (hasTarget)
                    LostTarget();
            }
        }
    }

    public void FollowPlayer()
    {
        transform.SetParent(LocalGameManager.Instance.player.GetPlayerComponents().minionSpawnLocation.transform);

        transform.localPosition = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void ChaseTarget(Transform target)
    {
        if (target != null)
        {
            minionStoppingDistance = targetStoppingDistance;
            transform.SetParent(null);
            transform.position = target.position;
        }
    }

    public void LostTarget()
    {
        if (closestEnemy.currentEnemyTarget == null)
        {
            minionStoppingDistance = defaultStoppingDistance;
            setAttackCooldown = true;
            FollowPlayer();
            hasTarget = false;
        }
    }

    public bool AttackCooldown()
    {
        if (setAttackCooldown)
        {
            cooldownTimer = MinionAttackCooldown();
            setAttackCooldown = false;
        }
        
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            return true;
        }

        return false;
    }

    public float MinionAttackDamage()
    {
        if (minionStage == 0)
            return PlayerStats.Instance.GetAttackDamage() * 0.75f;

        else if (minionStage == 1)
            return PlayerStats.Instance.GetAttackDamage();

        else if (minionStage == 2)
            return PlayerStats.Instance.GetAttackDamage() * 1.5f;

        else return 0;
    }

    public float MinionAttackCooldown()
    {
        if (minionStage == 0)
            return PlayerStats.Instance.GetAttackCooldown() * 2;

        else if (minionStage == 1)
            return PlayerStats.Instance.GetAttackCooldown() * 1.5f;

        else if (minionStage == 2)
            return PlayerStats.Instance.GetAttackCooldown();

        else return 0;
    }

    public void SetMagicFocus()
    {
        var currentMagicFocus = magicFocusEffect.main;
        currentMagicFocus.maxParticles = PlayerStats.Instance.GetMagicFocus();
    }

    public void LifeDrain()
    {
        if (setLifeDrainCD)
        {
            lifeDrainTimer = 60;
            setLifeDrainCD = false;
        }

        if (lifeDrainTimer > 0)
            lifeDrainTimer -= Time.deltaTime;

        else if (lifeDrainTimer <= 0)
        {
            minionHealth -= 60;
            if (minionHealth <= 0)
                MinionDead();

            var currentMagicFocus = magicFocusEffect.main;
            if (currentMagicFocus.maxParticles > 0)
                currentMagicFocus.maxParticles -= 1;

            setLifeDrainCD = true;
        }
    }

    public void MinionDead()
    {
        isDead = true;
        minion.minionAnimator.ChangeAnimation(MinionAnimator.minionState.dying);
    }
}
