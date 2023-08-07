using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevOptions : MonoBehaviour
{
    public enum DevOption { resetHandPos, adjustHandPositioning, flight, map, compass, gold, killEnemies, killReaper, 
        spellCastAcceleration, spellCastDistance, spellCastStopForce, spellCastHandHead, giveHealth, resetPlayerSave, godMode, changeClass, magicType, castingType }
    
    public DevOption playerDevOptions;

    [SerializeField] private Text _textBox;
    [SerializeField] private bool _checkStatusOnly;
    [SerializeField] private float _valueAdjustment;
    [SerializeField] private bool _enableObjs, _disableObjs;
    [SerializeField] private List<GameObject> _objsToEnable, _objsToDisable;

    private LocalGameManager _gameManager;
    private EnemyTrackerController _enemyTrackerController;

    private VRPlayerController _player;
    private PlayerComponents _playerComponents;
    private PlayerStats _playerStats;

    private DungeonBuildParent _dungeonBuildParent;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _enemyTrackerController = _gameManager.GetEnemyTrackerController();

        _player = _gameManager.player;
        _playerComponents = _player.GetPlayerComponents();
        _playerStats = _gameManager.GetPlayerStats();

        if (DungeonBuildParent.instance != null) { _dungeonBuildParent = DungeonBuildParent.instance; }

        if (_checkStatusOnly) { ChangeDevOption(); }
    }

    public void ChangeDevOption()
    {
        switch(playerDevOptions)
        {
            case DevOption.resetHandPos:
                break;

            case DevOption.adjustHandPositioning:
                break;

            case DevOption.flight:
                FlightToggle();
                break;

            case DevOption.map:
                RevealMap();
                break;

            case DevOption.compass:
                RevealCompass();
                break;

            case DevOption.gold:
                GiveItem(Mathf.RoundToInt(_valueAdjustment));
                break;

            case DevOption.killEnemies:
                KillEnemies();
                break;

            case DevOption.killReaper:
                KillReaper();
                break;

            case DevOption.spellCastAcceleration:
                SpellCastAcceleraion(_valueAdjustment);
                break;

            case DevOption.spellCastDistance:
                SpellCastDistance(_valueAdjustment);
                break;

            case DevOption.spellCastStopForce:
                SpellCastStopForce(_valueAdjustment);
                break;

            case DevOption.spellCastHandHead:
                SpellCastHandHeadDistance(_valueAdjustment);
                break;

            case DevOption.giveHealth:
                GiveHealth(_valueAdjustment);
                break;

            case DevOption.resetPlayerSave:
                ResetPlayerSave();
                break;

            case DevOption.godMode:
                ToggleGodMode();
                break;

            case DevOption.changeClass:
                ChangeClass();
                break;

            case DevOption.magicType:
                ChangeMagicType(Mathf.RoundToInt(_valueAdjustment));
                break;

            case DevOption.castingType:
                ChangeCastingType();
                break;
        }

        if (_enableObjs)
        {
            foreach (GameObject obj in _objsToEnable)
            {
                obj.SetActive(true);
            }
        }

        if (_disableObjs)
        {
            foreach (GameObject obj in _objsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }

    private void FlightToggle()
    {
        if (_player.canFly)
        {
            if (!_checkStatusOnly) 
            {
                _player.canFly = false;
                _textBox.text = "Flight:\nOff";
            }
            else _textBox.text = "Flight:\nOn";
        }
        else
        {
            if (!_checkStatusOnly) 
            {
                _player.canFly = true;
                _textBox.text = "Flight:\nOn";
            }
            else _textBox.text = "Flight:\nOff";
        }
    }

    private void RevealMap()
    {
        if (_dungeonBuildParent != null)
        {
            _dungeonBuildParent.GetMapController().RevealMap();
            _dungeonBuildParent.GetCompassController().CompassReveal();
        }
    }

    private void RevealCompass()
    {
        if (_dungeonBuildParent != null) { _dungeonBuildParent.GetCompassController().CompassReveal(); }
    }

    private void GiveItem(int amount)
    {
        _playerStats.AdjustGoldAmount(amount);
        _playerStats.AdjustArcaneCrystalAmount(amount);
        _playerStats.AdjustKeyAmount(amount);
    }

    private void KillEnemies()
    {
        _enemyTrackerController.KillAllEnemies();
    }

    private void KillReaper()
    {
        _enemyTrackerController.KillReaper();
    }

    private void SpellCastAcceleraion(float adjustmentValue)
    {
        //if (!_checkStatusOnly) { _playerComponents.spellCasting.accelerationReq += adjustmentValue; }
        //_textBox.text = "Acceleration:\n" + Mathf.RoundToInt(_playerComponents.spellCasting.accelerationReq *100);
    }

    private void SpellCastDistance(float adjustmentValue)
    {
        //if (!_checkStatusOnly) { _playerComponents.spellCasting.distanceReq += adjustmentValue; }        
        //_textBox.text = "Distance:\n" + Mathf.RoundToInt(_playerComponents.spellCasting.distanceReq *100);
    }

    private void SpellCastStopForce(float adjustmentValue)
    {
        //if (!_checkStatusOnly) { _playerComponents.spellCasting.stoppingForce += adjustmentValue; }
        //_textBox.text = "Stopping Force:\n" + Mathf.RoundToInt(_playerComponents.spellCasting.stoppingForce *100);
    }

    private void SpellCastHandHeadDistance(float adjustmentValue)
    {
        //if (!_checkStatusOnly) { _playerComponents.spellCasting.distanceBetweenHandAndChest += adjustmentValue; }
        //_textBox.text = "Hand Away From Head Distance:\n" + Mathf.RoundToInt(_playerComponents.spellCasting.distanceBetweenHandAndChest *100);
    }

    private void GiveHealth(float adjustmentValue)
    {
        _playerStats.AdjustHealth(adjustmentValue, "*@#&$&%#&@!#@)*)#@!!");
    }

    private void ResetPlayerSave()
    {
        PlayerPrefs.DeleteKey("ReturningPlayer");
    }

    private void ToggleGodMode()
    {
        if (_checkStatusOnly)
        {
            if (_player.godMode) { _textBox.text = "God Mode:\nOn"; }
            else { _textBox.text = "God Mode:\nOff"; }
        }
        else
        {
            if (_player.godMode)
            {
                _textBox.text = "God Mode:\nOff";
                _player.godMode = false;
            }
            else
            {
                _textBox.text = "God Mode:\nOn";
                _player.godMode = true;
            }
        }
    }

    private void ChangeClass()
    {

    }

    private void ChangeMagicType(int magicIdx)
    {

    }

    private void ChangeCastingType()
    {

    }
}
