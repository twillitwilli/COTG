using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSpawnTemplate : MonoBehaviour
{
    [HideInInspector] public enum SpawnType { loadingAreas, rooms, enemies, bosses, rocks, enemyProjectile, playerProjectile, itemScrolls }
    [HideInInspector] public SpawnType spawnType;
    [HideInInspector] public string spawnTypeName;
    [HideInInspector] public bool randomSpawn, isDestroyed;
    [HideInInspector] public int spawnID, spawnCountID;

    private void OnEnable()
    {
        switch (spawnType)
        {
            case SpawnType.loadingAreas:
                spawnTypeName = "LoadingAreas";
                SpawnLoadingArea();
                break;
            case SpawnType.rooms:
                spawnTypeName = "Rooms";
                RoomSpawn();
                break;
            case SpawnType.enemies:
                spawnTypeName = "Enemies";
                break;
            case SpawnType.bosses:
                spawnTypeName = "Bosses";
                break;
            case SpawnType.rocks:
                spawnTypeName = "Rocks";
                break;
            case SpawnType.enemyProjectile:
                spawnTypeName = "EnemyProjectiles";
                break;
            case SpawnType.playerProjectile:
                spawnTypeName = "PlayerProjectile";
                break;
            case SpawnType.itemScrolls:
                spawnTypeName = "ItemScrolls";
                break;
        }
    }

    private void SpawnLoadingArea()
    {

    }

    private void RoomSpawn()
    {
        if (randomSpawn)
        {

        }
        else
        {

        }
    }

    public void ObjectDestroyed()
    {
        isDestroyed = true;
        //signal event that will go to all players that object was destroyed
    }
}
