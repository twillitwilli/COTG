using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInDungeonCheck : MonoBehaviour
{
    public void Start()
    {
        switch (LocalGameManager.Instance.currentGameMode)
        {
            case LocalGameManager.GameMode.master | LocalGameManager.GameMode.normal:
                gameObject.SetActive(false);
                break;
        }
    }
}
