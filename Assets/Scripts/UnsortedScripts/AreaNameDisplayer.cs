using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNameDisplayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;
        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            DisplayAreaName(player);
        }
    }

    private void DisplayAreaName(VRPlayerController player)
    {
        if (LocalGameManager.Instance.currentLevel > 0 && LocalGameManager.Instance.currentLevel < 4)
        {
            player.GetPlayerComponents().onScreenText.PrintText("Evernight Forest\nLevel " + LocalGameManager.Instance.currentLevel, true);
        }
    }
}
