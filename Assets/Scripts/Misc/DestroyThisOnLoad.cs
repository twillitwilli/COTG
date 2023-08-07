using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisOnLoad : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject);
    }
}
