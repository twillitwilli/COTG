using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenEnterTrigger : MonoBehaviour
{
    public string[] tagsToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger object = " + gameObject + " / object entering trigger = " + other.gameObject);
        for (int i = 0; i < tagsToDestroy.Length; i++)
        {
            if (other.gameObject.CompareTag(tagsToDestroy[i])) { Destroy(other.gameObject); }
        }
    }
}
