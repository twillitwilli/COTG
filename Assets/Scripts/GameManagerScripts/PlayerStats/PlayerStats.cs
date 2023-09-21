using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using QTArts.AbstractClasses;
using QTArts.Interfaces;

public class PlayerStats : MonoSingleton<PlayerStats>, iCooldownable, iDamagable<float>
{
    [SerializeField]
    VRPlayerController _player;

    [SerializeField]
    PlayerComponents _playerComponents;

    public float cooldownTimer { get; set; }
    public float Health { get; set; }
    public bool godMode { get; set; }

    public StatsData data { get; set; }

    float _minAttackDamage;
    float _maxAttackDamage;

    public int currentMagicFocus { get; set; }

    public int gold { get; set; }
    public int arcaneCrystals { get; set; }
    public int keyCrystals { get; set; }
    public int souls { get; set; }

    private void Awake()
    {
        data = new StatsData();
    }

    private void LateUpdate()
    {
        if (data.iFrame && CooldownDone())
            data.iFrame = false;
    }

    public void SetAllStats(StatsData newStats)
    {
        data = newStats;

        SetAttackDamageRange();
    }

    public void AdjustSpecificStat(StatsData.StatAdjustmentType statToAdjust, float adjustmentValue)
    {
        switch (statToAdjust)
        {
            case StatsData.StatAdjustmentType.iFrameTime:

                data.iFrameTime += adjustmentValue;

                data.iFrameTime = CheckStatLimit(data.iFrameTime, 0.5f, 3);

                break;

            case StatsData.StatAdjustmentType.maxHealth:

                data.maxHealth += adjustmentValue;

                data.maxHealth = CheckStatLimit(data.maxHealth, 0, 1000);

                Health = CheckStatLimit(Health, 0, data.maxHealth);

                if (Health == 0)
                    Death();

                break;

            case StatsData.StatAdjustmentType.playerSpeed:

                data.playerSpeed += adjustmentValue;

                data.playerSpeed = CheckStatLimit(data.playerSpeed, 3, 10);

                break;

            case StatsData.StatAdjustmentType.jumpVelocity:

                data.jumpVelocity += adjustmentValue;

                data.jumpVelocity = CheckStatLimit(data.jumpVelocity, 4, 15);

                break;

            case StatsData.StatAdjustmentType.dashDistance:

                data.dashDistance += adjustmentValue;

                data.dashDistance = CheckStatLimit(data.dashDistance, 6, 15);

                break;

            case StatsData.StatAdjustmentType.throwingForce:

                data.throwingForce += adjustmentValue;

                data.throwingForce = CheckStatLimit(data.throwingForce, 10, 20);

                break;

            case StatsData.StatAdjustmentType.attackDamage:

                data.attackDamage += adjustmentValue;

                data.attackDamage = CheckStatLimit(data.attackDamage, 0.5f, 150);

                SetAttackDamageRange();

                break;

            case StatsData.StatAdjustmentType.attackRange:

                data.attackRange += adjustmentValue;

                data.attackRange = CheckStatLimit(data.attackRange, 1, 5);

                break;

            case StatsData.StatAdjustmentType.attackCooldown:

                data.attackCooldown += adjustmentValue;

                data.attackCooldown = CheckStatLimit(data.attackCooldown, 0.25f, 5);

                break;

            case StatsData.StatAdjustmentType.elementalEffectChance:

                data.elementalEffectChance += adjustmentValue;

                data.elementalEffectChance = CheckStatLimit(data.elementalEffectChance, 5, 100);

                break;

            case StatsData.StatAdjustmentType.luck:

                data.luck += adjustmentValue;

                data.luck = CheckStatLimit(data.luck, -10, 10);

                break;

            case StatsData.StatAdjustmentType.critChance:

                data.critChance += adjustmentValue;

                data.critChance = CheckStatLimit(data.critChance, 5, 100);

                break;

            case StatsData.StatAdjustmentType.critDamage:

                data.critDamage += adjustmentValue;

                data.critDamage = CheckStatLimit(data.critDamage, 10, 150);

                break;

            case StatsData.StatAdjustmentType.specialEffectChance:

                data.specialEffectChance += adjustmentValue;

                data.specialEffectChance = CheckStatLimit(data.specialEffectChance, 5, 100);

                break;

            case StatsData.StatAdjustmentType.aimAssist:

                data.aimAssist += adjustmentValue;

                data.aimAssist = CheckStatLimit(data.aimAssist, 0.5f, 3);

                break;

            case StatsData.StatAdjustmentType.magicFocus:

                data.magicFocus += adjustmentValue;

                data.magicFocus = CheckStatLimit(data.magicFocus, 1, 20);

                break;

            case StatsData.StatAdjustmentType.gold:

                gold += (int)adjustmentValue;

                gold = (int)CheckStatLimit(gold, 0, 999);

                WalletController.Instance.UpdateGoldDisplay(gold);

                break;

            case StatsData.StatAdjustmentType.arcaneCrystals:

                arcaneCrystals += (int)adjustmentValue;

                arcaneCrystals = (int)CheckStatLimit(arcaneCrystals, 0, 16);

                _playerComponents.bombKeyDisplay[_player.GetOffHandIndex()].AdjustDisplay(arcaneCrystals, 16);

                break;

            case StatsData.StatAdjustmentType.keys:

                keyCrystals += (int)adjustmentValue;

                keyCrystals = (int)CheckStatLimit(keyCrystals, 0, 16);

                _playerComponents.bombKeyDisplay[_player.GetPrimaryHandIndex()].AdjustDisplay(keyCrystals, 16);

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

    public bool iFrame { get; private set; }


    public void Damage(float damageAmount)
    {
        if (damageAmount < 0 && !godMode && !data.iFrame)
        {
            Health += damageAmount;

            _player.GetPlayerComponents().hitEffect.PlayerHit();

            data.iFrame = true;

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

        data.isDead = true;

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
        data.isDead = false;

        data.maxHealth = CheckStatLimit(maxHealth, 1, 100);

        Health = CheckStatLimit(health, 1, data.maxHealth);
    }

    private void SetAttackDamageRange()
    {
        _minAttackDamage = data.attackDamage * 0.9f;
        _maxAttackDamage = data.attackDamage * 1.1f;
    }

    public float AttackDamage() 
    { 
        float damageAmount = Random.Range(_minAttackDamage, _maxAttackDamage);

        bool critHit = data.critChance > Random.Range(0, 100) ? true : false;

        if (critHit)
            damageAmount = (damageAmount * data.critDamage) + damageAmount;

        return damageAmount;
    }

    public void ChargeMagicFocus()
    {
        currentMagicFocus = (int)data.magicFocus;
    }

    public async Task LoadStats(PlayerDungeonData loadedData)
    {
        // Base Stats
        data.maxHealth = loadedData.maxHealth;
        Health = loadedData.currentHealth;
        data.playerSpeed = loadedData.playerSpeed;
        data.sprintSpeed = loadedData.sprintMultiplier;
        data.crouchSpeed = loadedData.crouchSpeedReduction;
        data.jumpVelocity = loadedData.jumpVelocity;
        data.dashDistance = loadedData.dashDistance;


        // Attack Stats
        data.attackDamage = loadedData.attackDamage;
        _minAttackDamage = loadedData.minAttackDamage;
        _maxAttackDamage = loadedData.maxAttackDamage;
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
        gold = loadedData.currentGold;
        arcaneCrystals = loadedData.currentArcaneCrystals;
        keyCrystals = loadedData.currentKeys;
        souls = loadedData.currentSouls;
    }
}
