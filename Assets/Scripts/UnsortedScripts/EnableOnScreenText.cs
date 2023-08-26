using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnScreenText : MonoBehaviour
{
    private PlayerComponents _playerComponents;

    private void OnEnable()
    {
        _playerComponents = LocalGameManager.Instance.player.GetPlayerComponents();
        _playerComponents.onScreenText.PrintText("This is the on screen text displayer", false);
    }

    private void OnDisable()
    {
        _playerComponents.onScreenText.DisplayOff();
    }
}
