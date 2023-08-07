using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyRoomUnlockCheck : MonoBehaviour
{
    private PlayerTotalStats _totalStats;

    private void Start()
    {
        _totalStats = LocalGameManager.instance.GetTotalStats();

        if (_totalStats.enemiesKilled > 1) { gameObject.SetActive(false); }
    }
}
