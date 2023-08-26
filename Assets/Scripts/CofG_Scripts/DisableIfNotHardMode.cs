using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotHardMode : MonoBehaviour
{
    private void Start()
    {
        if (LocalGameManager.Instance.currentGameMode != LocalGameManager.GameMode.master) { gameObject.SetActive(false); }
    }
}
