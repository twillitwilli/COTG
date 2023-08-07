using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTriggerEnter : MonoBehaviour
{
    public string tagToLookFor;
    public List<GameObject> objToDisable = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (tagToLookFor == other.gameObject.tag)
        {
            foreach (GameObject obj in objToDisable) { obj.SetActive(true); }
        }
    }
}
