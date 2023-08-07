using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public string tagToLookFor;
    public Transform teleportToHere;
    public bool alignRotationToTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            other.gameObject.transform.position = teleportToHere.position;

            if (alignRotationToTransform)
            {
                other.gameObject.transform.rotation = teleportToHere.rotation;
            }
        }
    }
}
