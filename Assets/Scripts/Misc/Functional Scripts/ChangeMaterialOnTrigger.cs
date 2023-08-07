using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnTrigger : MonoBehaviour
{
    public Material normalMat, changeToMat;
    public string tagToLookFor;
    private MeshRenderer thisRenderer;
    private int colliderCheck;

    private void Awake()
    {
        thisRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            colliderCheck++;
            thisRenderer.material = changeToMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            colliderCheck--;
            if (colliderCheck <= 0)
            {
                colliderCheck = 0;
                thisRenderer.material = normalMat;
            }
        }
    }
}
