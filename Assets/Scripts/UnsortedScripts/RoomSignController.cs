using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSignController : MonoBehaviour
{
    public GameObject signObj;

    private void Awake()
    {
        switch (LocalGameManager.Instance.currentGameMode)
        {
            case LocalGameManager.GameMode.tutorial:
                ActivateSign();
                break;

            default:
                signObj.SetActive(false);
                break;
        }
    }

    public void ActivateSign()
    {
        signObj.SetActive(true);
        signObj.transform.SetParent(null);
    }
}
