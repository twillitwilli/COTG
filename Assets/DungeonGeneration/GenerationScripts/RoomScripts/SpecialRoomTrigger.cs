using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomTrigger : MonoBehaviour
{
    [SerializeField] private SpecialRoomSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            spawner.ExploredRoom();
            spawner.SpawnRoom(); 
        }
    }
}
