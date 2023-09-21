using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using unityEngine = UnityEngine;
using QTArts.AbstractClasses;


public class DungeonGenerationV3 : MonoSingleton<DungeonGenerationV3>
{

    private DungeonBuildParent _dungeonBuildParent;
    private Rooms _rooms;

    public SpawnedRooms spawnedRooms;
    public int roomMinRange, roomMaxRange, dungeonRoomLimit = 5;
    
    [HideInInspector] 
    public int currentLevel, roomLimitMin, roomLimitMax, roomCount, spawnerCount, specialRoomCount, totalRoomCount;
    
    private bool dungeonError;
    private string _errorMessage;
    
    [HideInInspector] 
    public RoomObjects roomsObjs;

    private void Awake()
    {
        _rooms = _dungeonBuildParent.GetRooms();
    }

    private void Start()
    {
        roomsObjs = RoomObjects.instance;
        currentLevel = LocalGameManager.Instance.currentLevel;

        if (!MultiplayerManager.Instance.coop || !MultiplayerManager.Instance.coop && LocalGameManager.Instance.isHost)
        {
            Task.Delay(5000);

            roomLimitMax = unityEngine::Random.Range(roomMinRange + (currentLevel * 2), roomMaxRange + ((currentLevel * 2) + 4));
            roomLimitMin = roomMinRange + (currentLevel * 2);

            MakeStartingRoom();
        }
    }

    private void MakeStartingRoom()
    {
        int startRoom = unityEngine::Random.Range(0, roomsObjs.roomPrefabs[LocalGameManager.Instance.dungeonType].roomLists[4].roomCount);
        GameObject spawnedStartingRoom = Instantiate(roomsObjs.roomPrefabs[LocalGameManager.Instance.dungeonType].roomLists[4].rooms[startRoom]);
        
        //room save data
        Vector3 pos = spawnedStartingRoom.transform.localPosition;
        Vector3 rot = spawnedStartingRoom.transform.localEulerAngles;

        if (MultiplayerManager.Instance.coop) 
        { 
            MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.AddDungeonRoom(4, startRoom, pos, rot);
            MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.spawnedRooms.Add(spawnedStartingRoom);
        }

        spawnedStartingRoom.transform.SetParent(spawnedRooms.transform);
        spawnedRooms.startingRoom = spawnedStartingRoom;
        _rooms.startingRoom = spawnedStartingRoom.GetComponent<RoomControllerLink>().roomController.roomModel.gameObject;
    }

    public void DungeonBuildCheck()
    {
        if (spawnerCount == 0)
            CheckForDungeonErrors();
    }

    private async void CheckForDungeonErrors()
    {
        if (roomCount < roomLimitMin)
        {
            DungeonBuildError("Dungeon Room Count Too Low");
            return;
        }   

        else if (spawnedRooms.deadendRooms.Count < dungeonRoomLimit)
        {
            DungeonBuildError("Not Enough Deadend Rooms");
            return;
        }

        await Task.Delay(2000);

        ConfigureDungeon();
    }

    public void DungeonBuildError(string errorMsg)
    {
        dungeonError = true;

        Destroy(gameObject);
    }

    private async void ConfigureDungeon()
    {
        Debug.Log("Configuring Dungeon...");

        if (!MultiplayerManager.Instance.coop || MultiplayerManager.Instance.coop && LocalGameManager.Instance.isHost) 
        {
            await CheckRoomOpenings();

            await AssignDungeonRooms();
            
            await SpawnDungeonRooms();
            
            await RemoveDungeonRoomSpawners();
            
            await ParentRoomModels();

            RunDungeonParent();
        }
    }

    private async Task CheckRoomOpenings()
    {
        for (int i = 0; i < spawnedRooms.roomOpenings.Count; i++)
        {
            spawnedRooms.roomOpenings[i].CheckRoomConnection();
        }
    }

    private async Task AssignDungeonRooms()
    {
        Debug.Log("assigning dungeon rooms");
        for (int i = 0; i < dungeonRoomLimit; i++)
        {
            if (i == 0)
            {
                int lastDeadendRoom = spawnedRooms.deadendRooms.Count - 1;
                spawnedRooms.dungeonRooms.Add(spawnedRooms.deadendRooms[lastDeadendRoom]);

                if (MultiplayerManager.Instance.coop) 
                {
                    MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.dungeonRoomCount++;
                    MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.assignedDungeonRooms.Add(spawnedRooms.deadendRooms[lastDeadendRoom]); 
                }

                Destroy(spawnedRooms.deadendRooms[lastDeadendRoom].GetComponentInChildren<RoomController>().roomModel.gameObject);
                spawnedRooms.deadendRooms.Remove(spawnedRooms.deadendRooms[lastDeadendRoom]);
            }

            else
            {
                int randomRoomSelection = unityEngine::Random.Range(0, spawnedRooms.deadendRooms.Count - 1);
                spawnedRooms.dungeonRooms.Add(spawnedRooms.deadendRooms[randomRoomSelection]);

                if (MultiplayerManager.Instance.coop) 
                {
                    MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.dungeonRoomCount++;
                    MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.assignedDungeonRooms.Add(spawnedRooms.deadendRooms[randomRoomSelection]); 
                }

                Destroy(spawnedRooms.deadendRooms[randomRoomSelection].GetComponentInChildren<RoomController>().roomModel.gameObject);
                spawnedRooms.deadendRooms.Remove(spawnedRooms.deadendRooms[randomRoomSelection]);
            }
        }
    }

    public async Task SpawnDungeonRooms()
    {
        for (int i = 0; i < spawnedRooms.dungeonRooms.Count; i++)
        {
            Transform spawnLocation = spawnedRooms.dungeonRooms[i].transform;

            int dungeonRoomSelection = unityEngine::Random.Range(0, roomsObjs.roomPrefabs[LocalGameManager.Instance.dungeonType].dungeonRoomList[i].dungeonRooms.Count);
            
            if (roomsObjs.roomPrefabs[LocalGameManager.Instance.dungeonType].dungeonRoomList[i].dungeonRooms[dungeonRoomSelection] == null)
                Debug.Log("dungeon room selection doesnt exist" + i);

            GameObject dungeonRoom = Instantiate(roomsObjs.roomPrefabs[LocalGameManager.Instance.dungeonType].dungeonRoomList[i].dungeonRooms[dungeonRoomSelection], spawnLocation.position, spawnLocation.rotation);
            _rooms.dungeonRooms.Add(dungeonRoom);
            dungeonRoom.transform.SetParent(_dungeonBuildParent.transform);
            dungeonRoom.GetComponent<RoomMarkerLink>().roomID = new Vector2(i, dungeonRoomSelection); //roomType, roomSelection
        }
    }

    private async Task RemoveDungeonRoomSpawners()
    {
        foreach (GameObject obj in spawnedRooms.dungeonRooms) 
        {
            if (obj != null)
                Destroy(obj);
        }

        for (int i = 0; i < spawnedRooms.roomOpenings.Count; i++) 
        { 
            if (spawnedRooms.roomOpenings[i])
                Destroy(spawnedRooms.roomOpenings[i].gameObject);
        }
    }

    private async Task ParentRoomModels()
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
        if (dungeonError) 
        { 
            Debug.Log("Dungeon Generation Error: " + _errorMessage); 
            DungeonGeneratorPrefabCaller.instance.SpawnDungeonGenerator(); 
        }
    }
}
