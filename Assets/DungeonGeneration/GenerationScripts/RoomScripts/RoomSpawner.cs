using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public RoomController roomController;

    private void Awake()
    {
        DungeonGenerationV3.Instance.spawnerCount++;
    }

    private void Start()
    {
        float spawnTime = Random.Range(1000, 3000);

        Task.Delay(Mathf.RoundToInt(spawnTime));

        if (!roomController.disableSpawning && DungeonGenerationV3.Instance.roomCount <= DungeonGenerationV3.Instance.roomLimitMax)
            DetermineSpawnType();

        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
            Destroy(gameObject);
    }

    private void DetermineSpawnType()
    {
        if (DungeonGenerationV3.Instance.roomCount < DungeonGenerationV3.Instance.roomLimitMin)
        {
            int spawnType = Random.Range(0, 100);

            // Deadend Room
            if (spawnType >= 0 && spawnType < 10)
                RoomSpawn(0);

            // 1 Opening Room
            else if (spawnType >= 10 && spawnType < 25)
                RoomSpawn(1);

            // 2 Opening Room
            else if (spawnType >= 25 && spawnType < 45)
                RoomSpawn(2);

            // 3 Opening Room
            else if (spawnType >= 45 && spawnType <= 60)
                RoomSpawn(3);

            // Special Room
            else
                RoomSpawn(5);
        }

        else if (DungeonGenerationV3.Instance.roomCount > DungeonGenerationV3.Instance.roomLimitMin && DungeonGenerationV3.Instance.roomCount < DungeonGenerationV3.Instance.roomLimitMax)
        {
            int spawnType = Random.Range(0, 100);
            
            // Deadend Room
            if (spawnType >= 0 && spawnType < 40)
                RoomSpawn(0);

            //  1 Opening Room
            else if (spawnType >= 40 && spawnType < 60)
                RoomSpawn(1);

            // 2 Opening Room
            else if (spawnType >= 60 && spawnType < 75)
                RoomSpawn(2);

            // 3 Opening Room
            else if (spawnType >= 75 && spawnType <= 80)
                RoomSpawn(3);

            // Special Room
            else
                RoomSpawn(5);
        }

        else
        {
            int canSpawn = Random.Range(0, 100);

            if (canSpawn > 15)
                RoomSpawn(0);
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

        if (MultiplayerManager.Instance.coop) 
        {
            MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.AddDungeonRoom(roomType, roomSelection, pos, rot);
            MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.spawnedRooms.Add(newRoom);
        }

        newRoom.transform.SetParent(DungeonGenerationV3.Instance.spawnedRooms.transform);
    }

    private void OnDestroy()
    {
        DungeonGenerationV3.Instance.spawnerCount--;
        DungeonGenerationV3.Instance.DungeonBuildCheck();
    }
}
