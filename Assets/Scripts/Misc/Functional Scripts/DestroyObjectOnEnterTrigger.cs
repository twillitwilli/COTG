using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnEnterTrigger : MonoBehaviour
{
    public string tagToLookFor;
    public float delay;
    public GameObject[] objectToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor)) 
        { 
            foreach (GameObject obj in objectToDestroy) { Destroy(obj, delay); } 
        }
    }
}
