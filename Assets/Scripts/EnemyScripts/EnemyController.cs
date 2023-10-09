using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum Enemy 
    { 
        bat, 
        bee, 
        bunny, 
        goblin, 
        mushroom, 
        plant, 
        wolf, 
        golem, 
        treant, 
        dragon, 
        babyReaper, 
        princeReaper, 
        godReaper 
    }

    public Enemy enemyName;

    public enum EnemyType 
    { 
        chaser, 
        wanderer, 
        stationary, 
        teleporter 
    }

    public EnemyType typeOfEnemy;

    public enum EnemyState 
    { 
        spawn, 
        idle, 
        wandering, 
        chasing, 
        attacking, 
        death 
    }

    public EnemyState currentEnemyState;

    public EnemyStats enemyStats;
    public Transform wanderPoint, enemyModelCenter;
    public float lookRotationSpeed = 5f;
    public float detectPlayerRange = 10f;
    public float willWalkRange = 3.5f;
    public bool canRun, avoidPlayer;
    public float distanceToAvoidPlayer = 2;
    public float fleeSpeed = 0.01f;
    public float chaseSpeed;
    public EnemyHealth enemyHealth;

    [Header("Raycast Layers")]
    public LayerMask ignoreLayers;

    public VRPlayer player { get; private set; }
    private PlayerComponents _playerComponents;

    [HideInInspector] 
    public EnemyAnimationController animationController;
    
    [HideInInspector] 
    public EnemyTracker enemyTracker;
    
    [HideInInspector] 
    public EnemyAttack enemyAttack;
    
    [HideInInspector] 
    public DropOnDestroy dropScript;
    
    [HideInInspector] 
    public Transform playerTarget, currentTarget;
    
    [HideInInspector] 
    public NavMeshAgent agent;
    
    [HideInInspector] 
    public bool isDead, idleAnim, stopMovement, stopRotation, agroCurrentPlayer, otherPlayerSpawned, idAssigned;
    
    [HideInInspector] 
    public int spawnID, roomID, enemyID, enemyLevel;
    
    [HideInInspector] 
    public float currentPlayerDamage, otherPlayerDamage;

    protected Rigidbody rb;
    protected bool gotToPoint, runningAnim, deadFailSafe;
    protected float agentStartSpeed, distanceFromPlayer;
    protected bool enemyReady;
    protected Vector3 lastPos;

    

    public virtual void Awake()
    {
        player = LocalGameManager.Instance.player;
        _playerComponents = player.GetPlayerComponents();

        animationController = GetComponent<EnemyAnimationController>();
        enemyTracker = GetComponent<EnemyTracker>();
        enemyAttack = GetComponent<EnemyAttack>();
        dropScript = GetComponent<DropOnDestroy>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public async virtual void Start()
    {
        otherPlayerSpawned = enemyTracker.otherPlayerSpawned;
        animationController.ChangeAnimation(EnemyAnimationController.AnimationState.spawn);
        idleAnim = true;
        wanderPoint.SetParent(null);

        if (!MultiplayerManager.Instance.coop)
            playerTarget = _playerComponents.playerTarget;

        else if (MultiplayerManager.Instance.coop && !otherPlayerSpawned && DistanceAgroCheck()) 
        { 
            if (DistanceAgroCheck())
                SetPlayerTarget();

            else
                ChangePlayerTarget();
        }

        if (!otherPlayerSpawned)
            currentTarget = playerTarget;

        if (!enemyTracker.otherPlayerSpawned)
        {
            agentStartSpeed = agent.speed;
            agent.isStopped = true;
            gotToPoint = true;
            rb.isKinematic = false;

            int delayTimer = Mathf.RoundToInt(1000 * Random.Range(0.1f, 2f));

            await Task.Delay(delayTimer);

            EnemyReady();
        }

        else
        {
            Destroy(agent);
            rb.isKinematic = true;
            enemyReady = true;
        }

        if (LocalGameManager.Instance.currentGameMode == LocalGameManager.GameMode.master)
            agent.speed += (agent.speed * 0.25f);
    }

    public void AdjustEnemyStats()
    {

    }

    public async virtual void FixedUpdate()
    {
        if (!isDead)
        {
            if (!otherPlayerSpawned && playerTarget != null)
            {
                distanceFromPlayer = Vector3.Distance(playerTarget.position, transform.position);

                if (enemyReady && !enemyAttack.isAttacking)
                {
                    if (!stopMovement)
                        EnemyMovementController();

                    if (!stopRotation)
                        FaceTarget(currentTarget);

                    enemyAttack.AttackController(distanceFromPlayer);
                }

                else if (enemyAttack.isAttacking)
                {
                    if (!stopRotation)
                        FaceTarget(currentTarget);

                    if (enemyAttack.moveWhileAttacking)
                        MoveWhileAttacking();

                    else 
                    { 
                        stopMovement = true; 
                        agent.isStopped = true; 
                    }

                    enemyAttack.AttackController(distanceFromPlayer);
                }

                if (MultiplayerManager.Instance.coop)
                    MultiplayerManager.Instance.GetCoopManager().coopEnemyController.SendTrackingInfo(spawnID, transform.position, transform.localEulerAngles, agroCurrentPlayer);
            }
        }

        else 
        {
            if (enemyAttack.castingSpellEffect.activeSelf)
                enemyAttack.castingSpellEffect.SetActive(false);

            if (!otherPlayerSpawned)
            {
                agent.isStopped = true;
                agent.speed = 0;
            }
        }

        if (enemyHealth.Health <= 0 && !deadFailSafe) 
        {
            await Task.Delay(3000);

            DestroyEnemy();

            deadFailSafe = true; 
        }
    }

    public virtual void EnemyMovementController()
    {
        switch (typeOfEnemy)
        {
            case EnemyType.chaser:
                if (agent.isStopped)
                    agent.isStopped = false;

                ChaseController();
                break;

            case EnemyType.wanderer:
                if (agent.isStopped)
                    agent.isStopped = false;

                WandererController();
                break;

            case EnemyType.stationary:
                if (!stopMovement)
                    stopMovement = true;
                break;

            case EnemyType.teleporter:
                break;
        }
    }

    public virtual void ChaseController()
    {
        if (distanceFromPlayer <= detectPlayerRange)
        {
            if(currentTarget != playerTarget)
                currentTarget = playerTarget;

            WalkingRunningController(distanceFromPlayer);
            agent.SetDestination(playerTarget.position);
        }

        else if (distanceFromPlayer > detectPlayerRange)
        {
            agent.speed = agentStartSpeed;
            WandererController();
        }
    }

    public virtual void MoveWhileAttacking()
    {
        agent.speed = agentStartSpeed / 2;
        agent.SetDestination(playerTarget.position);
    }

    public virtual void WandererController()
    {
        if (MultiplayerManager.Instance.coop) 
        {
            if (DistanceAgroCheck())
                SetPlayerTarget();

            else
                ChangePlayerTarget();
        }

        if (gotToPoint)
            CheckPathToNewPoint(CreateNewWanderPoint());

        else
        {
            if (lastPos == transform.position)
                gotToPoint = true;

            currentTarget = wanderPoint;
            float distanceFromPoint = Vector3.Distance(wanderPoint.position, transform.position);
            WalkingRunningController(distanceFromPoint);
            agent.SetDestination(wanderPoint.position);

            if (distanceFromPoint - 1 <= agent.stoppingDistance)
                gotToPoint = true;

            lastPos = transform.position;
        }
    }

    public virtual void CheckPathToNewPoint(Vector3 checkPos)
    {
        RaycastHit hit;
        float range = Vector3.Distance(checkPos, transform.position);

        if (Physics.Raycast(transform.position, checkPos - transform.position, out hit, range, -ignoreLayers))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Climb Point") || hit.collider.CompareTag("Ground"))
                CreateNewWanderPoint();
        }

        else 
            gotToPoint = false;
    }

    public virtual Vector3 CreateNewWanderPoint()
    {
        float newX = transform.position.x + Random.Range(-20, 20);
        float newZ = transform.position.z + Random.Range(-20, 20);
        wanderPoint.position = new Vector3(newX, 46, newZ);

        return wanderPoint.position;
    }

    public virtual void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);

        if (agent.isStopped && agent.speed == 0 && idleAnim && !enemyAttack.attackStarted)
        {
            if (TurningRight(target.position))
                animationController.ChangeAnimation(EnemyAnimationController.AnimationState.turnRight);

            else
                animationController.ChangeAnimation(EnemyAnimationController.AnimationState.turnLeft);
        }
    }

    public virtual bool TurningRight(Vector3 target)
    {
        if (transform.InverseTransformPoint(target).x > 0)
            return true;

        else return false;
    }

    public virtual void FaceAwayFromTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    public virtual bool FacingTarget()
    {
        Vector3 currentPos = transform.position;
        Vector3 playerPos = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
        Vector3 direction = playerPos - currentPos;

        float viewRange = Vector3.Angle(transform.forward, direction);

        if (viewRange < 45)
            return true;

        else return false;
    }

    public virtual void WalkingRunningController(float distanceFromTarget)
    {
        if (canRun && distanceFromTarget > willWalkRange)
        {
            agent.speed = (agentStartSpeed + chaseSpeed);

            if (!runningAnim || idleAnim)
            {
                animationController.ChangeAnimation(EnemyAnimationController.AnimationState.running);
                runningAnim = true;
                idleAnim = false;
            }
        }

        else
        {
            agent.speed = agentStartSpeed;

            if (runningAnim || idleAnim)
            {
                animationController.ChangeAnimation(EnemyAnimationController.AnimationState.walking);
                runningAnim = false;
                idleAnim = false;
            }
        }
    }

    public float CurrentPlayerDistance()
    {
        return Vector3.Distance(transform.position, _playerComponents.playerTarget.position);
    }

    public float OtherPlayerDistance()
    {
        return Vector3.Distance(transform.position, MultiplayerManager.Instance.GetCoopManager().otherPlayerTracker.transform.position);
    }

    public bool DistanceAgroCheck()
    {
        if (CurrentPlayerDistance() < OtherPlayerDistance())
            return true;

        else return false;
    }

    public void DamageAgroCheck()
    {
        if (currentPlayerDamage > otherPlayerDamage)
            agroCurrentPlayer = true;

        else 
            agroCurrentPlayer = false;
    }

    public void SetPlayerTarget()
    {
        playerTarget = _playerComponents.playerTarget;
        agroCurrentPlayer = true;
    }

    public void ChangePlayerTarget()
    {
        playerTarget = MultiplayerManager.Instance.GetCoopManager().otherPlayerTracker.transform;
        agroCurrentPlayer = false;
    }

    public virtual void EnemyDead()
    {
        isDead = true;
        Destroy(wanderPoint.gameObject);

        if (!otherPlayerSpawned)
            animationController.ChangeAnimation(EnemyAnimationController.AnimationState.death);
    }

    public virtual void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public virtual void EnemyReady()
    {
        rb.isKinematic = true;
        agent.isStopped = false;
        enemyReady = true;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectPlayerRange);
        Gizmos.DrawWireSphere(transform.position, willWalkRange);
    }
}
