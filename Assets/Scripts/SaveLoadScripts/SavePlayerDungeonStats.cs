using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SavePlayerDungeonStats : MonoBehaviour
{
    public void SaveDungeon()
    {
        BinarySaveSystem.SaveDungeon(CreateSaveData(), LocalGameManager.Instance.player.playerSaveFile);
    }

    private PlayerDungeonData CreateSaveData()
    {
        PlayerDungeonData newData = new PlayerDungeonData();

        // Dungeon Stats

        newData.difficulty = (int)LocalGameManager.Instance.currentGameMode;
        newData.dungeonType = LocalGameManager.Instance.dungeonType;
        newData.currentLevel = LocalGameManager.Instance.currentLevel;


        // Base Stats

        newData.maxHealth = PlayerStats.Instance.data.maxHealth;
        newData.currentHealth = PlayerStats.Instance.Health;
        newData.playerSpeed = PlayerStats.Instance.data.playerSpeed;
        newData.sprintMultiplier = PlayerStats.Instance.data.sprintMultiplier;
        newData.crouchSpeedReduction = PlayerStats.Instance.data.crouchSpeedReduction;
        newData.jumpVelocity = PlayerStats.Instance.data.jumpVelocity;
        newData.dashDistance = PlayerStats.Instance.data.dashDistance;
        newData.throwingForce = PlayerStats.Instance.data.throwingForce;
        newData.iFrameTime = PlayerStats.Instance.data.iFrameTime;


        // Attack Stats

        newData.attackDamage = PlayerStats.Instance.data.attackDamage;
        newData.attackRange = PlayerStats.Instance.data.attackRange;
        newData.attackCooldown = PlayerStats.Instance.data.attackCooldown;
        newData.damageUpgrades = PlayerStats.Instance.data.damageUpgrades;
        newData.rangeUpgrades = PlayerStats.Instance.data.rangeUpgrades;
        newData.magicFocus = PlayerStats.Instance.data.magicFocus;
        newData.elementalEffectChance = PlayerStats.Instance.data.elementalEffectChance;
        newData.luck = PlayerStats.Instance.data.luck;
        newData.critChance = PlayerStats.Instance.data.critChance;
        newData.critDamage = PlayerStats.Instance.data.critDamage;
        newData.specialEffectChance = PlayerStats.Instance.data.specialEffectChance;
        newData.aimAssist = PlayerStats.Instance.data.aimAssist;


        // Gold, Bombs, Keys, Souls
        newData.currentGold = PlayerStats.Instance.data.currentGold;
        newData.currentArcaneCrystals = PlayerStats.Instance.data.currentArcaneCrystals;
        newData.currentKeys = PlayerStats.Instance.data.currentKeys;
        newData.currentSouls = PlayerStats.Instance.data.currentSouls;


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
        PlayerDungeonData loadedData = BinarySaveSystem.LoadDungeon(LocalGameManager.Instance.player.playerSaveFile);

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

        BinarySaveSystem.DeleteFile("DungeonData", LocalGameManager.Instance.player.playerSaveFile);
    }
}
