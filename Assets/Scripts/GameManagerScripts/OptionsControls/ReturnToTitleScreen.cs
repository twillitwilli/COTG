using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToTitleScreen : MonoBehaviour
{
    private LocalGameManager _gameManager;

    [SerializeField] private bool _inMenu;
    [SerializeField] private GameObject _buttonParent;

    private void Start()
    {
        _gameManager = LocalGameManager.Instance;
    }

    public void ReturnTitleScreen()
    {
        _gameManager.CloseEyes();

        if (PlayerMenu.Instance != null) { PlayerMenu.Instance.ClosePlayerMenu(); }

        _gameManager.ResetPlayer();
        _gameManager.PlayerBackToTitleScreen();
    }
}
