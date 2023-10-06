using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class PlayerPotionController : MonoSingleton<PlayerPotionController>
{
    private VRPlayerController _player;

    public enum PotionType 
    { 
        none, 
        death, 
        sight, 
        movement, 
        strength, 
        fairy, 
        arcane, 
        rainbow, 
        health, 
        angelic, 
        lucky 
    }

    public enum TempEffect 
    { 
        movement, 
        strength, 
        flight, 
        magicFocus, 
        godMode 
    }

    [HideInInspector] 
    public float movementBoost, strengthBoost, magicFocusBoost;

    public bool[] effectBoosts;

    [HideInInspector] 
    public PotionType lastPotionDrank;

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        _player = player;
    }

    public void GrabPotion(VRPlayerHand hand, GameObject potionObj)
    {
        Vector3 pos;
        Vector3 rot;
        Vector3 scale;

        if (!hand.IsRightHand())
        {
            pos = new Vector3(-0.02700001f, -0.034f, -0.05700001f);
            rot = new Vector3(-90, 0, -101.592f);
            scale = new Vector3(0.2500001f, 0.2500001f, 0.2499999f);
        }

        else
        {
            pos = new Vector3(-0.07714288f, -0.09714283f, -0.1628571f);
            rot = new Vector3(-90, 0 , -101.592f);
            scale = new Vector3(0.714286f, 0.7142861f, 0.7142854f);
        }

        hand.ParentObjectToFixedHandPosition(potionObj, pos, rot, scale);
    }

    public void PotionEffect(PotionType whichPotionEffect, string potionDescription, bool drankRainbowPotion)
    {
        if (!drankRainbowPotion)
        {
            if (_player == null)
                _player = LocalGameManager.Instance.player;

            lastPotionDrank = whichPotionEffect;

            OnScreenText onScreenText = _player.GetPlayerComponents().onScreenText;
            onScreenText.PrintText(potionDescription, true);
        }
        

        switch (lastPotionDrank)
        {
            case PotionType.death:
                //onScreenText.PrintText("The world is empty", true);
                DeathEffect();
                break;

            case PotionType.sight:
                //onScreenText.PrintText("I can see everything", true);
                SightEffect();
                break;

            case PotionType.movement:
                //onScreenText.PrintText("Is this a drink of energy", true);
                TemporaryEffect(TempEffect.movement);
                break;

            case PotionType.strength:
                //onScreenText.PrintText("Power from the gods", true);
                TemporaryEffect(TempEffect.strength);
                break;

            case PotionType.fairy:
                //onScreenText.PrintText("Light as a feather", true);
                if (!_player.canFly) { TemporaryEffect(TempEffect.flight); }
                break;

            case PotionType.arcane:
                //onScreenText.PrintText("Knowledge of the gods", true);
                TemporaryEffect(TempEffect.magicFocus);
                break;
            case PotionType.rainbow:
                //onScreenText.PrintText("Knowledge of the dark", true);
                RainbowEffect(potionDescription);
                break;

            case PotionType.health:
                //onScreenText.PrintText("Blood of the fallen", true);
                PlayerStats.Instance.Damage(100);
                break;

            case PotionType.angelic:
                //onScreenText.PrintText("True power of a god", true);
                TemporaryEffect(TempEffect.godMode);
                break;

            case PotionType.lucky:
                //onScreenText.PrintText("Seems lucky", true);
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.luck, 5);
                break;
        }
    }

    public void DeathEffect()
    {
        if (EnemyTrackerController.Instance.spawnedEnemies.Count > 0)
        {
            foreach (GameObject enemy in EnemyTrackerController.Instance.spawnedEnemies) 
            { 
                enemy.GetComponentInChildren<EnemyHealth>().AdjustHealth(-375, _player); 
            }
        }

        if (EnemyTrackerController.Instance.spawnedBoss != null)
            EnemyTrackerController.Instance.spawnedBoss.GetComponentInChildren<EnemyHealth>().AdjustHealth(-375, _player);
    }

    public void SightEffect()
    {
        MapController.Instance.RevealDungeonMap();
        CompassController.Instance.CompassReveal();
    }

    public void RainbowEffect(string potionDescription)
    {
        int randomEffect = Random.Range(1, 10);

        switch (randomEffect)
        {
            case 1:
                PotionEffect(PotionType.death, potionDescription, true);
                break;

            case 2:
                PotionEffect(PotionType.sight, potionDescription, true);
                break;

            case 3:
                PotionEffect(PotionType.movement, potionDescription, true);
                break;

            case 4:
                PotionEffect(PotionType.strength, potionDescription, true);
                break;

            case 5:
                PotionEffect(PotionType.fairy, potionDescription, true);
                break;

            case 6:
                PotionEffect(PotionType.arcane, potionDescription, true);
                break;

            case 7:
                PotionEffect(PotionType.health, potionDescription, true);
                break;

            case 8:
                PotionEffect(PotionType.angelic, potionDescription, true);
                break;

            default:
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.luck, 5);
                break;
        }
    }

    public void TemporaryEffect(TempEffect whichEffect)
    {
        switch (whichEffect)
        {
            case TempEffect.movement:
                effectBoosts[0] = true;
                movementBoost += 2;
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.playerSpeed, 2);
                break;

            case TempEffect.strength:
                effectBoosts[1] = true;
                strengthBoost += 15;
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.attackDamage, 15);
                break;

            case TempEffect.flight:
                effectBoosts[2] = true;
                _player.canFly = true;
                break;

            case TempEffect.magicFocus:
                effectBoosts[3] = true;
                magicFocusBoost += 3;
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.magicFocus, 3);
                break;

            case TempEffect.godMode:
                effectBoosts[4] = true;
                _player.godMode = true;
                Invoke("ClearGodMode", 30);
                break;
        }
    }

    public void ClearGodMode()
    {
        _player.godMode = false;
    }

    public void ClearTemporaryEffects()
    {
        if (effectBoosts[0])
        {
            PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.playerSpeed, -movementBoost);
            movementBoost = 0;
        }

        if (effectBoosts[1])
        {
            PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.attackDamage, -strengthBoost);
            strengthBoost = 0;
        }

        if (effectBoosts[2])
            _player.canFly = false;

        if (effectBoosts[3])
        {
            PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.magicFocus, -magicFocusBoost);
            magicFocusBoost = 0;
        }

        if (effectBoosts[4])
            _player.godMode = false;

        for (int i = 0; i < effectBoosts.Length; i++)
        {
            effectBoosts[i] = false;
        }
    }
}
