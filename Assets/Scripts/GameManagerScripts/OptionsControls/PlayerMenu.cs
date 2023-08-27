using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMenu : MonoSingleton<PlayerMenu>
{
    private VRPlayerController _player;

    [HideInInspector] public string multiplayerRoomName;

    private void Start()
    {
        _player = LocalGameManager.Instance.player;
        _player.menuSpawned = true;
    }

    public void ClosePlayerMenu()
    {
        Destroy(gameObject);
    }

    private async void OnDestroy()
    {
        _player.menuSpawned = false;
        await LocalGameManager.Instance.GetPlayerPrefsSaveData().SavePlayerPrefs();
    }
}
