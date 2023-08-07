using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    private VRPlayerController _player;

    private void Start()
    {
        _player = LocalGameManager.instance.player;
    }

    public void ClosePlayerMenu()
    {
        if (PlayerMenu.instance != null) { Destroy(PlayerMenu.instance.gameObject); }
    }
}