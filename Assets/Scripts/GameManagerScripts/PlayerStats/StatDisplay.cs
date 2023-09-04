using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    private Text _thisDisplay;

    public enum Stats 
    { 
        damage, 
        magicFocus, 
        rateOfFire, 
        range, 
        movement, 
        luck, 
        curse, 
        potionEffect, 
        hp, 
        arcane, 
        critChance, 
        aimAssist, 
        elementalEffect, 
        playerClass, 
        magicType, 
        castingType, 
        critDamage, 
        specialEffectChance 
    }

    public Stats statDisplay;

    [SerializeField] 
    private MapItem mapItem;

    private void Awake()
    {
        _thisDisplay = GetComponent<Text>();
    }

    private void Start()
    {
        PlayerStats stats = PlayerStats.Instance;

        switch (statDisplay)
        {
            case Stats.damage:
                _thisDisplay.text = "Atk Dmg: " + stats.GetMinAttackDamage().ToString() + "-" + stats.GetMaxAttackDamage().ToString();
                break;

            case Stats.magicFocus:
                _thisDisplay.text = "Magic Focus: " + stats.GetMagicFocus().ToString();
                break;

            case Stats.rateOfFire:
                _thisDisplay.text = "Atk CD: " + stats.GetAttackCooldown().ToString();
                break;

            case Stats.range:
                _thisDisplay.text = "Atk Range: " + stats.GetAttackRange().ToString();
                break;

            case Stats.movement:
                _thisDisplay.text = "Movement: " + stats.GetPlayerSpeed().ToString();
                break;

            case Stats.luck:
                _thisDisplay.text = "Luck: " + stats.GetLuck().ToString();
                break;

            case Stats.curse:
                _thisDisplay.text = PlayerCurse.Instance.CheckCurrentCurseStatus();
                break;

            case Stats.potionEffect:
                CheckPotionEffect();
                break;

            case Stats.hp:
                int currentHealth = Mathf.RoundToInt(stats.GetCurrentHealth());
                int maxHealth = Mathf.RoundToInt(stats.GetMaxHealth());

                _thisDisplay.text = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
                break;

            case Stats.arcane:
                _thisDisplay.text = "Arcane Crystals: " + stats.GetCurrentArcaneCrystals().ToString() + "/16";
                break;

            case Stats.critChance:
                _thisDisplay.text = "Crit Chance: " + stats.GetCritChance().ToString() + "%";
                break;

            case Stats.aimAssist:
                _thisDisplay.text = "Aim Assist: " + stats.GetAimAssist().ToString();
                break;

            case Stats.elementalEffect:
                _thisDisplay.text = "Elemental Effect:\n" + stats.GetElementalEffectChance().ToString() + "%";
                break;

            case Stats.playerClass:
                _thisDisplay.text = "Class: " + MagicController.Instance.currentClass;
                break;

            case Stats.magicType:
                _thisDisplay.text = "Magic Type: " + MagicController.Instance.magicName;
                break;

            case Stats.castingType:
                _thisDisplay.text = "Casting Type: " + MagicController.Instance.currentCastingType;
                break;

            case Stats.critDamage:
                _thisDisplay.text = "Crit Dmg: " + Mathf.RoundToInt(stats.GetCritDamage() * 100).ToString();
                break;

            case Stats.specialEffectChance:
                _thisDisplay.text = "Special Effect Chance: " + stats.GetElementalEffectChance().ToString() + "%";
                break;
        }
    }

    public void CheckPotionEffect()
    {
        switch (PlayerPotionController.Instance.lastPotionDrank)
        {
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

            default:
                _thisDisplay.text = "";
                break;
        }
    }
}
