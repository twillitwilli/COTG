using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToTitleScreen : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private VRPlayerController _player;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _player = _gameManager.player;
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
