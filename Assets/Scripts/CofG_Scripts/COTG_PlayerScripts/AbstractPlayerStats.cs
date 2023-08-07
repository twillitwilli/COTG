using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayerStats : MonoBehaviour
{
    // Base Stats
    public float maxHealth, currentHealth, playerSpeed, sprintMultiplier, _crouchSpeedReduction,
        jumpVelocity, dashDistance, dashCooldown, throwingForce;

    //Attack Stats
    public float minAttackDamage, maxAttackDamage, attackRange, attackCooldown, magicFocus, 
        elementalEffectChance, luck, critChance, critDamage, specialEffectChance,aimAssist;
}
