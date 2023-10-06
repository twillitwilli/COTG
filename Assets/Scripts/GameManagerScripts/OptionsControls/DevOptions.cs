using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevOptions : MonoBehaviour
{
    public enum DevOption 
    { 
        flight, 
        map, 
        giveItems, 
        killEnemies, 
        killReaper, 
        giveHealth, 
        resetPlayerSave, 
        godMode, 
        changeClass, 
        magicType, 
        castingType 
    }
    
    public DevOption playerDevOptions;

    [SerializeField] 
    private Text _textBox;
    
    [SerializeField] 
    private bool _checkStatusOnly;

    [SerializeField] 
    private MagicController.ClassType _classType;
    
    [SerializeField] 
    private MagicController.MagicType _magicType;
    
    [SerializeField] 
    private MagicController.CastingType _castingType;

    private void Start()
    {
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
            bool flight = LocalGameManager.Instance.player.canFly ? false : true;
            LocalGameManager.Instance.player.canFly = flight;
        }
        
        ChangeText(LocalGameManager.Instance.player.canFly ? "Flight:\nOn" : "Flight:\nOff");
    }

    private void RevealMap()
    {
        if (DungeonBuildParent.Instance != null)
        {
            MapController.Instance.RevealDungeonMap();
            CompassController.Instance.CompassReveal();
        }
    }

    private void GiveItem()
    {
        PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.gold, 999);
        PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.arcaneCrystals, 999);
        PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.keys, 999);
    }

    private void KillEnemies()
    {
        EnemyTrackerController.Instance.KillAllEnemies();
    }

    private void KillReaper()
    {
        EnemyTrackerController.Instance.KillReaper();
    }

    private void GiveHealth()
    {
        PlayerStats.Instance.Damage(9999);
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
            bool godMode = LocalGameManager.Instance.player.godMode ? false : true;
            LocalGameManager.Instance.player.godMode = godMode;
        }

        ChangeText(LocalGameManager.Instance.player.godMode ? "God Mode:\nOn" : "God Mode:\nOff");
    }

    private void ChangeClass()
    {
        if (!_checkStatusOnly)
            MagicController.Instance.ChangeClass(_classType);

        ChangeText("Current Class:\n" + MagicController.Instance.currentClass);
    }

    private void ChangeMagicType()
    {
        if (!_checkStatusOnly)
            MagicController.Instance.AddMagic(_magicType);

        ChangeText("Current Magic:\n" + MagicController.Instance.magicName);
    }

    private void ChangeCastingType()
    {
        if (!_checkStatusOnly)
            MagicController.Instance.ChangeCastingType(_castingType);

        ChangeText("Casting Type:\n" + MagicController.Instance.currentCastingType);
    }
}
