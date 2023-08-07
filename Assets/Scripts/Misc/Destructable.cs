using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public float breakableForce;
    public GameObject destroyedObject;
    public bool spawnOnDestroy;
    public GameObject[] spawnObjects;

    private bool isBroken;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isBroken && collision.relativeVelocity.magnitude >= breakableForce)
        {
            isBroken = true;

            if (spawnOnDestroy)
            {
                SpawnObjects();
            }

            Break();
        }
    }

    private void SpawnObjects()
    {
        foreach (GameObject spawnedObjects in spawnObjects)
        {
            Instantiate(spawnedObjects, transform.position, transform.rotation);
        }
    }

    private void Break()
    {
        Instantiate(destroyedObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
