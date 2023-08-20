using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotHardMode : MonoBehaviour
{
    private void Start()
    {
        if (LocalGameManager.instance.currentGameMode != LocalGameManager.GameMode.master) { gameObject.SetActive(false); }
    }
}
