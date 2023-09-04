using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SavePlayerDungeonStats : MonoBehaviour
{
    public void SaveDungeon()
    {
        BinarySaveSystem.SaveDungeon(CreateSaveData(), PlayerStats.Instance.saveFile);
    }

    private PlayerDungeonData CreateSaveData()
    {
        PlayerDungeonData newData = new PlayerDungeonData();

        // Dungeon Stats

        newData.difficulty = (int)LocalGameManager.Instance.currentGameMode;
        newData.dungeonType = LocalGameManager.Instance.dungeonType;
        newData.currentLevel = LocalGameManager.Instance.currentLevel;


        // Base Stats

        newData.maxHealth = PlayerStats.Instance.GetMaxHealth();
        newData.currentHealth = PlayerStats.Instance.GetCurrentHealth();
        newData.playerSpeed = PlayerStats.Instance.GetPlayerSpeed();
        newData.sprintMultiplier = PlayerStats.Instance.GetSprintMultiplier();
        newData.crouchSpeedReduction = PlayerStats.Instance.GetCrouchSpeedReduction();
        newData.jumpVelocity = PlayerStats.Instance.GetJumpVelocity();
        newData.dashDistance = PlayerStats.Instance.GetDashDistance();
        newData.throwingForce = PlayerStats.Instance.GetThrowingForce();


        // Attack Stats

        newData.attackDamage = PlayerStats.Instance.GetAttackDamage();
        newData.minAttackDamage = PlayerStats.Instance.GetMinAttackDamage();
        newData.maxAttackDamage = PlayerStats.Instance.GetMaxAttackDamage();
        newData.attackRange = PlayerStats.Instance.GetAttackRange();
        newData.attackCooldown = PlayerStats.Instance.GetAttackCooldown();
        newData.damageUpgrades = PlayerStats.Instance.GetDamageUpgrades();
        newData.rangeUpgrades = PlayerStats.Instance.GetRangeUpgrades();
        newData.magicFocus = PlayerStats.Instance.GetMagicFocus();
        newData.elementalEffectChance = PlayerStats.Instance.GetElementalEffectChance();
        newData.luck = PlayerStats.Instance.GetLuck();
        newData.critChance = PlayerStats.Instance.GetCritChance();
        newData.critDamage = PlayerStats.Instance.GetCritDamage();
        newData.specialEffectChance = PlayerStats.Instance.GetSpecialChance();
        newData.aimAssist = PlayerStats.Instance.GetAimAssist();


        // Gold, Bombs, Keys, Souls
        newData.currentGold = PlayerStats.Instance.GetCurrentGold();
        newData.currentArcaneCrystals = PlayerStats.Instance.GetCurrentArcaneCrystals();
        newData.currentKeys = PlayerStats.Instance.GetCurrentKeys();
        newData.currentSouls = PlayerStats.Instance.GetCurrentSouls();


        // Class & Magic Stats 
        newData.playerClass = (int)MagicController.Instance.currentClass;
        newData.magicType = (int)MagicController.Instance.currentMagic;
        newData.statusEffect = (int)MagicController.Instance.currentStatusEffect;
        newData.dashEffect = (int)MagicController.Instance.currentDashEffects;
        newData.collisionEffect = (int)MagicController.Instance.currentCollisionEffects;
        newData.specialEffect = (int)MagicController.Instance.currentCastingType;


        return newData;
    }

    public async void LoadDungeon()
    {
        PlayerDungeonData loadedData = BinarySaveSystem.LoadDungeon(PlayerStats.Instance.saveFile);

        switch (loadedData.difficulty)
        {
            // Tutorial
            case 1:
                LocalGameManager.Instance.currentGameMode = LocalGameManager.GameMode.tutorial;
                break;

            // Normal
            case 2:
                LocalGameManager.Instance.currentGameMode = LocalGameManager.GameMode.normal;
                break;

            // Master
            case 3:
                LocalGameManager.Instance.currentGameMode = LocalGameManager.GameMode.master;
                break;
        }

        LocalGameManager.Instance.dungeonType = loadedData.dungeonType;
        LocalGameManager.Instance.currentLevel = loadedData.currentLevel;

        await PlayerStats.Instance.LoadStats(loadedData);

        await MagicController.Instance.LoadSavedDungeonMagicStats(loadedData);

        BinarySaveSystem.DeleteFile("DungeonData", PlayerStats.Instance.saveFile);
    }
}
