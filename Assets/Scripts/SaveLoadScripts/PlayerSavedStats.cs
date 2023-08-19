using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSavedStats
{
    public float maxHealth, currentHealth, speed, sprint, crouchReduction, jump, dash, throwingForce, attackDmg, attRange, 
        attCooldown, dmgUpgrades, rangeUpgrades, magicFocus, elementalChance, luck, critChance, critDamage, specialEffectChance, 
        aimAssist;

    public int currentGold, currentArcaneCrystals, currentKeys, currentSouls;

    public PlayerSavedStats(PlayerStats playerStat)
    { 
        speed = playerStat.GetPlayerSpeed();
        sprint = playerStat.GetSprintMultiplier();
        crouchReduction = playerStat.GetCrouchSpeedReduction();
        jump = playerStat.GetJumpVelocity();
        dash = playerStat.GetDashDistance();
        throwingForce = playerStat.GetThrowingForce();

        maxHealth = playerStat.GetMaxHealth();
        currentHealth = playerStat.GetCurrentHealth();

        attackDmg = playerStat.GetAttackDamage();
        attRange = playerStat.GetAttackRange();
        attCooldown = playerStat.GetAttackCooldown();
        dmgUpgrades = playerStat.GetDamageUpgrades();
        rangeUpgrades = playerStat.GetRangeUpgrades();

        magicFocus = playerStat.GetMagicFocus();
        elementalChance = playerStat.GetElementalEffectChance();

        luck = playerStat.GetLuck();

        critChance = playerStat.GetCritChance();
        critDamage = playerStat.GetCritDamage();

        specialEffectChance = playerStat.GetSpecialChance();

        aimAssist = playerStat.GetAimAssist();

        currentGold = playerStat.GetCurrentGold();

        currentArcaneCrystals = playerStat.GetCurrentArcaneCrystals();

        currentKeys = playerStat.GetCurrentKeys();

        currentSouls = playerStat.GetCurrentSouls();
    }
}
