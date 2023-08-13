using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    private Text _thisDisplay;

    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;
    private MagicController _magicController;
    private PlayerCurse _playerCurse;
    private PlayerPotionController _potionController;

    public enum stats { damage, magicFocus, rateOfFire, range, movement, luck, curse, potionEffect, hp, arcane, critChance, aimAssist, 
        elementalEffect, playerClass, magicType, castingType, critDamage, specialEffectChance }
    public stats statDisplay;

    [SerializeField] private MapItem mapItem;

    private void Awake()
    {
        _thisDisplay = GetComponent<Text>();
    }

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _playerStats = _gameManager.GetPlayerStats();
        _magicController = _gameManager.GetMagicController();
        _playerCurse = _gameManager.GetCurseController();
        _potionController = _gameManager.GetPotionController();

        switch (statDisplay)
        {
            case stats.damage:
                _thisDisplay.text = "Atk Dmg: " + _playerStats.GetMinAttackDamage().ToString() + "-" + _playerStats.GetMaxAttackDamage().ToString();
                break;

            case stats.magicFocus:
                _thisDisplay.text = "Magic Focus: " + _playerStats.GetMagicFocus().ToString();
                break;

            case stats.rateOfFire:
                _thisDisplay.text = "Atk CD: " + _playerStats.GetAttackCooldown().ToString();
                break;

            case stats.range:
                _thisDisplay.text = "Atk Range: " + _playerStats.GetAttackRange().ToString();
                break;

            case stats.movement:
                _thisDisplay.text = "Movement: " + _playerStats.GetPlayerSpeed().ToString();
                break;

            case stats.luck:
                _thisDisplay.text = "Luck: " + _playerStats.GetLuck().ToString();
                break;

            case stats.curse:
                _thisDisplay.text = _playerCurse.CheckCurrentCurseStatus();
                break;

            case stats.potionEffect:
                CheckPotionEffect();
                break;

            case stats.hp:
                int currentHealth = Mathf.RoundToInt(_playerStats.GetCurrentHealth());
                int maxHealth = Mathf.RoundToInt(_playerStats.GetMaxHealth());
                _thisDisplay.text = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
                break;

            case stats.arcane:
                _thisDisplay.text = "Arcane Crystals: " + _playerStats.GetCurrentArcaneCrystals().ToString() + "/16";
                break;

            case stats.critChance:
                _thisDisplay.text = "Crit Chance: " + _playerStats.GetCritChance().ToString() + "%";
                break;

            case stats.aimAssist:
                _thisDisplay.text = "Aim Assist: " + _playerStats.GetAimAssist().ToString();
                break;

            case stats.elementalEffect:
                _thisDisplay.text = "Elemental Effect:\n" + _playerStats.GetElementalEffectChance().ToString() + "%";
                break;

            case stats.playerClass:
                _thisDisplay.text = "Class: " + _magicController.GetClassType();
                break;

            case stats.magicType:
                _thisDisplay.text = "Magic Type: " + _magicController.magicName;
                break;

            case stats.castingType:
                _thisDisplay.text = "Casting Type: " + _magicController.GetCurrentCastingType();
                break;

            case stats.critDamage:
                _thisDisplay.text = "Crit Dmg: " + Mathf.RoundToInt(_playerStats.GetCritDamage() * 100).ToString();
                break;

            case stats.specialEffectChance:
                _thisDisplay.text = "Special Effect Chance: " + _playerStats.GetElementalEffectChance().ToString() + "%";
                break;
        }
    }

    public void CheckPotionEffect()
    {
        switch (_potionController.lastPotionDrank)
        {
            case PlayerPotionController.PotionType.none:
                _thisDisplay.text = "";
                break;
            case PlayerPotionController.PotionType.death:
                _thisDisplay.text = "Death Potion";
                break;
            case PlayerPotionController.PotionType.sight:
                _thisDisplay.text = "Sight Potion";
                break;
            case PlayerPotionController.PotionType.movement:
                _thisDisplay.text = "Movement Potion";
                break;
            case PlayerPotionController.PotionType.strength:
                _thisDisplay.text = "Strength Potion";
                break;
            case PlayerPotionController.PotionType.fairy:
                _thisDisplay.text = "Fairy Potion";
                break;
            case PlayerPotionController.PotionType.arcane:
                _thisDisplay.text = "Arcane Potion";
                break;
            case PlayerPotionController.PotionType.rainbow:
                _thisDisplay.text = "Rainbow Potion";
                break;
            case PlayerPotionController.PotionType.health:
                _thisDisplay.text = "Health Potion";
                break;
            case PlayerPotionController.PotionType.angelic:
                _thisDisplay.text = "Angelic Potion";
                break;
            case PlayerPotionController.PotionType.lucky:
                _thisDisplay.text = "Lucky Potion";
                break;
        }
    }
}
