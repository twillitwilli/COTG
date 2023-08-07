using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableGameobjectSetActive : MonoBehaviour
{
    [Header("Enable Objects")]
    public bool enableObjects;
    public GameObject[] enableGameobjects;

    [Header("Disable Objects")]
    public bool disableObjects;
    public GameObject[] disableGameobjects;

    private void OnEnable()
    {
        if (enableObjects)
        {
            foreach (GameObject enableObjs in enableGameobjects)
            {
                if (enableObjs != null) { enableObjs.SetActive(true); }
            }
        }

        if (disableObjects)
        {
            foreach (GameObject disableObjs in disableGameobjects)
            {
                if (disableObjs != null) { disableObjs.SetActive(false); }
            }
        }
    }
}
