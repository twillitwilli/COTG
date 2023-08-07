using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPetController : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;
    private MagicController _magicController;
    private PlayerComponents _playerComponents;

    public enum MinionState { idle, followingPlayer, chasingEnemy, attacking, isDead }
    private MinionState _currentMinionState;

    public int minionStage;
    public GetClosestEnemy closestEnemy;
    public MinionMovementController minion;
    public GameObject minionModel;
    public Transform spellSpawnLocation;
    public ParticleSystem magicFocusEffect;
    public AimAtEnemy faceObject;
    public float distanceBeforeMinionWillFollow, minionStoppingDistance, minionSpeed;

    [HideInInspector] public VRPlayerController player;
    [HideInInspector] public bool setAttackCooldown, moveMinion, minionAttacking, isDead, hasTarget, setLifeDrainCD;
    [HideInInspector] public float attackCooldown;
    [HideInInspector] public float cooldownTimer, defaultStoppingDistance, targetStoppingDistance, lifeDrainTimer, minionHealth;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _playerStats = _gameManager.GetPlayerStats();
        _magicController = _gameManager.GetMagicController();
        _playerComponents = _gameManager.player.GetPlayerComponents();

        if (_magicController.GetCurrentMinion() != null)
        {
            setAttackCooldown = true;
            setLifeDrainCD = true;

            _playerComponents.minionSpawnLocation.currentMinion = gameObject;

            defaultStoppingDistance = minionStoppingDistance;
            targetStoppingDistance = minionStoppingDistance + 12;

            minionModel.transform.SetParent(null);

            FollowPlayer();

            minionHealth = _playerStats.GetMagicFocus() * 60;

            SetMagicFocus();
        }

        else { Destroy(gameObject); }
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
                    if (Vector3.Distance(transform.position, minion.transform.position) > distanceBeforeMinionWillFollow) { moveMinion = true; }
                }
                else if (moveMinion)
                {
                    minion.MoveMinion(transform);
                    if (Vector3.Distance(transform.position, minion.transform.position) > 50)
                    {
                        minion.transform.position = transform.position;
                        moveMinion = false;
                    }
                    if (Vector3.Distance(transform.position, minion.transform.position) < minionStoppingDistance) { moveMinion = false; }
                }
                if (closestEnemy.currentEnemyTarget != null)
                {
                    if (!hasTarget) 
                    {
                        if (closestEnemy.currentEnemyTarget != null) { faceObject.currentEnemy = closestEnemy.currentEnemyTarget; }
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
                if (hasTarget) { LostTarget(); }
            }
        }
    }

    public void FollowPlayer()
    {
        transform.SetParent(_playerComponents.minionSpawnLocation.transform);
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
        if (cooldownTimer > 0) { cooldownTimer -= Time.deltaTime; }
        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            return true;
        }
        return false;
    }

    public float MinionAttackDamage()
    {
        if (minionStage == 0) { return _playerStats.GetAttackDamage() * 0.75f; }
        else if (minionStage == 1) { return _playerStats.GetAttackDamage(); }
        else if (minionStage == 2) { return _playerStats.GetAttackDamage() * 1.5f; }
        else { return 0; }
    }

    public float MinionAttackCooldown()
    {
        if (minionStage == 0) { return _playerStats.GetAttackCooldown() * 2; }
        else if (minionStage == 1) { return _playerStats.GetAttackCooldown() * 1.5f; }
        else if (minionStage == 2) { return _playerStats.GetAttackCooldown(); }
        else { return 0; }
    }

    public void SetMagicFocus()
    {
        var currentMagicFocus = magicFocusEffect.main;
        currentMagicFocus.maxParticles = _playerStats.GetMagicFocus();
    }

    public void LifeDrain()
    {
        if (setLifeDrainCD)
        {
            lifeDrainTimer = 60;
            setLifeDrainCD = false;
        }
        if (lifeDrainTimer > 0) { lifeDrainTimer -= Time.deltaTime; }
        else if (lifeDrainTimer <= 0)
        {
            minionHealth -= 60;
            if (minionHealth <= 0) { MinionDead(); }
            var currentMagicFocus = magicFocusEffect.main;
            if (currentMagicFocus.maxParticles > 0) { currentMagicFocus.maxParticles -= 1; }
            setLifeDrainCD = true;
        }
    }

    public void MinionDead()
    {
        isDead = true;
        minion.minionAnimator.ChangeAnimation(MinionAnimator.minionState.dying);
    }
}
