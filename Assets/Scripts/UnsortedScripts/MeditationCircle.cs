using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationCircle : MonoBehaviour
{
    [SerializeField] private MediationState meditationState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayerController>())
        {
            transform.SetParent(null);
            meditationState.playerMeditating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayerController>()) meditationState.playerMeditating = false;
    }
}
