using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotInLobby : MonoBehaviour
{
    private void Start()
    {
        if (LocalGameManager.Instance.currentGameMode != LocalGameManager.GameMode.inLobby) 
        { 
            gameObject.SetActive(false); 
        }
    }
}
