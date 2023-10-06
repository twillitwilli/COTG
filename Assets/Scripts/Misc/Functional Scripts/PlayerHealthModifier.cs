using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthModifier : MonoBehaviour
{
    public float healthValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<VRPlayerController>())
            PlayerStats.Instance.Damage(healthValue);
    }
}
