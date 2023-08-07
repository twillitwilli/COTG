using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingClassTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayerController>())
        {
            VRPlayerController player = other.gameObject.GetComponent<VRPlayerController>();
            player.selectingClass = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayerController>())
        {
            VRPlayerController player = other.gameObject.GetComponent<VRPlayerController>();
            player.selectingClass = false;
        }
    }
}
