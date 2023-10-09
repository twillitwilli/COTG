using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNameDisplayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        VRPlayer player;

        if (other.gameObject.TryGetComponent<VRPlayer>(out player))
            DisplayAreaName(player);
    }

    void DisplayAreaName(VRPlayer player)
    {
        if (LocalGameManager.Instance.currentLevel > 0 && LocalGameManager.Instance.currentLevel < 4)
        {
            player.GetPlayerComponents().onScreenText.PrintText("Evernight Forest\nLevel " + LocalGameManager.Instance.currentLevel, true);
        }
    }
}
