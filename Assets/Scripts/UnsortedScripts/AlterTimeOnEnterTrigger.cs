using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterTimeOnEnterTrigger : MonoBehaviour
{
    public float slowFactor = 0.5f;
    private bool isSlowing = false;

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = slowFactor;
        isSlowing = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Time.timeScale = 1.0f;
        isSlowing = false;
    }

    private void OnDisable()
    {
        if (isSlowing)
        {
            Time.timeScale = 1.0f;
        }
    }
}
