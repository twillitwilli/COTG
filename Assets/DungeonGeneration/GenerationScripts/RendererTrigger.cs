using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererTrigger : MonoBehaviour
{
    public string tagToLookFor;

    public List<GameObject> objectsToEnable, objectsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            foreach (GameObject enableObj in objectsToEnable) { if (enableObj) { enableObj.SetActive(true); } }
            foreach (GameObject disableObj in objectsToDisable) { if (disableObj) { disableObj.SetActive(false); } }
        }
    }
}
