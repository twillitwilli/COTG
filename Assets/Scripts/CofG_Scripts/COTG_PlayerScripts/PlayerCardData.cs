using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCardData
{
    public string className;
    public int classIdx;

    public MagicController.ClassType classType;
    public MagicController.MagicType magicType;
    public MagicController.DashEffects dashEffects;
    public MagicController.CollisionEffects collisionEffect;
    public MagicController.SpecialEffects specialEffect;
    public MagicController.CastingType castingType;

    [Header("Base Stats")]
    public float maxHealth, currentHealth, playerSpeed, sprintMultiplier, crouchSpeedReduction, jumpVelocity,
    dashDistance, throwingForce;

    [Header("Attack Stats")]
    public float attackDamage, attackRange, attackCooldown, magicFocus, elementalEffectChance, luck, critChance, critDamage,
        specialEffectChance, aimAssist;

    [Header("Other Effects")]
    public bool controllable;
}
