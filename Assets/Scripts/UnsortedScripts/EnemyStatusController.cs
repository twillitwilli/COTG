using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusController : MonoBehaviour
{
    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public EnemyController enemyController;
    [HideInInspector] public bool isKnockedBack, isBurning, isBlinded, isFrozen, isElectrocuted, isSlowed, isRooted, isLifeDraining, isPoisoned;
    public float knockback;
    public bool knockbackImmunity, burnImmunity, blindImmunnity, electrocudedImmunity, frozenImmunity, slowedImmunity, rootedImmunity, lifeDrainImmunity, poisonImmunity;
    public GameObject burnEffect, blindedEffect, electrocutedEffect, frozenEffect, slowedEffect, rootedEffect, lifeDrainEffect, poisonEffect;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyController = enemyHealth.enemyController;
    }

    public void TookDamage()
    {
        if (!enemyHealth.isBoss && !knockbackImmunity)
        {
            Debug.Log("enemy knocked back");
            isKnockedBack = true;
        }
    }

    public void Burning()
    {
        if (!burnImmunity && !isBurning)
        {
            Debug.Log("enemy burning");
            isBurning = true;
            burnEffect.SetActive(true);
            EnemyDamageOverTime("Burning");
        }
    }

    public void Blinded()
    {
        if (!blindImmunnity && !isBlinded)
        {
            Debug.Log("enemy blinded");
            isBlinded = true;
            blindedEffect.SetActive(true);
        }
    }

    public void Frozen()
    {
        if (!frozenImmunity && !isFrozen)
        {
            Debug.Log("enemy frozen");
            isFrozen = true;
            frozenEffect.SetActive(true);
        }
    }

    public void Electrocuted()
    {
        if (!electrocudedImmunity && !isElectrocuted)
        {
            Debug.Log("enemy electrocuted");
            isElectrocuted = true;
            EnemyDamageOverTime("Eletrocuted");
            electrocutedEffect.SetActive(true);
        }
    }

    public void Slowed()
    {
        if (!slowedImmunity && !isSlowed)
        {
            Debug.Log("enemy slowed");
            isSlowed = true;
            slowedEffect.SetActive(true);
        }
    }

    public void Rooted()
    {
        if (!rootedImmunity && !isRooted)
        {
            Debug.Log("enemy rooted");
            isRooted = true;
            rootedEffect.SetActive(true);
        }
    }

    public void LifeDrained()
    {
        if (!lifeDrainImmunity && !isLifeDraining)
        {
            Debug.Log("enemy life drained");
            isLifeDraining = true;
            EnemyDamageOverTime("Life Drained");
            lifeDrainEffect.SetActive(true);
        }
    }

    public void Poisoned()
    {
        if (!poisonImmunity && !isPoisoned)
        {
            Debug.Log("enemy poisoned");
            isPoisoned = true;
            EnemyDamageOverTime("Poisoned");
            poisonEffect.SetActive(true);
        }
    }

    private void EnemyDamageOverTime(string effectName)
    {
        GameObject damageNode = Instantiate(MasterManager.playerMagicController.damageOverTimeNode);
        DamageOverTimeNode damageComponent = damageNode.GetComponent<DamageOverTimeNode>();
        damageComponent.forPlayer = true;
        damageComponent.enemy = enemyController;
        damageComponent.nameOfAttack = effectName;
        damageComponent.howLong = Random.Range(3, 8);
        damageComponent.damage = Random.Range(3.5f, 8.5f);
    }
}
