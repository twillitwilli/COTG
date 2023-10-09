using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingClassTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayer>())
        {
            VRPlayer player = other.gameObject.GetComponent<VRPlayer>();
            player.selectingClass = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayer>())
        {
            VRPlayer player = other.gameObject.GetComponent<VRPlayer>();
            player.selectingClass = false;
        }
    }
}
