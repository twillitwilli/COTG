using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemScrollData
{
    public string itemName;
    public int itemIdx;

    [Header("Base Stats")]
    public float maxHealth, currentHealth, playerSpeed, sprintMultiplier, crouchSpeedReduction, jumpVelocity, 
        dashDistance, throwingForce;

    [Header("Attack Stats")]
    public float attackDamage, attackRange, attackCooldown, magicFocus, elementalEffectChance, luck, critChance, critDamage, 
        specialEffectChance, aimAssist;

    [Header("Collision Effect")]
    public bool piercing, bouncing, split;

    [Header("Dash Effect")]
    public bool dashPillar, teleportBurst;

    [Header("Special Effect")]
    public bool explosion, rain, summoning, burst, pillar, aoeGround;

    [Header("Casting Type")]
    public bool chargedAttack, controllable, rapidFire, beam;
}
