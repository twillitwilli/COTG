using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerAndQuit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.DeleteKey("ReturningPlayer");
            Application.Quit();
        }
    }
}
