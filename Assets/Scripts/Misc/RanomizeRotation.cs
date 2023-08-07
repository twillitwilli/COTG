using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanomizeRotation : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Random.rotation;
    }
}
