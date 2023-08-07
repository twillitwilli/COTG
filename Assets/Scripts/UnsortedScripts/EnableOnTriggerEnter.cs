using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTriggerEnter : MonoBehaviour
{
    public string tagToLookFor;
    public List<GameObject> objToEnable = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (tagToLookFor == other.gameObject.tag)
        {
            foreach(GameObject obj in objToEnable) { obj.SetActive(true); }
        }
    }
}
