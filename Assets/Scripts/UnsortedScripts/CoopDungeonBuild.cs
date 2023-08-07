using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CoopDungeonBuild : MonoBehaviour
{
    [HideInInspector] public CoopManager coopManager;
    [HideInInspector] public PhotonView photonComponent;
    [HideInInspector] public bool loadingAreaSpawned, dungeonCompleted;
    [HideInInspector] public int spawnedObjects, spawnedLoadingArea;
    [HideInInspector] public List<GameObject> spawnedPrefabs = new List<GameObject>();
    [HideInInspector] public List<GameObject> spawnedRooms = new List<GameObject>();
    [HideInInspector] public List<GameObject> assignedDungeonRooms = new List<GameObject>();
    [HideInInspector] public List<bool> dungeonFloorsCompleted = new List<bool>();
    //Dungeon Build
    [HideInInspector] public int totalSpawnedRooms, whichRoom, dungeonRoomCount, nonHostDungeonCount;
    [HideInInspector] public List<int> roomSpawnType = new List<int>();
    [HideInInspector] public List<int> roomSpawnSelection = new List<int>();
    [HideInInspector] public List<Vector3> roomSpawnPos = new List<Vector3>();
    [HideInInspector] public List<Vector3> roomSpawnRot = new List<Vector3>();

    private void OnEnable()
    {
        coopManager = GetComponent<CoopManager>();
        photonComponent = coopManager.photonComponent;
    }

    public void CheckSpawnedLoadingArea()
    {
        photonComponent.RPC("SpawnedLoadingArea", RpcTarget.Others);
    }

    [PunRPC]
    public void SpawnedLoadingArea()
    {
        if (coopManager.isMaster) { photonComponent.RPC("SpawnLoadingArea", RpcTarget.Others, spawnedLoadingArea); }
    }

    [PunRPC]
    public void SpawnLoadingArea(int spawnedLoadingArea)
    {
        if (!loadingAreaSpawned)
        {
            Instantiate(DungeonGeneratorPrefabCaller.instance.loadingAreas[spawnedLoadingArea], transform.position, transform.rotation);
            loadingAreaSpawned = true;
        }
    }

    public void CheckDungeonBuild()
    {
        photonComponent.RPC("DungeonBuildStatus", RpcTarget.Others);
    }

    public void AddDungeonRoom(int roomType, int roomSelection, Vector3 position, Vector3 rotation)
    {
        totalSpawnedRooms++;
        roomSpawnType.Add(roomType);
        roomSpawnSelection.Add(roomSelection);
        roomSpawnPos.Add(position);
        roomSpawnRot.Add(rotation);
    }

    public void ClearDungeonRoomList()
    {
        totalSpawnedRooms = 0;
        dungeonRoomCount = 0;
        nonHostDungeonCount = 0;
        roomSpawnType.Clear();
        roomSpawnSelection.Clear();
        roomSpawnPos.Clear();
        roomSpawnRot.Clear();
        spawnedRooms.Clear();
        assignedDungeonRooms.Clear();
    }

    public void DungeonBuildCheck()
    {
        photonComponent.RPC("DungeonBuildStatus", RpcTarget.Others);
    }


    public void DungeonBuildCompleted()
    {
        if (coopManager.isMaster) { DungeonBuildStatus(); }
    }

    [PunRPC]
    public void DungeonBuildStatus()
    {
        if (coopManager.isMaster && dungeonCompleted)
        {
            int floorCompleted = LocalGameManager.instance.currentLevel;
            int totalRooms = totalSpawnedRooms;
            int totalDungeonRooms = dungeonRoomCount;
            int startingRoom = roomSpawnType[0];
            int startingRoomSelection = roomSpawnSelection[0];
            float posX = roomSpawnPos[0].x;
            float posY = roomSpawnPos[0].y;
            float posZ = roomSpawnPos[0].z;
            float rotX = roomSpawnRot[0].x;
            float rotY = roomSpawnRot[0].y;
            float rotZ = roomSpawnRot[0].z;
            photonComponent.RPC("StartCoopDungeonBuild", RpcTarget.Others, floorCompleted, totalRooms, totalDungeonRooms, startingRoom, startingRoomSelection, posX, posY, posZ, rotX, rotY, rotZ);
        }
    }

    [PunRPC]
    public void BuildCoopDungeon(int whichRoom)
    {
        if (coopManager.isMaster)
        {
            int roomType = roomSpawnType[whichRoom];
            int roomSelection = roomSpawnSelection[whichRoom];
            float posX = roomSpawnPos[whichRoom].x;
            float posY = roomSpawnPos[whichRoom].y;
            float posZ = roomSpawnPos[whichRoom].z;
            float rotX = roomSpawnRot[whichRoom].x;
            float rotY = roomSpawnRot[whichRoom].y;
            float rotZ = roomSpawnRot[whichRoom].z;
            photonComponent.RPC("SpawnRoom", RpcTarget.Others, roomType, roomSelection, posX, posY, posZ, rotX, rotY, rotZ);
        }
    }

    [PunRPC]
    public void StartCoopDungeonBuild(int floorCompleted, int totalRooms, int totalDungeonRooms, int startingRoom, int startingSelection, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
    {
        LocalGameManager.instance.currentLevel = floorCompleted;
        totalSpawnedRooms = totalRooms;
        dungeonRoomCount = totalDungeonRooms;
        SpawnRoom(startingRoom, startingSelection, posX, posY, posZ, rotX, rotY, rotZ);
    }

    [PunRPC]
    public void SpawnRoom(int roomType, int roomSelection, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
    {
        whichRoom++;
        if (whichRoom == totalSpawnedRooms)
        {
            photonComponent.RPC("GetDungeonRooms", RpcTarget.Others);
        }
        else
        {
            GameObject newRoomSpawn = Instantiate(RoomObjects.instance.roomPrefabs[LocalGameManager.instance.dungeonType].roomLists[roomType].rooms[roomSelection]);
            DisableSpawning(newRoomSpawn);
            Vector3 pos = new Vector3(posX, posY, posZ);
            Vector3 rot = new Vector3(rotX, rotY, rotZ);
            newRoomSpawn.transform.localPosition = pos;
            newRoomSpawn.transform.localEulerAngles = rot;
            newRoomSpawn.transform.SetParent(DungeonGenerationV3.instance.spawnedRooms.transform);
            photonComponent.RPC("BuildCoopDungeon", RpcTarget.Others, whichRoom);
        }
    }

    public void DisableSpawning(GameObject newRoom)
    {
        if (newRoom.GetComponent<RoomControllerLink>())
        {
            RoomControllerLink controllerLink = newRoom.GetComponent<RoomControllerLink>();
            controllerLink.roomController.disableSpawning = true;
        }
        else if (newRoom.GetComponent<SpecialRoom>())
        {
            SpecialRoom specialRoomSettings = newRoom.GetComponent<SpecialRoom>();
            foreach (RoomController controller in specialRoomSettings.roomControllers)
            {
                controller.disableSpawning = true;
            }
        }
    }

    [PunRPC]
    public void AssignDungeonRoom(int dungeonRoom)
    {
        DungeonGenerationV3.instance.spawnedRooms.dungeonRooms.Add(spawnedRooms[dungeonRoom]);
        assignedDungeonRooms.Add(spawnedRooms[dungeonRoom]);
        nonHostDungeonCount++;
        if (nonHostDungeonCount == dungeonRoomCount)
        {
            DungeonGenerationV3.instance.SpawnDungeonRooms();
            dungeonCompleted = true;
        }
    }

    [PunRPC]
    public void GetDungeonRooms()
    {
        if (coopManager.isMaster)
        {
            for (int i = 0; i < assignedDungeonRooms.Count; i++)
            {
                for (int i2 = 0; i2 < spawnedRooms.Count; i2++)
                {
                    if (spawnedRooms[i2] == assignedDungeonRooms[i])
                    {
                        photonComponent.RPC("AssignDungeonRoom", RpcTarget.Others, i2);
                    }
                }
            }
        }
    }

    public RoomModel GetLocalRoom(int roomID)
    {
        if (DungeonBuildParent.instance != null)
        {
            for (int i = 0; i < DungeonBuildParent.instance.rooms.rooms.Count; i++)
            {
                if (roomID == DungeonBuildParent.instance.rooms.rooms[i].GetComponent<RoomModel>().roomID)
                {
                    RoomModel foundRoom = DungeonBuildParent.instance.rooms.rooms[i].GetComponent<RoomModel>();
                    return foundRoom;
                }
            }
        }
        Debug.Log("Error Couldnt Find Room");
        return null;
    }
}
