using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthModifier : MonoBehaviour
{
    public float healthValue;
    public bool fromPlayer;

    public VRPlayer player { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyHealth>())
        {
            if (fromPlayer)
                other.gameObject.GetComponent<EnemyHealth>().AdjustHealth(healthValue, player);

            else 
                other.gameObject.GetComponent<EnemyHealth>().AdjustHealth(healthValue, false);

        }
    }
}
