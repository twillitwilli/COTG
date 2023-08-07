using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDelay : MonoBehaviour
{
    public GameObject objectToDestroy;
    public float delayBeforeDestroy;

    private void Start()
    {
        Destroy(objectToDestroy, delayBeforeDestroy);
    }
}
