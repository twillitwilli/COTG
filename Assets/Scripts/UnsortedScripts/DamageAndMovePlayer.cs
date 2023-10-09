using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndMovePlayer : MonoBehaviour
{
    public string objectName;
    public float damageAmount;

    void OnTriggerEnter(Collider other)
    {
        VRPlayer player;

        if (other.gameObject.TryGetComponent<VRPlayer>(out player))
        {
            PlayerStats.Instance.Damage(-damageAmount);

            LocalGameManager.Instance.MovePlayer(LocalGameManager.SpawnLocation.moveableSpawnPoint);
        }
    }
}
