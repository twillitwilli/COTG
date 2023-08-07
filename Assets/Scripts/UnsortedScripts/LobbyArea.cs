using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyArea : MonoBehaviour
{
    private void OnEnable()
    {
        LocalGameManager.instance.inLobby = true;
        LocalGameManager.instance.inTutorial = false;
        LocalGameManager.instance.inTitleScreen = false;
    }

    private void OnDisable()
    {
        LocalGameManager.instance.inLobby = false;
    }
}
