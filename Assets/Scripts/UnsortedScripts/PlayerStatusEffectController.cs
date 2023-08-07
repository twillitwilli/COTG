using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffectController : MonoBehaviour
{
    [SerializeField] private VRPlayerController player;

    public enum StatusEffect { none, burning, blinded, frozen, electrocuted, slowed, rooted, lifeDraining, poisoned }
    [HideInInspector] public StatusEffect currentEffectOnPlayer;
    [HideInInspector] public bool canBurn, canBlind, canFreeze, canElectrocute, canSlow, canRoot, canLifeSteal, canPoison;

    public void ResetAllStatusEffects()
    {
        currentEffectOnPlayer = StatusEffect.none;
        canBurn = false;
        canBlind = false;
        canFreeze = false;
        canElectrocute = false;
        canSlow = false;
        canRoot = false;
        canLifeSteal = false;
        canPoison = false;
    }

    public void EffectedPlayer(StatusEffect statusEffectToPlayer)
    {
        if (currentEffectOnPlayer == StatusEffect.none)
        {
            currentEffectOnPlayer = statusEffectToPlayer;
            switch (currentEffectOnPlayer)
            {
                case StatusEffect.burning:
                    PlayerDamageOverTime("Burning");
                    break;
                case StatusEffect.blinded:
                    break;
                case StatusEffect.frozen:
                    break;
                case StatusEffect.electrocuted:
                    PlayerDamageOverTime("Electrocuted");
                    break;
                case StatusEffect.slowed:
                    break;
                case StatusEffect.rooted:
                    break;
                case StatusEffect.lifeDraining:
                    PlayerDamageOverTime("Life Drained");
                    break;
                case StatusEffect.poisoned:
                    PlayerDamageOverTime("Poisoning");
                    break;
            }
        }
    }

    private void PlayerDamageOverTime(string effectName)
    {
        GameObject damageNode = Instantiate(MasterManager.playerMagicController.damageOverTimeNode);
        DamageOverTimeNode damageComponent = damageNode.GetComponent<DamageOverTimeNode>();
        damageComponent.forPlayer = true;
        damageComponent.player = player;
        damageComponent.nameOfAttack = effectName;
        damageComponent.howLong = Random.Range(3, 8);
        damageComponent.damage = Random.Range(0.5f, 3.5f);
        damageComponent.cooldownController.cooldownTimer = Random.Range(0.5f, 1);
    }

    public void EffectOnEnemy(EnemyStatusController enemyStatusController)
    {
        int effect = Random.Range(0, 8);
        if (effect == 0)
        {
            if (canBurn) { enemyStatusController.Burning(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
        else if (effect == 1)
        {
            if (canBlind) { enemyStatusController.Blinded(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
        else if (effect == 2)
        {
            if (canFreeze) { enemyStatusController.Frozen(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
        else if (effect == 3)
        {
            if (canElectrocute) { enemyStatusController.Electrocuted(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
        else if (effect == 4)
        {
            if (canSlow) { enemyStatusController.Slowed(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
        else if (effect == 5)
        {
            if (canRoot) { enemyStatusController.Rooted(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
        else if (effect == 6)
        {
            if (canLifeSteal) { enemyStatusController.LifeDrained(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
        else if (effect == 7)
        {
            if (canPoison) { enemyStatusController.Poisoned(); }
            else { EffectOnEnemy(enemyStatusController); }
        }
    }
}
