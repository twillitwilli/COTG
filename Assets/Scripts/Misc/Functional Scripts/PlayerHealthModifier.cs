using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthModifier : MonoBehaviour
{
    public string objectName;
    public float healthValue;

    private void Awake()
    {
        if (objectName == null)
            objectName = "Object Not Named: " + gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<VRPlayerController>())
            PlayerStats.Instance.AdjustHealth(healthValue, objectName);
    }
}
