using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotInLobby : MonoBehaviour
{
    private void Start()
    {
        if (LocalGameManager.instance.currentGameMode != LocalGameManager.GameMode.inLobby) 
        { 
            gameObject.SetActive(false); 
        }
    }
}
