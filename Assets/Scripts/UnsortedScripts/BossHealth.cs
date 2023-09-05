using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public BossController bossController;

    public override void Awake()
    {
        if (enemyController != null)
            base.Awake();
    }

    public override void AdjustHealth(float adjustmentValue, bool coopSync)
    {
        if (!isDead)
        {
            if (MultiplayerManager.Instance.coop && !coopSync)
                MultiplayerManager.Instance.GetCoopManager().coopEnemyController.AdjustEnemyHealth(adjustmentValue, 0, true);

            if (adjustmentValue < 0)
                WasHit();

            currentHealth += adjustmentValue;
            
            if (currentHealth <= 0)
            {
                Debug.Log("Enemy Died");

                if (!coopSync)
                    AdjustTotalStats();

                Dead();
            }

            else if (currentHealth >= maxHealth)
                currentHealth = maxHealth;

            HealthDisplay();
        }
    }

    public override void Dead()
    {
        isDead = true;
        bossController.dropOnDestroy.disableDrop = false;

        if (healthDisplay.gameObject)
            Destroy(healthDisplay.gameObject);

        bossController.BossDead();
    }
}
