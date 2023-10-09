using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationCircle : MonoBehaviour
{
    [SerializeField] 
    MediationState meditationState;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayer>())
        {
            transform.SetParent(null);
            meditationState.playerMeditating = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayer>()) 
            meditationState.playerMeditating = false;
    }
}
