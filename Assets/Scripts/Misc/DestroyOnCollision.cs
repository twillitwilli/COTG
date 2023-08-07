using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public string destroyObjectWithThisTag;

    private GameObject objectThatHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(destroyObjectWithThisTag))
        {
            objectThatHit = collision.gameObject;
            Destroy(objectThatHit);
            Debug.Log("collided");
        }
    }
}
