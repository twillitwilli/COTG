using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSteal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        VRPlayer player;

        if (other.gameObject.TryGetComponent<VRPlayer>(out player))
            PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.gold, -RandomGoldAmount());
    }

    int RandomGoldAmount()
    {
        return Random.Range(1, 6);
    }
}
