using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUsed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RitualRune"))
        {
            switch (LocalGameManager.Instance.currentGameMode)
            {
                case LocalGameManager.GameMode.master | LocalGameManager.GameMode.normal:
                    PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.runesUsed);
                    break;
            }
        }
    }
}
