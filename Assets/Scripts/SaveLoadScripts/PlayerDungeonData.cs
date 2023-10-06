[System.Serializable]
public class PlayerDungeonData
{
    // Dungeon Stats
    public int 
        difficulty, 
        dungeonType, 
        currentLevel;

    // Base Stats
    public float
        maxHealth,
        currentHealth,
        playerSpeed,
        sprintMultiplier,
        crouchSpeedReduction,
        jumpVelocity,
        dashDistance,
        throwingForce,
        iFrameTime;

    //Attack Stats
    public float 
        attackDamage, 
        attackRange, 
        attackCooldown, 
        damageUpgrades, 
        rangeUpgrades,
        magicFocus, 
        elementalEffectChance, 
        luck, 
        critChance, 
        critDamage, 
        specialEffectChance, 
        aimAssist;

    //Gold, Bombs, Keys, Souls
    public int 
        currentGold,
        currentArcaneCrystals,
        currentKeys,
        currentSouls;

    // Class & Magic Stats
    public int 
        playerClass, 
        magicType, 
        statusEffect, 
        dashEffect, 
        collisionEffect, 
        specialEffect, 
        castingType;
}
