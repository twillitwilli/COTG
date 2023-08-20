using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private async void OnDestroy()
    {
        _player.menuSpawned = false;
        await LocalGameManager.instance.GetPlayerPrefsSaveData().SavePlayerPrefs();
    }
}
