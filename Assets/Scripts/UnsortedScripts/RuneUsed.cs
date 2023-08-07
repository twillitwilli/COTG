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
            _totalStats.AdjustStat(PlayerTotalStats.StatType.runesUsed, 0);
        }
    }
}
