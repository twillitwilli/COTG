using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerTotalStats _playerTotalStats;
    [SerializeField] private MagicController _magicController;
    [SerializeField] private GearController _gearController;

    private VRPlayerController _player;
    private PlayerComponents _playerComponents;

    public float flightTesting;
    public AnimationCurve flightVelocity;
    private float _cooldownTimer;
    private bool _iFrame, _setCoolDown, _isDead;

    // Base Stats
    private float _maxHealth = 100, _currentHealth = 100, _playerSpeed = 4.0f, _sprintMultiplier = 2f, _crouchSpeedReduction = 2f, 
        _jumpVelocity = 5f, _dashDistance = 8, _throwingForce = 3.5f;

    //Attack Stats
    private float _attackDamage, _minAttackDamage = 12, _maxAttackDamage = 16, _attackRange = 1, _attackCooldown = 6, _damageUpgrades, _rangeUpgrades, 
        _magicFocus = 4, _elementalEffectChance = 10, _luck = 5, _critChance = 10, _critDamage = 0.25f, _specialEffectChance = 10, 
        _aimAssist = 1, _currentMagicFocus;

    //Gold
    private int _maxGold = 999, _currentGold;
    [SerializeField] private CurrentGoldDisplay _goldDisplay;

    //Arcane Crystals
    private int _maxArcaneCrystals = 16, _currentArcaneCrystals;

    //Keys
    private int _maxKeys = 16, _currentKeys;

    //Souls
    private int _currentSouls;

    //Save References
    public int saveFile { get; private set; }

    private void Awake()
    {
        LocalGameManager.playerCreated += SetNewPlayer;
    }

    private void LateUpdate()
    {
        if (_iFrame && IFrameCooldown()) { _iFrame = false; }
    }

    public void SetSaveFileIndex(int file) { saveFile = file;  }

    public void SetNewPlayer(VRPlayerController player)
    {
        _player = player;
        _playerComponents = _player.GetPlayerComponents();
    }

    public float GetPlayerSpeed() { return _playerSpeed; }
    public float GetSprintMultiplier() { return _sprintMultiplier; }
    public float GetCrouchSpeedReduction() { return _crouchSpeedReduction; }
    public float GetJumpVelocity() { return _jumpVelocity; }
    public float GetDashDistance() { return _dashDistance; }
    public float GetThrowingForce() { return _throwingForce; }

    public void AdjustPlayerSpeed(float adjustmentValue)
    {
        _playerSpeed += adjustmentValue;
    }

    public void StartIFrame()
    {
        _iFrame = true;
        _cooldownTimer = 0.5f;
    }

    private bool IFrameCooldown()
    {
        if (_cooldownTimer > 0) { _cooldownTimer -= Time.deltaTime; }
        if (_cooldownTimer <= 0)
        {
            _cooldownTimer = 0;
            return true;
        }
        return false;
    }

    public bool iFrame { get; private set; }

    public float GetMaxHealth() { return _maxHealth; }

    public void AdjustMaxHealth(float adjustmentValue)
    {
        _maxHealth += adjustmentValue;
        if (_currentHealth > _maxHealth) { AdjustHealth(-0.1f, "Sold Your Soul For Death"); }
    }

    public void AdjustHealth(float adjustmentValue, string deathMessage)
    {
        if (!_player.godMode && !_iFrame)
        {
            if (adjustmentValue < 0) 
            { 
                _player.GetPlayerComponents().hitEffect.PlayerHit();
                StartIFrame();
            }
            else { _playerComponents.hitEffect.CheckVision(); }

            _currentHealth += adjustmentValue;

            if (_currentHealth <= 0) { PlayerDead(deathMessage); }
            else if (_currentHealth >= _maxHealth) { _currentHealth = _maxHealth; }

            foreach (PlayerHealthDisplay healthDisplay in _playerComponents.healthDisplay) { healthDisplay.AdjustHealthDisplay((_currentHealth / _maxHealth) * 100); }
        }
    }

    public float GetCurrentHealth() { return _currentHealth; }

    public bool IsPlayerDead() { return _isDead; }

    public void PlayerDead(string deathMessage)
    {
        _playerTotalStats.AdjustStats(PlayerTotalStats.StatType.deaths);
        _playerComponents.onScreenText.PrintText(deathMessage, true);
        _isDead = true;

        if (CoopManager.instance == null) 
        {
            _playerTotalStats.SavePlayerProgress(saveFile);
            _playerComponents.resetPlayer.ResetPlayer(true); 
        }
        else { CoopManager.instance.PlayerDied(); }
    }

    public void AdjustAttackDamage(float adjustmentValue)
    {
        _attackDamage += adjustmentValue;
        _minAttackDamage = _attackDamage - 3;
        _maxAttackDamage = _attackDamage + 3; 

        if (adjustmentValue > 0) { AdjustDamageUpgrades(1); }
        else { AdjustDamageUpgrades(-1); }

        if (_minAttackDamage <= 1f) { _minAttackDamage = 1f; }
        if (_maxAttackDamage <= 4f) { _maxAttackDamage = 4f; }
    }

    public float GetAttackDamage() { return _attackDamage; }

    public float AttackDamage() { return Random.Range(_minAttackDamage, _maxAttackDamage); }

    public float GetMinAttackDamage() { return _minAttackDamage; }
    public float GetMaxAttackDamage() { return _maxAttackDamage; }

    public void AdjustAttackRange(float adjustmentValue)
    {
        _attackRange += adjustmentValue;
        if (adjustmentValue > 0) { AdjustRangeUprades(1); }
        else { AdjustRangeUprades(-1); }
        if (_attackRange <= 0.01f) { _attackRange = 0.01f; }
    }

    public float GetAttackRange() { return _attackRange; }

    public void AdjustAttackCooldown(float adjustmentValue)
    {
        _attackCooldown += adjustmentValue;
        if (_attackCooldown <= 0.01f) { _attackCooldown = 0.01f; }
    }

    public float GetAttackCooldown() { return _attackCooldown; }

    public void AdjustDamageUpgrades(float adjustmentValue)
    {
        _damageUpgrades += adjustmentValue;
        if (_damageUpgrades < 0) { _damageUpgrades = 0; }
    }

    public float GetDamageUpgrades() { return _damageUpgrades; }

    public void AdjustRangeUprades(float adjustmentValue)
    {
        _rangeUpgrades += adjustmentValue;
        if (_rangeUpgrades < 0) { _rangeUpgrades = 0; }
    }

    public float GetRangeUpgrades() { return _rangeUpgrades; }
 
    public void AdjustMagicFocus(int adjustmentValue)
    {
        _magicFocus += adjustmentValue;
        if (_magicFocus <= 0) { _magicFocus = 1; }
        else if (_magicFocus >= 20) { _magicFocus = 20; }

        switch (_magicController.currentClass)
        {
            case MagicController.ClassType.Wizard:
                _gearController.GetStaffController().GetPlayerStaff().AdjustMagicFocus();
                break;
        }
        
        if (_playerComponents.minionSpawnLocation.currentMinion != null)
        {
            _playerComponents.minionSpawnLocation.CheckMinionStage();
        }
    }

    public int GetMagicFocus() { return Mathf.RoundToInt(_magicFocus); }

    public void AdjustCurrentMagicFocus(int adjustmentValue)
    {
        _currentMagicFocus += adjustmentValue;
        if (_currentMagicFocus > _magicFocus) { _currentMagicFocus = _magicFocus; }
    }

    public int GetCurrentMagicFocus() { return Mathf.RoundToInt(_currentMagicFocus); }

    public void AdjustElementalEffectChance(float adjustmentValue)
    {
        _elementalEffectChance += adjustmentValue;
        if (_elementalEffectChance < 0) { _elementalEffectChance = 0; }
    }

    public float GetElementalEffectChance() { return _elementalEffectChance; }

    public void AdjustLuck(float adjustmentValue)
    {
        _luck += adjustmentValue;
        if (_luck < 0) { _luck = 0; }
    }

    public int GetLuck() { return Mathf.RoundToInt(_luck); }

    public void AdjustCritChance(float adjustmentValue)
    {
        _critChance += adjustmentValue;
        if (_critChance < 1) { _critChance = 1; }
    }

    public float GetCritChance() { return _critChance; }

    public void AdjustCritDamage(float adjustmentValue)
    {
        _critDamage += adjustmentValue;
        if (_critDamage < 0.1) { _critDamage = 0.1f; }
    }

    public float GetCritDamage() { return _critDamage; }

    public void AdjustSpecialEffectChance(float adjustmentValue)
    {
        _specialEffectChance += adjustmentValue;
        if (_specialEffectChance < 1) { _specialEffectChance = 1; }
    }

    public float GetSpecialChance() { return _specialEffectChance; }

    public void AdjustAimAssist(float adjustmentValue)
    {
        _aimAssist += adjustmentValue;
        if (_aimAssist < 0) { _aimAssist = 0; }
    }

    public float GetAimAssist() { return _aimAssist; }

    public bool HitCrit()
    {
        int critHit = Random.Range(0, 100);
        if (critHit < _critChance) { return true; }
        else return false;
    }

    public bool SpecialAttack()
    {
        int specialHit = Random.Range(0, 100);

        if (specialHit < _specialEffectChance) { return true; }
        else return false;
    }

    public void AdjustGoldAmount(int gold)
    {
        _currentGold += gold;

        if (_currentGold < 0) { _currentGold = 0; }
        else if (_currentGold > _maxGold) { _currentGold = _maxGold; }

        if (_goldDisplay != null) { _goldDisplay.UpdateDisplay(_currentGold); }
    }

    public int GetCurrentGold() { return _currentGold; }

    public void AdjustArcaneCrystalAmount(int changeArcaneCrystalValue)
    {
        _currentArcaneCrystals += changeArcaneCrystalValue;

        if (_currentArcaneCrystals < 0) { _currentArcaneCrystals = 0; }
        else if (_currentArcaneCrystals > _maxArcaneCrystals) { _currentArcaneCrystals = _maxArcaneCrystals; }

        _playerComponents.bombKeyDisplay[_player.GetOffHandIndex()].AdjustDisplay(_currentArcaneCrystals, _maxArcaneCrystals);
    }

    public int GetCurrentArcaneCrystals() { return _currentArcaneCrystals; }

    public void AdjustKeyAmount(int changeKeyAmount)
    {
        _currentKeys += changeKeyAmount;

        if (_currentKeys < 0) { _currentKeys = 0; }
        else if (_currentKeys > _maxKeys) { _currentKeys = _maxKeys; }

        _playerComponents.bombKeyDisplay[_player.GetPrimaryHandIndex()].AdjustDisplay(_currentKeys, _maxKeys);
    }

    public int GetCurrentKeys() { return _currentKeys; }

    public void AdjustSoulAmount(int adjustmentValue)
    {
        _currentSouls += adjustmentValue;
        if (_currentSouls < 0) { _currentSouls = 0; }
    }

    public int GetCurrentSouls() { return _currentSouls; }

    public async Task LoadStats(PlayerDungeonData loadedData)
    {
        // Base Stats
        _maxHealth = loadedData.maxHealth;
        _currentHealth = loadedData.currentHealth;
        _playerSpeed = loadedData.playerSpeed;
        _sprintMultiplier = loadedData.sprintMultiplier;
        _crouchSpeedReduction = loadedData.crouchSpeedReduction;
        _jumpVelocity = loadedData.jumpVelocity;
        _dashDistance = loadedData.dashDistance;


        // Attack Stats
        _attackDamage = loadedData.attackDamage;
        _minAttackDamage = loadedData.minAttackDamage;
        _maxAttackDamage = loadedData.maxAttackDamage;
        _attackRange = loadedData.attackRange;
        _attackCooldown = loadedData.attackCooldown;
        _damageUpgrades = loadedData.damageUpgrades;
        _rangeUpgrades = loadedData.rangeUpgrades;
        _magicFocus = loadedData.magicFocus;
        _elementalEffectChance = loadedData.elementalEffectChance;
        _luck = loadedData.luck;
        _critChance = loadedData.critChance;
        _critDamage = loadedData.critDamage;
        _specialEffectChance = loadedData.specialEffectChance;
        _aimAssist = loadedData.aimAssist;


        // Gold, Bombs, Keys, Souls
        _currentGold = loadedData.currentGold;
        _currentArcaneCrystals = loadedData.currentArcaneCrystals;
        _currentKeys = loadedData.currentKeys;
        _currentSouls = loadedData.currentSouls;
    }
}
