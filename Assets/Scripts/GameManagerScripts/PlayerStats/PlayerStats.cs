using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using QTArts.AbstractClasses;
using QTArts.Interfaces;

public class PlayerStats : MonoSingleton<PlayerStats>, iCooldownable, iDamagable<float>
{
    [SerializeField]
    VRPlayer _player;

    [SerializeField]
    PlayerComponents _playerComponents;

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
        keys,
        souls
    }

    public float cooldownTimer { get; set; }
    public float Health { get; set; }
    public bool godMode { get; set; }
    public bool isDead { get; set; }

    public PlayerDungeonData data { get; set; }

    public float minAttackDamage { get; private set; }
    public float maxAttackDamage { get; private set; }

    public int currentMagicFocus { get; set; }

    public bool iFrame { get; set; }

    private void Awake()
    {
        data = new PlayerDungeonData();

        DefaultStats();
    }

    private void LateUpdate()
    {
        if (iFrame && CooldownDone())
            iFrame = false;
    }

    private void DefaultStats()
    {
        // Base Stats
        data.maxHealth = 100;
        data.currentHealth = 100;
        data.playerSpeed = 4;
        data.sprintMultiplier = 2;
        data.crouchSpeedReduction = 2;
        data.jumpVelocity = 5;
        data.dashDistance = 8;
        data.throwingForce = 3.5f;
        data.iFrameTime = 0.5f;

        // Attack Stats
        data.attackDamage = 10;
        data.attackRange = 1;
        data.attackCooldown = 6;
        data.elementalEffectChance = 10;
        data.luck = 5;
        data.critChance = 10;
        data.critDamage = 0.25f;
        data.specialEffectChance = 10;
        data.aimAssist = 1;
        data.magicFocus = 4;

        DefaultCollectableStat();
    }

    public void DefaultCollectableStat()
    {
        data.currentGold = 0;
        data.currentArcaneCrystals = 0;
        data.currentKeys = 0;
        data.currentSouls = 0;
    }

    public void SetAllStats(PlayerDungeonData newStats)
    {
        data = newStats;

        SetAttackDamageRange();
    }

    public void AdjustSpecificStat(StatAdjustmentType statToAdjust, float adjustmentValue)
    {
        switch (statToAdjust)
        {
            case StatAdjustmentType.iFrameTime:

                data.iFrameTime += adjustmentValue;

                data.iFrameTime = CheckStatLimit(data.iFrameTime, 0.5f, 3);

                break;

            case StatAdjustmentType.maxHealth:

                data.maxHealth += adjustmentValue;

                data.maxHealth = CheckStatLimit(data.maxHealth, 0, 1000);

                Health = CheckStatLimit(Health, 0, data.maxHealth);

                if (Health == 0)
                    Death();

                break;

            case StatAdjustmentType.playerSpeed:

                data.playerSpeed += adjustmentValue;

                data.playerSpeed = CheckStatLimit(data.playerSpeed, 3, 10);

                break;

            case StatAdjustmentType.jumpVelocity:

                data.jumpVelocity += adjustmentValue;

                data.jumpVelocity = CheckStatLimit(data.jumpVelocity, 4, 15);

                break;

            case StatAdjustmentType.dashDistance:

                data.dashDistance += adjustmentValue;

                data.dashDistance = CheckStatLimit(data.dashDistance, 6, 15);

                break;

            case StatAdjustmentType.throwingForce:

                data.throwingForce += adjustmentValue;

                data.throwingForce = CheckStatLimit(data.throwingForce, 10, 20);

                break;

            case StatAdjustmentType.attackDamage:

                data.attackDamage += adjustmentValue;

                data.attackDamage = CheckStatLimit(data.attackDamage, 0.5f, 150);

                SetAttackDamageRange();

                break;

            case StatAdjustmentType.attackRange:

                data.attackRange += adjustmentValue;

                data.attackRange = CheckStatLimit(data.attackRange, 1, 5);

                break;

            case StatAdjustmentType.attackCooldown:

                data.attackCooldown += adjustmentValue;

                data.attackCooldown = CheckStatLimit(data.attackCooldown, 0.25f, 5);

                break;

            case StatAdjustmentType.elementalEffectChance:

                data.elementalEffectChance += adjustmentValue;

                data.elementalEffectChance = CheckStatLimit(data.elementalEffectChance, 5, 100);

                break;

            case StatAdjustmentType.luck:

                data.luck += adjustmentValue;

                data.luck = CheckStatLimit(data.luck, -10, 10);

                break;

            case StatAdjustmentType.critChance:

                data.critChance += adjustmentValue;

                data.critChance = CheckStatLimit(data.critChance, 5, 100);

                break;

            case StatAdjustmentType.critDamage:

                data.critDamage += adjustmentValue;

                data.critDamage = CheckStatLimit(data.critDamage, 10, 150);

                break;

            case StatAdjustmentType.specialEffectChance:

                data.specialEffectChance += adjustmentValue;

                data.specialEffectChance = CheckStatLimit(data.specialEffectChance, 5, 100);

                break;

            case StatAdjustmentType.aimAssist:

                data.aimAssist += adjustmentValue;

                data.aimAssist = CheckStatLimit(data.aimAssist, 0.5f, 3);

                break;

            case StatAdjustmentType.magicFocus:

                data.magicFocus += adjustmentValue;

                data.magicFocus = CheckStatLimit(data.magicFocus, 1, 20);

                break;

            case StatAdjustmentType.gold:

                data.currentGold += (int)adjustmentValue;

                data.currentGold = (int)CheckStatLimit(data.currentGold, 0, 999);

                WalletController.Instance.UpdateGoldDisplay(data.currentGold);

                break;

            case StatAdjustmentType.arcaneCrystals:

                data.currentArcaneCrystals += (int)adjustmentValue;

                data.currentArcaneCrystals = (int)CheckStatLimit(data.currentArcaneCrystals, 0, 16);

                _playerComponents.bombKeyDisplay[_player.GetOffHandIndex()].AdjustDisplay(data.currentArcaneCrystals, 16);

                break;

            case StatAdjustmentType.keys:

                data.currentKeys += (int)adjustmentValue;

                data.currentKeys = (int)CheckStatLimit(data.currentKeys, 0, 16);

                _playerComponents.bombKeyDisplay[_player.GetPrimaryHandIndex()].AdjustDisplay(data.currentKeys, 16);

                break;

            case StatAdjustmentType.souls:

                data.currentSouls += (int)adjustmentValue;

                data.currentSouls = (int)CheckStatLimit(data.currentSouls, 0, 9999999);

                break;
        }
    }

    private float CheckStatLimit(
        float currentValue, 
        float minLimit, 
        float maxLimit)
    {
        if (currentValue < minLimit)
            return minLimit;

        else if (currentValue > maxLimit)
            return maxLimit;

        else
            return currentValue;
    }

    public bool CooldownDone(bool setTimer = false, float cooldownTime = 0.5f)
    {
        if (setTimer)
            cooldownTimer = data.iFrameTime;

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        else
            return true;

        return false;
    }

    public void Damage(float damageAmount)
    {
        if (damageAmount < 0 && !godMode && !iFrame)
        {
            Health += damageAmount;

            _player.GetPlayerComponents().hitEffect.PlayerHit();

            iFrame = true;

            if (Health <= 0)
                Death();
        }

        else if (damageAmount > 0)
            Health = CheckStatLimit(Health += damageAmount, 0, data.maxHealth);

        foreach (PlayerHealthDisplay healthDisplay in _playerComponents.healthDisplay)
        {
            healthDisplay.AdjustHealthDisplay((Health / data.maxHealth) * 100);
        }
    }

    public void Death()
    {
        PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.deaths);

        isDead = true;

        if (!MultiplayerManager.Instance.coop)
        {
            PlayerTotalStats.Instance.SavePlayerProgress(LocalGameManager.Instance.saveFile);
            _playerComponents.resetPlayer.ResetPlayer(true);
        }

        else
            MultiplayerManager.Instance.GetCoopManager().PlayerDied();
    }

    public void Revive(float maxHealth, float health)
    {
        isDead = false;

        data.maxHealth = CheckStatLimit(maxHealth, 1, 100);

        Health = CheckStatLimit(health, 1, data.maxHealth);
    }

    private void SetAttackDamageRange()
    {
        minAttackDamage = data.attackDamage * 0.9f;
        maxAttackDamage = data.attackDamage * 1.1f;
    }

    public float AttackDamage() 
    { 
        float damageAmount = Random.Range(minAttackDamage, maxAttackDamage);

        if (PercentChance(data.critChance))
            damageAmount = (damageAmount * data.critDamage) + damageAmount;

        return damageAmount;
    }

    public bool SpecialAttack()
    {
        bool triggerSpecial = PercentChance(data.specialEffectChance);

        return triggerSpecial;
    }

    public bool ElementalEffect()
    {
        bool triggerElementalEffect = PercentChance(data.elementalEffectChance);

        return triggerElementalEffect;
    }

    bool PercentChance(float chance)
    {
        bool percentChance = chance < Random.Range(0, 100) ? true : false;

        return percentChance;
    }

    public async Task LoadStats(PlayerDungeonData loadedData)
    {
        // Base Stats
        data.maxHealth = loadedData.maxHealth;
        Health = loadedData.currentHealth;
        data.playerSpeed = loadedData.playerSpeed;
        data.sprintMultiplier = loadedData.sprintMultiplier;
        data.crouchSpeedReduction = loadedData.crouchSpeedReduction;
        data.jumpVelocity = loadedData.jumpVelocity;
        data.dashDistance = loadedData.dashDistance;
        data.iFrameTime = loadedData.iFrameTime;


        // Attack Stats
        data.attackDamage = loadedData.attackDamage;
        data.attackRange = loadedData.attackRange;
        data.attackCooldown = loadedData.attackCooldown;
        data.magicFocus = loadedData.magicFocus;
        data.elementalEffectChance = loadedData.elementalEffectChance;
        data.luck = loadedData.luck;
        data.critChance = loadedData.critChance;
        data.critDamage = loadedData.critDamage;
        data.specialEffectChance = loadedData.specialEffectChance;
        data.aimAssist = loadedData.aimAssist;


        // Gold, Bombs, Keys, Souls
        data.currentGold = loadedData.currentGold;
        data.currentArcaneCrystals = loadedData.currentArcaneCrystals;
        data.currentKeys = loadedData.currentKeys;
        data.currentSouls = loadedData.currentSouls;
    }
}
