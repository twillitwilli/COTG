using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class PlayerCardContnroller : MonoSingleton<PlayerCardContnroller>
{
    [SerializeField] 
    PlayerCard[] _playerCards;

    PlayerCard _currentPlayerCard;

    // Sorcerer Only
    public delegate void SorcererSelected(Sorcerer sorcererController);
    public static event SorcererSelected newSorcerer;

    public void ChangePlayerCard(MagicController.ClassType newClass)
    {
        _currentPlayerCard = _playerCards[(int)newClass];

        ClassDefaultStats();
    }

    public void ClassDefaultStats()
    {
        PlayerStats playerStats = PlayerStats.Instance;

        playerStats.data.maxHealth = _currentPlayerCard.classStats.maxHealth;
        playerStats.data.currentHealth = _currentPlayerCard.classStats.currentHealth;
        playerStats.data.playerSpeed = _currentPlayerCard.classStats.playerSpeed;
        playerStats.data.sprintMultiplier = _currentPlayerCard.classStats.sprintMultiplier;
        playerStats.data.crouchSpeedReduction = _currentPlayerCard.classStats.crouchSpeedReduction;
        playerStats.data.jumpVelocity = _currentPlayerCard.classStats.jumpVelocity;
        playerStats.data.dashDistance = _currentPlayerCard.classStats.dashDistance;
        playerStats.data.throwingForce = _currentPlayerCard.classStats.throwingForce;
        playerStats.data.iFrameTime = _currentPlayerCard.classStats.iFrameTime;

        // Attack Stats
        playerStats.data.attackDamage = _currentPlayerCard.classStats.attackDamage;
        playerStats.data.attackRange = _currentPlayerCard.classStats.attackRange;
        playerStats.data.attackCooldown = _currentPlayerCard.classStats.attackCooldown;
        playerStats.data.elementalEffectChance = _currentPlayerCard.classStats.elementalEffectChance;
        playerStats.data.luck = _currentPlayerCard.classStats.luck;
        playerStats.data.critChance = _currentPlayerCard.classStats.critChance;
        playerStats.data.critDamage = _currentPlayerCard.classStats.critDamage;
        playerStats.data.specialEffectChance = _currentPlayerCard.classStats.specialEffectChance;
        playerStats.data.aimAssist = _currentPlayerCard.classStats.aimAssist;
        playerStats.data.magicFocus = _currentPlayerCard.classStats.magicFocus;
    }
}
