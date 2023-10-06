using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndMovePlayer : MonoBehaviour
{
    public string objectName;
    public float damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;

        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            PlayerStats.Instance.Damage(-damageAmount);

            LocalGameManager.Instance.MovePlayer(LocalGameManager.SpawnLocation.moveableSpawnPoint);
        }
    }
}
