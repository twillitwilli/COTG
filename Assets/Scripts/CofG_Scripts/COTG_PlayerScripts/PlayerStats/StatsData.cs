using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    public enum StatAdjustmentType
    {
        iFrameTime,
        maxHealth,
        playerSpeed,
        jumpVelocity,
        dashDistance,
        throwingForce,
        attackDamage,
        attackRange,
        attackCooldown,
        elementalEffectChance,
        luck,
        critChance,
        critDamage,
        specialEffectChance,
        aimAssist,
        magicFocus,
        gold,
        arcaneCrystals,
        keys
    }

    public float flightTesting;
    public AnimationCurve flightVelocity;

    public float iFrameTime;

    public bool 
        iFrame, 
        setIFrameCoolDown, 
        isDead;

    // Base Stats
    public float 
        maxHealth = 100, 
        playerSpeed = 4.0f, 
        sprintSpeed = 2f, 
        crouchSpeed = 2f,
        jumpVelocity = 5f, 
        dashDistance = 8, 
        throwingForce = 3.5f;

    //Attack Stats
    public float
        attackDamage,
        attackRange = 1,
        attackCooldown = 6,
        elementalEffectChance = 10,
        luck = 5,
        critChance = 10,
        critDamage = 0.25f,
        specialEffectChance = 10,
        aimAssist = 1,
        magicFocus = 4;
}
