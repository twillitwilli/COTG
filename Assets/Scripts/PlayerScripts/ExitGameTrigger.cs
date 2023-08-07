﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<VRPlayerController>().GetPlayerComponents().GetEyeManager().EyesClosing();
            Application.Quit();
        }
    }
}
