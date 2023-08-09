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
        _gameManager = LocalGameManager.instance;

        if (_inMenu && !_gameManager.inDungeon) { _buttonParent.SetActive(false); }
    }

    public void ReturnTitleScreen()
    {
        _gameManager.GetPlayerPrefsSaveData().SaveData();
        _gameManager.CloseEyes();

        if (PlayerMenu.instance != null) { Destroy(PlayerMenu.instance.gameObject); }

        _gameManager.ResetPlayer();
        _gameManager.savedDungeon = false;
        _gameManager.PlayerBackToTitleScreen();
    }
}
