using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableDelayGameobject : MonoBehaviour
{
    [Header("Enable Objects")]
    public bool enableObjects;
    public float enableDelay;
    public GameObject[] enableGameobjects;

    [Header("Disable Objects")]
    public bool disableObjects;
    public float disableDelay;
    public GameObject[] disableGameobjects;

    private void OnEnable()
    {
        if (enableObjects)
        {
            Invoke("EnableObjects", enableDelay);
        }

        if (disableObjects)
        {
            Invoke("DisableObjects", disableDelay);
        }
    }

    private void EnableObjects()
    {
        foreach (GameObject enableObjs in enableGameobjects)
        {
            enableObjs.SetActive(true);
        }
    }

    private void DisableObjects()
    {
        foreach (GameObject disableObjs in disableGameobjects)
        {
            disableObjs.SetActive(false);
        }
    }
}
