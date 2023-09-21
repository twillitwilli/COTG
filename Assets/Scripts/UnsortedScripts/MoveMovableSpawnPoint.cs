using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMovableSpawnPoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LocalGameManager.Instance.moveableSpawnPoint.position = other.transform.position;
            LocalGameManager.Instance.moveableSpawnPoint.rotation = other.transform.rotation;
        }
    }
}
