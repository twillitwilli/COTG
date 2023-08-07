using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpOverTime : MonoBehaviour
{
    public float scaleSpeed = 1.0f;
    public Vector3 targetScale = new Vector3(2, 2, 2);

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }
}
