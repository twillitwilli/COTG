using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.Interfaces;

public class DamageOverTimeNode : iCooldownable
{
    public float cooldownTimer { get; set; }

    [HideInInspector] 
    public bool forPlayer;
    
    [HideInInspector] 
    public VRPlayerController player;
    
    [HideInInspector] 
    public EnemyController enemy;
    
    [HideInInspector] 
    public string nameOfAttack;
    
    [HideInInspector] 
    public float howLong;
    
    [HideInInspector] 
    public float damage;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (CooldownDone())
        {
            if (forPlayer)
                PlayerStats.Instance.AdjustHealth(-damage, nameOfAttack);

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

    private void OnDestroy()
    {
        if (nameOfAttack == "Burning")
        {
            enemy.enemyHealth.statusController.burnEffect.SetActive(false);
            enemy.enemyHealth.statusController.isBurning = false;
        }

        else if (nameOfAttack == "Eletrocuted")
        {
            enemy.enemyHealth.statusController.electrocutedEffect.SetActive(false);
            enemy.enemyHealth.statusController.isElectrocuted = false;
        }

        else if (nameOfAttack == "Life Drained")
        {
            enemy.enemyHealth.statusController.lifeDrainEffect.SetActive(false);
            enemy.enemyHealth.statusController.isLifeDraining = false;
        }

        else if (nameOfAttack == "Poisoned")
        {
            enemy.enemyHealth.statusController.poisonEffect.SetActive(false);
            enemy.enemyHealth.statusController.isPoisoned = false;
        }
    }
}
