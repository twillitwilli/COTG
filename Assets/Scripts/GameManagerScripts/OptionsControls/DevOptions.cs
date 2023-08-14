using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevOptions : MonoBehaviour
{
    public enum DevOption { flight, map, giveItems, killEnemies, killReaper, giveHealth, resetPlayerSave, 
        godMode, changeClass, magicType, castingType }
    
    public DevOption playerDevOptions;

    [SerializeField] private Text _textBox;
    [SerializeField] private bool _checkStatusOnly;

    [SerializeField] private MagicController.ClassType _classType;
    [SerializeField] private MagicController.MagicType _magicType;
    [SerializeField] private MagicController.CastingType _castingType;

    private LocalGameManager _gameManager;
    private EnemyTrackerController _enemyTrackerController;

    private VRPlayerController _player;
    private PlayerStats _playerStats;

    private DungeonBuildParent _dungeonBuildParent;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _enemyTrackerController = _gameManager.GetEnemyTrackerController();

        _player = _gameManager.player;
        _playerStats = _gameManager.GetPlayerStats();

        if (DungeonBuildParent.instance != null) { _dungeonBuildParent = DungeonBuildParent.instance; }

        if (_checkStatusOnly) { ChangeDevOption(); }
    }

    public void ChangeDevOption()
    {
        switch(playerDevOptions)
        {
            case DevOption.flight:
                FlightToggle();
                break;

            case DevOption.map:
                RevealMap();
                break;

            case DevOption.giveItems:
                GiveItem();
                break;

            case DevOption.killEnemies:
                KillEnemies();
                break;

            case DevOption.killReaper:
                KillReaper();
                break;

            case DevOption.giveHealth:
                GiveHealth();
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
                ChangeMagicType();
                break;

            case DevOption.castingType:
                ChangeCastingType();
                break;
        }
    }

    private void ChangeText(string newText)
    {
        _textBox.text = newText;
    }

    private void FlightToggle()
    {
        if (!_checkStatusOnly)
        {
            bool flight = _player.canFly ? false : true;
            _player.canFly = flight;
        }
        
        ChangeText(_player.canFly ? "Flight:\nOn" : "Flight:\nOff");
    }

    private void RevealMap()
    {
        if (_dungeonBuildParent != null)
        {
            _dungeonBuildParent.GetMapController().RevealMap();
            _dungeonBuildParent.GetCompassController().CompassReveal();
        }
    }

    private void GiveItem()
    {
        _playerStats.AdjustGoldAmount(999);
        _playerStats.AdjustArcaneCrystalAmount(999);
        _playerStats.AdjustKeyAmount(999);
    }

    private void KillEnemies()
    {
        _enemyTrackerController.KillAllEnemies();
    }

    private void KillReaper()
    {
        _enemyTrackerController.KillReaper();
    }

    private void GiveHealth()
    {
        _playerStats.AdjustHealth(9999, "*@#&$&%#&@!#@)*)#@!!");
    }

    private void ResetPlayerSave()
    {
        PlayerPrefs.DeleteKey("ReturningPlayer");
        Application.Quit();
    }

    private void ToggleGodMode()
    {
        if (!_checkStatusOnly)
        {
            bool godMode = _player.godMode ? false : true;
            _player.godMode = godMode;
        }

        ChangeText(_player.godMode ? "God Mode:\nOn" : "God Mode:\nOff");
    }

    private void ChangeClass()
    {
        if (!_checkStatusOnly)
        {
            _gameManager.GetMagicController().ChangeClass(_classType);
        }

        ChangeText("Current Class:\n" + _gameManager.GetMagicController().GetClassType());
    }

    private void ChangeMagicType()
    {
        if (!_checkStatusOnly)
        {
            _gameManager.GetMagicController().AddMagic(_magicType);
        }

        ChangeText("Current Magic:\n" + _gameManager.GetMagicController().magicName);
    }

    private void ChangeCastingType()
    {
        if (!_checkStatusOnly)
        {
            _gameManager.GetMagicController().ChangeCastingType(_castingType);
        }

        ChangeText("Casting Type:\n" + _gameManager.GetMagicController().GetCurrentCastingType());
    }
}
