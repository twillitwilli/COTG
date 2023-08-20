using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUsed : MonoBehaviour
{
    private PlayerTotalStats _totalStats;

    private void Start()
    {
        _totalStats = LocalGameManager.instance.GetTotalStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RitualRune"))
        {
            switch (LocalGameManager.instance.currentGameMode)
            {
                case LocalGameManager.GameMode.master | LocalGameManager.GameMode.normal:
                    _totalStats.AdjustStats(PlayerTotalStats.StatType.runesUsed);
                    break;
            }
        }
    }
}
