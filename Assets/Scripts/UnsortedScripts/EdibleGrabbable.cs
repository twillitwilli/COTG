using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleGrabbable : MonoBehaviour
{
    public enum EdibleEffect 
    { 
        health 
    }

    public EdibleEffect effect;
    
    public float value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            switch (effect)
            {
                case EdibleEffect.health:
                    PlayerStats.Instance.AdjustHealth(value, "Food Poisoning");
                    break;
            }
        }
    }
}
