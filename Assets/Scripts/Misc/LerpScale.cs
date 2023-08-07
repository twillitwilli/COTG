using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScale : MonoBehaviour
{
    public Vector3 scaleTo;
    public float scaleTime;

    private void Update()
    {
        float xScale = Mathf.Lerp(transform.localScale.x, scaleTo.x, Time.deltaTime / scaleTime);
        float yScale = Mathf.Lerp(transform.localScale.y, scaleTo.y, Time.deltaTime / scaleTime);
        float zScale = Mathf.Lerp(transform.localScale.z, scaleTo.z, Time.deltaTime / scaleTime);

        transform.localScale = new Vector3(xScale, yScale, zScale);
    }
}
