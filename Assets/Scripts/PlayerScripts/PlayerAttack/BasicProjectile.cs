using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public bool forDemoOnly;
    public float attackDamage, projectileSpeed, projectileRange, aimAssist;
    public LayerMask ignoreLayers;
    public GameObject collisionEffect;
    public GameObject[] attackLevelEffects;

    [HideInInspector] public VRPlayerController player;
    [HideInInspector] public PlayerStats playerStats;
    [HideInInspector] public MagicController magicController;

    [HideInInspector] public Transform spawnParent;
    [HideInInspector] public bool tempPeircing, disableCrit, minionProjectile;
    [HideInInspector] public Rigidbody rb;

    protected bool rayHit;
    protected int statusEffectChance;
    protected List<string> critEffects = new List<string>();

    public virtual void Awake()
    {
        playerStats = LocalGameManager.Instance.GetPlayerStats();
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Start()
    {
        transform.SetParent(null);

        if (!minionProjectile) { StatModifier(); }

        rb.velocity = transform.forward * projectileSpeed;
        Invoke("DestroyThis", projectileRange);
    }

    public virtual void StatModifier()
    {
        attackDamage = playerStats.GetAttackDamage();
        projectileRange = playerStats.GetAttackRange();
        aimAssist = playerStats.GetAimAssist();

        CheckAttackLevel();
    }

    public virtual void CheckAttackLevel()
    {
        if (attackDamage < 20) { ChangeLevel(0); }
        else if (attackDamage >= 20 && attackDamage < 35) { ChangeLevel(1); }
        else { ChangeLevel(2); }
    }

    public virtual void ChangeLevel(int level)
    {
        for (int i = 0; i < attackLevelEffects.Length; i++)
        {
            if (level != i) { attackLevelEffects[i].SetActive(false); }
            else { attackLevelEffects[i].SetActive(true); }
        }
    }

    public virtual void FixedUpdate()
    {
        if (!forDemoOnly) { AimAssist(); }

        if (spawnParent != null && magicController.controllabeAttack) { rb.velocity = spawnParent.transform.right * projectileSpeed; }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!forDemoOnly)
        {
            if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyHealth>())
            {
                EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
                HitEnemy(enemy);
            }

            else if (other.gameObject.CompareTag("EnemyShield"))
            {
                EnemyShield shield = other.gameObject.GetComponent<EnemyShield>();
                shield.ShieldHit(attackDamage);
                CollisionAction();
            }

            else if (other.gameObject.CompareTag("Target"))
            {
                other.gameObject.GetComponent<TargetCollider>().TargetHit();
            }

            else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Climb Point"))
            {
                CollisionAction();
            }

            if (other.gameObject.GetComponent<BreakableObject>())
            {
                other.gameObject.GetComponent<BreakableObject>().BreakObjectWithAttack();
            }

            if (other.gameObject.GetComponent<MinionCollider>())
            {
                other.gameObject.GetComponent<MinionCollider>().HitPet();
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Target"))
            {
                other.gameObject.GetComponent<TargetCollider>().TargetHit();
                Destroy(gameObject);
            }
        }
    }

    public virtual void AimAssist()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, aimAssist, rb.velocity, out hit, 15, -ignoreLayers))
        {
            if (hit.collider.CompareTag("Enemy") || (hit.collider.GetComponent<BreakableObject>() && !hit.collider.CompareTag("Rock")))
            {
                Vector3 directionChange = hit.point - transform.position;
                float vel = rb.velocity.magnitude;
                rb.velocity = Vector3.Normalize(rb.velocity + Vector3.Normalize(directionChange)) * vel;
            }
        }
    }

    public virtual void HitEnemy(EnemyHealth enemy)
    {
        if (!disableCrit && playerStats.HitCrit())
        {
            int critAttackDamage = Mathf.RoundToInt(attackDamage + (attackDamage * playerStats.GetCritDamage()));
            enemy.AdjustHealth(-critAttackDamage, false);
        }

        else { enemy.AdjustHealth(-attackDamage, false); }


        if (playerStats.SpecialAttack()) { SpecialEffect(); }

        CollisionAction();
    }

    public virtual void CollisionAction()
    {
        if (collisionEffect != null) { Instantiate(collisionEffect, transform.position, transform.rotation); }

        switch (magicController.currentCollisionEffects)
        {
            case MagicController.CollisionEffects.none:
                if (!tempPeircing) { Destroy(gameObject); }
                break;

            case MagicController.CollisionEffects.peircing:
                break;

            case MagicController.CollisionEffects.bouncing:
                break;

            case MagicController.CollisionEffects.split:
                break;
        }
    }

    public virtual void ElementalEffect()
    {

    }

    public virtual void StatusEffect()
    {

    }

    public virtual void SpecialEffect()
    {

    }
}
