using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowBubble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BasicEnemyProjectile>())
        {
            other.gameObject.GetComponent<BasicEnemyProjectile>().ReduceSpeed();
        }
    }
}
