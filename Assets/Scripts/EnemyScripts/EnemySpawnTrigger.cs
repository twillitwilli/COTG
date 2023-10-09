using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (other.gameObject.GetComponent<VRPlayer>()) { other.gameObject.GetComponent<VRPlayer>().roomID = enemySpawner.room.roomID; }
            enemySpawner.ExploredRoom(false);
            enemySpawner.SpawnType(false); 
        }
    }
}
