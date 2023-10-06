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
                _thisDisplay.text = "Atk Dmg: " + stats.minAttackDamage.ToString() + "-" + stats.maxAttackDamage.ToString();
                break;

            case Stats.magicFocus:
                _thisDisplay.text = "Magic Focus: " + stats.data.magicFocus.ToString();
                break;

            case Stats.rateOfFire:
                _thisDisplay.text = "Atk CD: " + stats.data.attackCooldown.ToString();
                break;

            case Stats.range:
                _thisDisplay.text = "Atk Range: " + stats.data.attackRange.ToString();
                break;

            case Stats.movement:
                _thisDisplay.text = "Movement: " + stats.data.playerSpeed.ToString();
                break;

            case Stats.luck:
                _thisDisplay.text = "Luck: " + stats.data.luck.ToString();
                break;

            case Stats.curse:
                _thisDisplay.text = PlayerCurse.Instance.CheckCurrentCurseStatus();
                break;

            case Stats.potionEffect:
                CheckPotionEffect();
                break;

            case Stats.hp:
                int currentHealth = (int)stats.Health;
                int maxHealth = (int)stats.data.maxHealth;

                _thisDisplay.text = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
                break;

            case Stats.arcane:
                _thisDisplay.text = "Arcane Crystals: " + stats.data.currentArcaneCrystals.ToString() + "/16";
                break;

            case Stats.critChance:
                _thisDisplay.text = "Crit Chance: " + stats.data.critChance.ToString() + "%";
                break;

            case Stats.aimAssist:
                _thisDisplay.text = "Aim Assist: " + stats.data.aimAssist.ToString();
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
                _thisDisplay.text = "Crit Dmg: " + Mathf.RoundToInt(stats.data.critDamage * 100).ToString();
                break;

            case Stats.elementalEffect:
                _thisDisplay.text = "Elemental Effect Chance: " + stats.data.elementalEffectChance.ToString() + "%";
                break;

            case Stats.specialEffectChance:
                _thisDisplay.text = "Special Effect Chance: " + stats.data.specialEffectChance.ToString() + "%";
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
