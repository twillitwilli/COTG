using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.transform.SetParent(null);
    }
}
