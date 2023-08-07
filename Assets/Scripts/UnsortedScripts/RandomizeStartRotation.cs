using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeStartRotation : MonoBehaviour
{
    public bool randomizeX, randomizeY, randomizeZ;

    private void Start()
    {
        if (randomizeX)
        {
            float randomX = Random.Range(-180, 180);
            transform.localEulerAngles = new Vector3(randomX, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        if (randomizeY)
        {
            float randomY = Random.Range(-180, 180);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, randomY, transform.localEulerAngles.z);
        }
        if (randomizeZ)
        {
            float randomZ = Random.Range(-180, 180);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, randomZ);
        }
    }
}
