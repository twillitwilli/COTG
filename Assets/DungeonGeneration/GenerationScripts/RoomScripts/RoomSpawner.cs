using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public RoomController roomController;
    private DungeonGenerationV3 dungeonGeneration;

    private void Awake()
    {
        dungeonGeneration = DungeonGenerationV3.instance;
        dungeonGeneration.spawnerCount++;
    }

    private void Start()
    {
        Invoke("SpawnType", Random.Range(1f, 3f));
        Destroy(gameObject, 3);
    }

    private void SpawnType()
    {
        if (!roomController.disableSpawning)
        {
            if (dungeonGeneration.roomCount <= dungeonGeneration.roomLimitMax) { DetermineSpawnType(); }
            else { Destroy(gameObject); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject) { Destroy(gameObject); }
    }

    private void DetermineSpawnType()
    {
        if (dungeonGeneration.roomCount < dungeonGeneration.roomLimitMin)
        {
            int spawnType = Random.Range(0, 100);
            if (spawnType >= 0 && spawnType < 10) { RoomSpawn(0); } //deadend room
            else if (spawnType >= 10 && spawnType < 25) { RoomSpawn(1); } //1 opening room
            else if (spawnType >= 25 && spawnType < 45) { RoomSpawn(2); } //2 opening room
            else if (spawnType >= 45 && spawnType <= 60) { RoomSpawn(3); } //3 opening room
            else { RoomSpawn(5); } //special room
        }
        else if (dungeonGeneration.roomCount > dungeonGeneration.roomLimitMin && dungeonGeneration.roomCount < dungeonGeneration.roomLimitMax)
        {
            int spawnType = Random.Range(0, 100);
            if (spawnType >= 0 && spawnType < 40) { RoomSpawn(0); } //deadend room
            else if (spawnType >= 40 && spawnType < 60) { RoomSpawn(1); } //1 opening room
            else if (spawnType >= 60 && spawnType < 75) { RoomSpawn(2); } //2 opening room
            else if (spawnType >= 75 && spawnType <= 80) { RoomSpawn(3); } //3 opening room
            else { RoomSpawn(5); } //special room
        }
        else
        {
            int canSpawn = Random.Range(0, 100);
            if (canSpawn > 15) { RoomSpawn(0); }
        }
    }

    public void RoomSpawn(int roomType)
    {
        int roomSelection = Random.Range(0, RoomObjects.instance.roomPrefabs[LocalGameManager.Instance.dungeonType].roomLists[roomType].roomCount - 1);
        GameObject newRoom = Instantiate(RoomObjects.instance.roomPrefabs[LocalGameManager.Instance.dungeonType].roomLists[roomType].rooms[roomSelection], transform.position, transform.rotation);
        newRoom.transform.LookAt(roomController.transform);

        //room save data
        Vector2 roomData = new Vector2(roomType, roomSelection);
        Vector3 pos = newRoom.transform.localPosition;
        Vector3 rot = newRoom.transform.localEulerAngles;
        if (CoopManager.instance != null) 
        { 
            CoopManager.instance.coopDungeonBuild.AddDungeonRoom(roomType, roomSelection, pos, rot);
            CoopManager.instance.coopDungeonBuild.spawnedRooms.Add(newRoom);
        }

        newRoom.transform.SetParent(dungeonGeneration.spawnedRooms.transform);
    }

    private void OnDestroy()
    {
        dungeonGeneration.spawnerCount--;
        dungeonGeneration.DungeonBuildCheck();
    }
}
