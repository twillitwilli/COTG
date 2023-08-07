using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentOnAwake : MonoBehaviour
{
    private GameObject thisObject;

    private void Awake()
    {
        thisObject = this.gameObject;
        thisObject.transform.SetParent(null);
    }
}
