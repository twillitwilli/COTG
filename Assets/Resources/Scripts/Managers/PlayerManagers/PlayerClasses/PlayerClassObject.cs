using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStuff/CreateNewPlayerClass")]
public class PlayerClassObject : ScriptableObject
{
    public MagicController.ClassType classType;

    public float maxHealth = 100, currentHealth = 100, playerSpeed = 4.0f, sprintMultiplier = 2f, crouchSpeedReduction = 2f, jumpVelocity = 5f, dashDistance = 8, dashCooldown = 3;
    public float minAttackDamage, maxAttackDamage, attackRange = 1, attackCooldown = 6, damageUpgrades, rangeUpgrades, magicFocus = 4, elementalEffectChance = 10, luck;
    public float critChance = 10, critDamage = 0.25f, specialEffectChance = 10, aimAssist = 1;

    [Header("Dash Effects")]
    public bool dashPillar;
    public bool teleportBurst;

    [Header("Collision Effects")]
    public bool piercing;
    public bool bouncing;

    [Header("Special Effects")]
    public bool explosion;
    public bool rain, summoning, burst, pillar, aoeGround;

    [Header("Other Effects")]
    public bool controllable;

    [Header("Class Specific Changes")]
    public bool sorcererSpells;
    public bool wizardStaff, conjurerBow, mageWand, enchanterBlades;
}
