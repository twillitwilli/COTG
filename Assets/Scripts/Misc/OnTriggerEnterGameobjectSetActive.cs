using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterGameobjectSetActive : MonoBehaviour
{
    public string tagToLookFor;

    public bool enableObjects;
    public GameObject[] enableGameobjects;

    public bool disableObjects;
    public GameObject[] disableGameobjects;

    private int enteredObjs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            enteredObjs++;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor)) { enteredObjs--; }
        if (enteredObjs <= 0) { enteredObjs = 0; }
    }
}
