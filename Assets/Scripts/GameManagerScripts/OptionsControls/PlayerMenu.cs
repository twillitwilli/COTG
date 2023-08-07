using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public static PlayerMenu instance;

    private VRPlayerController _player;

    [HideInInspector] public string multiplayerRoomName;

    private void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        _player = LocalGameManager.instance.player;
        _player.menuSpawned = true;
    }

    private void OnDestroy()
    {
        _player.menuSpawned = false;
        LocalGameManager.instance.GetPlayerPrefsSaveData().SaveData();
    }
}
