using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPotionController : MonoBehaviour
{
    private LocalGameManager _gameManager;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private EnemyTrackerController _enemyTrackerController;

    private VRPlayerController _player;

    public enum PotionType { none, death, sight, movement, strength, fairy, arcane, rainbow, health, angelic, lucky }

    public enum TempEffect { movement, strength, flight, magicFocus, godMode }

    [HideInInspector] public float movementBoost, strengthBoost, magicFocusBoost;

    public bool[] effectBoosts;

    [HideInInspector] public PotionType lastPotionDrank;

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        _player = player;
    }

    private void Start()
    {
        _gameManager = LocalGameManager.Instance;
    }

    public void GrabPotion(VRPlayerHand hand)
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

        hand.ParentObjectToFixedHandPosition(hand.GetGrabController().currentGrabbableObj, pos, rot, scale);
    }

    public void PotionEffect(PotionType whichPotionEffect, string potionDescription, bool drankRainbowPotion)
    {
        if (!drankRainbowPotion)
        {
            if (_player == null) { _player = _gameManager.player; }

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
                _playerStats.AdjustHealth(100, "Healing Item");
                break;

            case PotionType.angelic:
                //onScreenText.PrintText("True power of a god", true);
                TemporaryEffect(TempEffect.godMode);
                break;

            case PotionType.lucky:
                //onScreenText.PrintText("Seems lucky", true);
                _playerStats.AdjustLuck(5);
                break;
        }
    }

    public void DeathEffect()
    {
        if (_enemyTrackerController.spawnedEnemies.Count > 0)
        {
            foreach (GameObject enemy in _enemyTrackerController.spawnedEnemies) { enemy.GetComponentInChildren<EnemyHealth>().AdjustHealth(-375, _player); }
        }

        if (_enemyTrackerController.spawnedBoss != null)
        {
            _enemyTrackerController.spawnedBoss.GetComponentInChildren<EnemyHealth>().AdjustHealth(-375, _player);
        }
    }

    public void SightEffect()
    {
        _gameManager.GetGearController().GetMapController().RevealMap();
        _gameManager.GetGearController().GetCompassController().CompassReveal();
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
                _playerStats.AdjustLuck(5);
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
                _playerStats.AdjustPlayerSpeed(2);
                break;

            case TempEffect.strength:
                effectBoosts[1] = true;
                strengthBoost += 15;
                _playerStats.AdjustAttackDamage(15);
                break;

            case TempEffect.flight:
                effectBoosts[2] = true;
                _player.canFly = true;
                break;

            case TempEffect.magicFocus:
                effectBoosts[3] = true;
                magicFocusBoost += 3;
                _playerStats.AdjustMagicFocus(3);
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
            _playerStats.AdjustPlayerSpeed(-movementBoost);
            movementBoost = 0;
        }

        if (effectBoosts[1])
        {
            _playerStats.AdjustAttackDamage(-strengthBoost);
            strengthBoost = 0;
        }

        if (effectBoosts[2])
        {
            _player.canFly = false;
        }

        if (effectBoosts[3])
        {
            _playerStats.AdjustMagicFocus(-Mathf.RoundToInt(magicFocusBoost));
            magicFocusBoost = 0;
        }

        if (effectBoosts[4])
        {
            _player.godMode = false;
        }

        for (int i = 0; i < effectBoosts.Length; i++)
        {
            effectBoosts[i] = false;
        }
    }
}
