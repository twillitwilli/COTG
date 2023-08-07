using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMovableSpawnPoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject moveableSpawn = LocalGameManager.instance.GetSpawnLocations()[3].gameObject;
            moveableSpawn.transform.position = other.transform.position;
            moveableSpawn.transform.rotation = other.transform.rotation;
        }
    }
}
