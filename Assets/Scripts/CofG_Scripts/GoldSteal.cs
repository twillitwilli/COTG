using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSteal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;
        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            LocalGameManager.Instance.GetPlayerStats().AdjustGoldAmount(-RandomGoldAmount());
        }
    }

    private int RandomGoldAmount()
    {
        return Random.Range(1, 6);
    }
}
