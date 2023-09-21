using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using QTArts.AbstractClasses;

public class PlayerMenu : MonoSingleton<PlayerMenu>
{
    [HideInInspector] 
    public string multiplayerRoomName;

    private void Start()
    {
        LocalGameManager.Instance.player.menuSpawned = true;
    }

    public void ClosePlayerMenu()
    {
        Destroy(gameObject);
    }

    private async void OnDestroy()
    {
        LocalGameManager.Instance.player.menuSpawned = false;

        await PlayerPrefsSaveData.Instance.SavePlayerPrefs();
    }
}
