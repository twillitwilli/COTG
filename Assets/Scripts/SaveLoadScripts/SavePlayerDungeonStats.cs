using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SavePlayerDungeonStats : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;
    private MagicController _magicController;

    private void Start()
    {
        _gameManager = LocalGameManager.Instance;
        _playerStats = _gameManager.GetPlayerStats();
        _magicController = _gameManager.GetMagicController();
    }

    public void SaveDungeon()
    {
        BinarySaveSystem.SaveDungeon(CreateSaveData(), _playerStats.saveFile);
    }

    private PlayerDungeonData CreateSaveData()
    {
        PlayerDungeonData newData = new PlayerDungeonData();

        // Dungeon Stats

        newData.difficulty = (int)_gameManager.currentGameMode;
        newData.dungeonType = _gameManager.dungeonType;
        newData.currentLevel = _gameManager.currentLevel;


        // Base Stats

        newData.maxHealth = _playerStats.GetMaxHealth();
        newData.currentHealth = _playerStats.GetCurrentHealth();
        newData.playerSpeed = _playerStats.GetPlayerSpeed();
        newData.sprintMultiplier = _playerStats.GetSprintMultiplier();
        newData.crouchSpeedReduction = _playerStats.GetCrouchSpeedReduction();
        newData.jumpVelocity = _playerStats.GetJumpVelocity();
        newData.dashDistance = _playerStats.GetDashDistance();
        newData.throwingForce = _playerStats.GetThrowingForce();


        // Attack Stats

        newData.attackDamage = _playerStats.GetAttackDamage();
        newData.minAttackDamage = _playerStats.GetMinAttackDamage();
        newData.maxAttackDamage = _playerStats.GetMaxAttackDamage();
        newData.attackRange = _playerStats.GetAttackRange();
        newData.attackCooldown = _playerStats.GetAttackCooldown();
        newData.damageUpgrades = _playerStats.GetDamageUpgrades();
        newData.rangeUpgrades = _playerStats.GetRangeUpgrades();
        newData.magicFocus = _playerStats.GetMagicFocus();
        newData.elementalEffectChance = _playerStats.GetElementalEffectChance();
        newData.luck = _playerStats.GetLuck();
        newData.critChance = _playerStats.GetCritChance();
        newData.critDamage = _playerStats.GetCritDamage();
        newData.specialEffectChance = _playerStats.GetSpecialChance();
        newData.aimAssist = _playerStats.GetAimAssist();


        // Gold, Bombs, Keys, Souls
        newData.currentGold = _playerStats.GetCurrentGold();
        newData.currentArcaneCrystals = _playerStats.GetCurrentArcaneCrystals();
        newData.currentKeys = _playerStats.GetCurrentKeys();
        newData.currentSouls = _playerStats.GetCurrentSouls();


        // Class & Magic Stats 
        newData.playerClass = (int)_magicController.currentClass;
        newData.magicType = (int)_magicController.currentMagic;
        newData.statusEffect = (int)_magicController.currentStatusEffect;
        newData.dashEffect = (int)_magicController.currentDashEffects;
        newData.collisionEffect = (int)_magicController.currentCollisionEffects;
        newData.specialEffect = (int)_magicController.currentCastingType;


        return newData;
    }

    public async void LoadDungeon()
    {
        PlayerDungeonData loadedData = BinarySaveSystem.LoadDungeon(_playerStats.saveFile);

        switch (loadedData.difficulty)
        {
            // Tutorial
            case 1:
                _gameManager.currentGameMode = LocalGameManager.GameMode.tutorial;
                break;

            // Normal
            case 2:
                _gameManager.currentGameMode = LocalGameManager.GameMode.normal;
                break;

            // Master
            case 3:
                _gameManager.currentGameMode = LocalGameManager.GameMode.master;
                break;
        }

        _gameManager.dungeonType = loadedData.dungeonType;
        _gameManager.currentLevel = loadedData.currentLevel;

        await _playerStats.LoadStats(loadedData);

        await _magicController.LoadSavedDungeonMagicStats(loadedData);

        BinarySaveSystem.DeleteFile("DungeonData", _playerStats.saveFile);
    }
}
