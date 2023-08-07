using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentAfterDelay : MonoBehaviour
{
    public float delay;

    public bool destroyParent;

    private GameObject parentObject;

    private void Start()
    {
        parentObject = GetComponentInParent<GameObject>();

        Invoke("UnparentDelay", delay);
    }

    private void UnparentDelay()
    {
        this.transform.SetParent(null);

        if(destroyParent)
        {
            Destroy(parentObject);
        }
        else if (!destroyParent)
        {
            parentObject = null;
        }
    }
}
