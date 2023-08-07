using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private VRPlayerController _player;

    public EnemyController enemyController;
    public EnemyHealthDisplay healthDisplayController;
    public SkinnedMeshRenderer healthDisplay;
    public bool isBoss;
    public float maxHealth;

    [Header("EnemyMesh")]
    public SkinnedMeshRenderer enemyMesh;
    public Material normal, wasHit;

    [HideInInspector] public EnemyStatusController statusController;
    [HideInInspector] public bool isDead;
    [HideInInspector] public float currentHealth;

    public virtual void Awake()
    {
        _gameManager = LocalGameManager.instance;
        _player = _gameManager.player;

        statusController = GetComponent<EnemyStatusController>();

        if (_gameManager.hardMode) { maxHealth += maxHealth * 0.25f; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        HealthDisplay();
    }

    public virtual void AdjustHealth(float adjustmentValue, bool coopSync)
    {
        if (!isDead)
        {
            if (CoopManager.instance != null && !coopSync) 
            { 
                CoopManager.instance.coopEnemyController.AdjustEnemyHealth(adjustmentValue, enemyController.spawnID, enemyController.enemyTracker.isBoss);
            }
            if (adjustmentValue < 0) { WasHit(); }
            currentHealth += adjustmentValue;
            if (currentHealth <= 0)
            {
                Debug.Log("Enemy Died");
                if (!coopSync) { AdjustTotalStats(); }
                Dead();
            }
            else if (currentHealth >= maxHealth) { currentHealth = maxHealth; }
            HealthDisplay();
        }
    }

    public void HealthDisplay()
    {
        float healthPercentage = (currentHealth / maxHealth);
        float healthWholeNumber = healthPercentage * 100;
        healthDisplay.SetBlendShapeWeight(0, healthWholeNumber);
        healthDisplayController.UpdateDisplay(Mathf.RoundToInt(currentHealth), Mathf.RoundToInt(maxHealth));
    }

    public void WasHit()
    {
        enemyMesh.material = wasHit;
        Invoke("ReturnToNormalMaterial", 0.1f);
    }

    private void ReturnToNormalMaterial()
    {
        enemyMesh.material = normal;
    }

    public void AdjustTotalStats()
    {
        PlayerTotalStats totalStats = LocalGameManager.instance.GetTotalStats();
        switch (enemyController.enemyName)
        {
            case EnemyController.Enemy.bat:
                totalStats.AdjustStat(PlayerTotalStats.StatType.batsKilled, 0);
                break;
            case EnemyController.Enemy.bee:
                totalStats.AdjustStat(PlayerTotalStats.StatType.beesKilled, 0);
                break;
            case EnemyController.Enemy.bunny:
                totalStats.AdjustStat(PlayerTotalStats.StatType.bunniesKilled, 0);
                break;
            case EnemyController.Enemy.goblin:
                totalStats.AdjustStat(PlayerTotalStats.StatType.goblinsKilled, 0);
                break;
            case EnemyController.Enemy.mushroom:
                totalStats.AdjustStat(PlayerTotalStats.StatType.mushroomsKilled, 0);
                break;
            case EnemyController.Enemy.plant:
                totalStats.AdjustStat(PlayerTotalStats.StatType.plantsKilled, 0);
                break;
            case EnemyController.Enemy.wolf:
                totalStats.AdjustStat(PlayerTotalStats.StatType.wolvesKilled, 0);
                break;
            case EnemyController.Enemy.golem:
                totalStats.AdjustStat(PlayerTotalStats.StatType.golemsKilled, 0);
                break;
            case EnemyController.Enemy.treant:
                totalStats.AdjustStat(PlayerTotalStats.StatType.treantsKilled, 0);
                break;
            case EnemyController.Enemy.dragon:
                totalStats.AdjustStat(PlayerTotalStats.StatType.dragonsKilled, 0);
                break;
            case EnemyController.Enemy.babyReaper:
                totalStats.AdjustStat(PlayerTotalStats.StatType.babyReaperKills, 0);
                break;
            case EnemyController.Enemy.princeReaper:
                totalStats.AdjustStat(PlayerTotalStats.StatType.princeReapersKilled, 0);
                break;
            case EnemyController.Enemy.godReaper:
                totalStats.AdjustStat(PlayerTotalStats.StatType.godReapersKilled, 0);
                break;
        }
        totalStats.AdjustStat(PlayerTotalStats.StatType.enemiesKilled, 0);
    }

    public virtual void Dead()
    {
        isDead = true;
        enemyController.dropScript.disableDrop = false;
        if (healthDisplay.gameObject) { Destroy(healthDisplay.gameObject); }
        enemyController.EnemyDead();
    }
}
