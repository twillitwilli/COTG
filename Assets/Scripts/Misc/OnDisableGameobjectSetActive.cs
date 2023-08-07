using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableGameobjectSetActive : MonoBehaviour
{
    [Header("Enable Objects")]
    public bool enableObjects;
    public GameObject[] enableGameobjects;

    [Header("Disable Objects")]
    public bool disableObjects;
    public GameObject[] disableGameobjects;

    private void OnDisable()
    {
        if (enableObjects)
        {
            foreach (GameObject enableObjs in enableGameobjects)
            {
                enableObjs.SetActive(true);
            }
        }

        if (disableObjects)
        {
            foreach (GameObject disableObjs in disableGameobjects)
            {
                disableObjs.SetActive(false);
            }
        }
    }
}
