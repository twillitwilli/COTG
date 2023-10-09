using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.Interfaces;

public class DamageOverTimeNode : iCooldownable
{
    public float cooldownTimer { get; set; }
    public bool forPlayer { get; set; }
    public VRPlayer player { get; set; }
    public EnemyController enemy { get; set; }
    public string nameOfAttack { get; set; }
    public float howLong { get; set; }
    public float damage { get; set; }

    void LateUpdate()
    {
        if (CooldownDone())
        {
            if (forPlayer)
                PlayerStats.Instance.Damage(-damage);

            else
                enemy.enemyHealth.AdjustHealth(-damage, false);

            CooldownDone(true, 0.5f);
        }
    }

    public bool CooldownDone(bool setTimer = false, float cooldownTime = 0)
    {
        if (setTimer)
            cooldownTimer = cooldownTime;

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        else
            return true;

        return false;
    }

    void OnDestroy()
    {
        switch (nameOfAttack)
        {
            case "Burning":
                enemy.enemyHealth.statusController.burnEffect.SetActive(false);
                enemy.enemyHealth.statusController.isBurning = false;
                break;

            case "Electrocuted":
                enemy.enemyHealth.statusController.electrocutedEffect.SetActive(false);
                enemy.enemyHealth.statusController.isElectrocuted = false;
                break;

            case "Life Drained":
                enemy.enemyHealth.statusController.lifeDrainEffect.SetActive(false);
                enemy.enemyHealth.statusController.isLifeDraining = false;
                break;

            case "Poisoned":
                enemy.enemyHealth.statusController.poisonEffect.SetActive(false);
                enemy.enemyHealth.statusController.isPoisoned = false;
                break;
        }
    }
}
