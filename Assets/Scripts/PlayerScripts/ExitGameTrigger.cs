using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<VRPlayer>().GetPlayerComponents().GetEyeManager().EyesClosing();

            Application.Quit();
        }
    }
}
