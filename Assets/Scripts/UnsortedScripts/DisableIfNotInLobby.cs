using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotInLobby : MonoBehaviour
{
    private void Start()
    {
        if (!LocalGameManager.instance.inLobby) { gameObject.SetActive(false); }
    }
}
