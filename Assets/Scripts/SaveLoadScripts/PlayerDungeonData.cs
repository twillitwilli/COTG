[System.Serializable]
public class PlayerDungeonData
{
    // Dungeon Stats
    public int difficulty, dungeonType, currentLevel;

    // Base Stats
    public float maxHealth, currentHealth, playerSpeed, sprintMultiplier, crouchSpeedReduction, jumpVelocity, dashDistance, throwingForce;

    //Attack Stats
    public float attackDamage, minAttackDamage, maxAttackDamage, attackRange, attackCooldown, damageUpgrades, rangeUpgrades,
        magicFocus, elementalEffectChance, luck, critChance, critDamage, specialEffectChance, aimAssist;

    //Gold, Bombs, Keys, Souls
    public int currentGold;
    public int currentArcaneCrystals;
    public int currentKeys;
    public int currentSouls;

    // Class & Magic Stats
    public int playerClass, magicType, statusEffect, dashEffect, collisionEffect, specialEffect, castingType;
}
