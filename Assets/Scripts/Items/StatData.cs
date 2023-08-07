using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatData
{
    public string itemName;
    public int itemIdx;

    //CLASS SELECTION
    public enum ClassType { none = 0, Wizard = 1, Conjurer = 2, Sorcerer = 3, Mage = 4, Enchanter = 5, Warlock = 6, Witch = 7, Tarot = 8 }
    public ClassType classSelection;

    //MAGIC SELECTION
    [System.Flags] public enum MagicType { arcane = 1, fire = 2, water = 4, earth = 8, dark = 16, light = 32, blood = 64, cupcakes = 128 }
    public MagicType magicSelection;

    //STATUS EFFECTS
    [System.Flags] public enum StatusEffects { none = 0, burning = 1, blinded = 2, frozen = 4, electrocuted = 8, slowed = 16, rooted = 32, lifeDraining = 64, poisoned = 128 }
    public StatusEffects statusEffectSelection;

    //DASH EFFECTS
    [System.Flags] public enum DashEffects { none = 0, dashAOETrail = 1, teleportBurst = 2, dashPillars = 4 }
    public DashEffects dashSelection;

    //COLLISION EFFECTS
    public enum CollisionEffects { none = 0, peircing = 1, bouncing = 2, split = 3 }
    public CollisionEffects collisionSelection;

    //SPECIAL EFFECTS
    [System.Flags] public enum SpecialEffects { none = 0, explosion = 1, rain = 2, summoning = 4, burst = 8, pillar = 16, AOEGround = 32 }
    public SpecialEffects specialSelection;

    //CASTING TYPE
    public enum CastingType { charge, rapidFire, beam }
    public CastingType castingSelection;
    public bool _controllabeAttack;


    [Header("Base Stats")]
    public float maxHealth, currentHealth, playerSpeed, sprintMultiplier, crouchSpeedReduction, jumpVelocity,
        dashDistance, throwingForce;

    [Header("Attack Stats")]
    public float attackDamage, attackRange, attackCooldown, magicFocus, elementalEffectChance, luck, critChance, critDamage,
        specialEffectChance, aimAssist;

    [Header("Dungeon Stats")]
    public int gold, arcaneCrystals, keyCrystals, souls;
}
