using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerationV3 : MonoBehaviour
{
    public static DungeonGenerationV3 instance;

    private DungeonBuildParent _dungeonBuildParent;
    private Rooms _rooms;

    public SpawnedRooms spawnedRooms;
    public int roomMinRange, roomMaxRange, dungeonRoomLimit = 5;
    [HideInInspector] public int currentLevel, roomLimitMin, roomLimitMax, roomCount, spawnerCount, specialRoomCount, totalRoomCount;
    private bool dungeonError;
    [HideInInspector] public RoomObjects roomsObjs;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }

        _dungeonBuildParent = DungeonBuildParent.instance;
        _rooms = _dungeonBuildParent.GetRooms();
    }

    private void Start()
    {
        roomsObjs = RoomObjects.instance;
        currentLevel = LocalGameManager.instance.currentLevel;
        if (CoopManager.instance == null || CoopManager.instance != null && LocalGameManager.instance.isHost) { Invoke("DelayStart", 5); }
    }

    private void DelayStart()
    {
        roomLimitMax = Random.Range(roomMinRange + (currentLevel * 2), roomMaxRange + ((currentLevel * 2) + 4));
        roomLimitMin = roomMinRange + (currentLevel * 2);
        MakeStartingRoom();
    }

    private void MakeStartingRoom()
    {
        int startRoom = Random.Range(0, roomsObjs.roomPrefabs[LocalGameManager.instance.dungeonType].roomLists[4].roomCount);
        GameObject spawnedStartingRoom = Instantiate(roomsObjs.roomPrefabs[LocalGameManager.instance.dungeonType].roomLists[4].rooms[startRoom]);
        //room save data
        Vector3 pos = spawnedStartingRoom.transform.localPosition;
        Vector3 rot = spawnedStartingRoom.transform.localEulerAngles;
        if (CoopManager.instance != null) 
        { 
            CoopManager.instance.coopDungeonBuild.AddDungeonRoom(4, startRoom, pos, rot);
            CoopManager.instance.coopDungeonBuild.spawnedRooms.Add(spawnedStartingRoom);
        }

        spawnedStartingRoom.transform.SetParent(spawnedRooms.transform);
        spawnedRooms.startingRoom = spawnedStartingRoom;
        _rooms.startingRoom = spawnedStartingRoom.GetComponent<RoomControllerLink>().roomController.roomModel.gameObject;
    }

    public void DungeonBuildCheck()
    {
        if (spawnerCount == 0) { DungeonGenerationCompleted(); }        
    }

    public void DungeonGenerationCompleted()
    {
        if (roomCount < roomLimitMin) 
        {
            Debug.Log("Room Count Too Low");
            DungeonBuildError(); 
        }
        else
        {
            if (CoopManager.instance == null || CoopManager.instance != null && LocalGameManager.instance.isHost) { CheckDeadendRooms(); }
            CheckRoomOpenings();
            Invoke("ConfigureDungeon", 2f);
        }
    }

    public void DungeonBuildError()
    {
        if (CoopManager.instance != null) CoopManager.instance.coopDungeonBuild.ClearDungeonRoomList();
        dungeonError = true;
        instance = null;
        DungeonBuildParent.instance = null;
        Destroy(gameObject);
    }

    private void CheckDeadendRooms()
    {
        if (spawnedRooms.deadendRooms.Count < dungeonRoomLimit) 
        {
            Debug.Log("Not Enough Dead Ends");
            DungeonBuildError(); 
        }
    }

    private void CheckRoomOpenings()
    {
        for (int i = 0; i < spawnedRooms.roomOpenings.Count; i++)
        {
            spawnedRooms.roomOpenings[i].CheckRoomConnection();
        }
    }

    private void ConfigureDungeon()
    {
        Debug.Log("Configuring Dungeon...");
        if (CoopManager.instance == null || CoopManager.instance != null && LocalGameManager.instance.isHost) 
        {
            AssignDungeonRooms();
            SpawnDungeonRooms();
            RemoveDungeonRoomSpawners();
            ParentRoomModels();
            Invoke("RunDungeonParent", .1f);
        }
    }

    private void AssignDungeonRooms()
    {
        Debug.Log("assigning dungeon rooms");
        for (int i = 0; i < dungeonRoomLimit; i++)
        {
            if (i == 0)
            {
                int lastDeadendRoom = spawnedRooms.deadendRooms.Count - 1;
                spawnedRooms.dungeonRooms.Add(spawnedRooms.deadendRooms[lastDeadendRoom]);
                if (CoopManager.instance != null) 
                {
                    CoopManager.instance.coopDungeonBuild.dungeonRoomCount++;
                    CoopManager.instance.coopDungeonBuild.assignedDungeonRooms.Add(spawnedRooms.deadendRooms[lastDeadendRoom]); 
                }
                Destroy(spawnedRooms.deadendRooms[lastDeadendRoom].GetComponentInChildren<RoomController>().roomModel.gameObject);
                spawnedRooms.deadendRooms.Remove(spawnedRooms.deadendRooms[lastDeadendRoom]);
            }
            else
            {
                int randomRoomSelection = Random.Range(0, spawnedRooms.deadendRooms.Count - 1);
                spawnedRooms.dungeonRooms.Add(spawnedRooms.deadendRooms[randomRoomSelection]);
                if (CoopManager.instance != null) 
                {
                    CoopManager.instance.coopDungeonBuild.dungeonRoomCount++;
                    CoopManager.instance.coopDungeonBuild.assignedDungeonRooms.Add(spawnedRooms.deadendRooms[randomRoomSelection]); 
                }
                Destroy(spawnedRooms.deadendRooms[randomRoomSelection].GetComponentInChildren<RoomController>().roomModel.gameObject);
                spawnedRooms.deadendRooms.Remove(spawnedRooms.deadendRooms[randomRoomSelection]);
            }
        }
    }

    public void SpawnDungeonRooms()
    {
        for (int i = 0; i < spawnedRooms.dungeonRooms.Count; i++)
        {
            Transform spawnLocation = spawnedRooms.dungeonRooms[i].transform;
            int dungeonRoomSelection = Random.Range(0, roomsObjs.roomPrefabs[LocalGameManager.instance.dungeonType].dungeonRoomList[i].dungeonRooms.Count);
            if (roomsObjs.roomPrefabs[LocalGameManager.instance.dungeonType].dungeonRoomList[i].dungeonRooms[dungeonRoomSelection] == null) { Debug.Log("dungeon room selection doesnt exist" + i); }
            GameObject dungeonRoom = Instantiate(roomsObjs.roomPrefabs[LocalGameManager.instance.dungeonType].dungeonRoomList[i].dungeonRooms[dungeonRoomSelection], spawnLocation.position, spawnLocation.rotation);
            _rooms.dungeonRooms.Add(dungeonRoom);
            dungeonRoom.transform.SetParent(_dungeonBuildParent.transform);
            dungeonRoom.GetComponent<RoomMarkerLink>().roomID = new Vector2(i, dungeonRoomSelection); //roomType, roomSelection
        }
    }

    private void RemoveDungeonRoomSpawners()
    {
        foreach (GameObject obj in spawnedRooms.dungeonRooms) { if (obj) { Destroy(obj); } }
        for (int i = 0; i < spawnedRooms.roomOpenings.Count; i++) { if (spawnedRooms.roomOpenings[i]) { Destroy(spawnedRooms.roomOpenings[i].gameObject); } }
    }

    private void ParentRoomModels()
    {
        for (int i = 0; i < spawnedRooms.roomModels.Count; i++)
        {
            if (!_rooms.rooms.Contains(spawnedRooms.roomModels[i].gameObject))
            {
                _rooms.rooms.Add(spawnedRooms.roomModels[i].gameObject);
                spawnedRooms.roomModels[i].transform.SetParent(_dungeonBuildParent.transform);
            } 
        }
    }

    private void RunDungeonParent()
    {
        _dungeonBuildParent.DungeonBuildCompleted();
    }

    public DungeonBuildParent GetDungeonBuildParent()
    {
        return _dungeonBuildParent;
    }

    private void OnDestroy()
    {
        if (dungeonError) { Debug.Log("Dungeon Generation Error"); DungeonGeneratorPrefabCaller.instance.SpawnDungeonGenerator(); }
    }
}
