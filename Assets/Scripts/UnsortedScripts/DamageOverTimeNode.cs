using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeNode : Cooldown
{
    [HideInInspector] public bool forPlayer;
    [HideInInspector] public VRPlayerController player;
    [HideInInspector] public EnemyController enemy;
    [HideInInspector] public string nameOfAttack;
    [HideInInspector] public float howLong;
    [HideInInspector] public float damage;

    private PlayerStats _playerStats;
    private MagicController _magicController;

    public void Start()
    {
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
        _magicController = LocalGameManager.Instance.GetMagicController();

        Destroy(gameObject, howLong);
    }

    private void LateUpdate()
    {
        if (!CooldownCompleted())
        {
            if (forPlayer)
            {
                _playerStats.AdjustHealth(-damage, nameOfAttack);

                CooldownCompleted(0.5f, true);
            }
            else
            {
                enemy.enemyHealth.AdjustHealth(-damage, false);

                CooldownCompleted(0.5f, true);
            }
        }
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
