using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonSaveSystem : MonoBehaviour
{
    private DungeonGenerationV3 _dungeonGeneration;
    private DungeonBuildParent _dungeonBuildParent;

    [HideInInspector] public int totalRoomCount;
    [HideInInspector] public List<int> roomType = new List<int>(), roomSelection = new List<int>(), isDungeonRoom = new List<int>();
    [HideInInspector] public List<float> xPos = new List<float>(), yPos = new List<float>(), zPos = new List<float>(), xRot = new List<float>(), yRot = new List<float>(), zRot = new List<float>();

    //New Save Data
    [HideInInspector] public List<int> roomSpawnType = new List<int>();
    [HideInInspector] public List<int> roomSpawnSelection = new List<int>();
    [HideInInspector] public List<Vector3> roomSpawnPos = new List<Vector3>();
    [HideInInspector] public List<Vector3> roomSpawnRot = new List<Vector3>();

    private void Start()
    {
        _dungeonGeneration = DungeonGenerationV3.instance;
        _dungeonBuildParent = _dungeonGeneration.GetDungeonBuildParent();
    }

    public void SaveRoomSpawn(int roomType, int roomSelection, Vector3 position, Vector3 rotation)
    {
        totalRoomCount++;
        roomSpawnType.Add(roomType);
        roomSpawnSelection.Add(roomSelection);
        roomSpawnPos.Add(position);
        roomSpawnRot.Add(rotation);
    }

    public void ClearRoomList()
    {
        totalRoomCount = 0;
        roomSpawnType.Clear();
        roomSpawnSelection.Clear();
        roomSpawnPos.Clear();
        roomSpawnRot.Clear();
    }

    public void SaveDungeon()
    {
        PlayerPrefs.SetInt("TotalRoomCount", totalRoomCount);

        for (int i = 0; i < totalRoomCount; i++)
        {
            PlayerPrefs.SetInt("RoomType" + i, roomType[i]);
            PlayerPrefs.SetInt("RoomSelection" + i, roomSelection[i]);
            PlayerPrefs.SetInt("IsDungeonRoom" + i, isDungeonRoom[i]);
            //vector3 pos
            PlayerPrefs.SetFloat("xPos" + i, xPos[i]);
            PlayerPrefs.SetFloat("yPos" + i, yPos[i]);
            PlayerPrefs.SetFloat("zPos" + i, zPos[i]);
            //vector3 rot
            PlayerPrefs.SetFloat("xRot" + i, xRot[i]);
            PlayerPrefs.SetFloat("yRot" + i, yRot[i]);
            PlayerPrefs.SetFloat("zRot" + i, zRot[i]);
        }
    }

    public void LoadSavedDungeon()
    {
        totalRoomCount = PlayerPrefs.GetInt("TotalRoomCount");

        for (int i = 0; i < totalRoomCount; i++)
        {
            roomType.Add(PlayerPrefs.GetInt("RoomType" + i));
            roomSelection.Add(PlayerPrefs.GetInt("RoomSelection" + i));
            isDungeonRoom.Add(PlayerPrefs.GetInt("IsDungeonRoom" + i));
            //vector3 pos
            xPos.Add(PlayerPrefs.GetFloat("xPos" + i));
            yPos.Add(PlayerPrefs.GetFloat("yPos" + i));
            zPos.Add(PlayerPrefs.GetFloat("zPos" + i));
            //vector3 rot
            xRot.Add(PlayerPrefs.GetFloat("xRot" + i));
            yRot.Add(PlayerPrefs.GetFloat("yRot" + i));
            zRot.Add(PlayerPrefs.GetFloat("zRot" + i));
        }

        SpawnRooms();
    }

    private void SpawnRooms()
    {
        for (int i = 0; i < totalRoomCount; i++)
        {
            if (isDungeonRoom[i] == 0)
            {
                GameObject newRoom = Instantiate(RoomObjects.instance.roomPrefabs[LocalGameManager.Instance.dungeonType].roomLists[roomType[i]].rooms[roomSelection[i]]);
                AdjustTransform(newRoom, i);
                newRoom.transform.SetParent(_dungeonGeneration.spawnedRooms.transform);
                AdjustRoom(newRoom);
            }
            else
            {
                GameObject newRoom = Instantiate(RoomObjects.instance.roomPrefabs[LocalGameManager.Instance.dungeonType].dungeonRoomList[roomType[i]].dungeonRooms[roomSelection[i]]);
                newRoom.transform.SetParent(_dungeonBuildParent.transform);
                AdjustTransform(newRoom, i);
            }
        }

        Invoke("DungeonCompleted", 1f);
    }

    private void AdjustTransform(GameObject newRoom, int i)
    {
        Vector3 newPos = new Vector3(xPos[i], yPos[i], zPos[i]);
        newRoom.GetComponent<Transform>().position = newPos;

        Vector3 newRot = new Vector3(xRot[i], yRot[i], zRot[i]);
        newRoom.GetComponent<Transform>().eulerAngles = newRot;
    }

    private void AdjustRoom(GameObject room)
    {
        if (room.GetComponent<RoomControllerLink>())
        {
            RoomController controller = room.GetComponentInChildren<RoomController>();
            foreach (GameObject obj in controller.roomSpawners) { Destroy(obj); }
        }
        else
        {
            SpecialRoom specialRoom = room.GetComponent<SpecialRoom>();

            foreach (SpecialRoomSpawnCheck spawnChecks in specialRoom.spawnChecks) { Destroy(spawnChecks.gameObject); }
            foreach (RoomController controller in specialRoom.roomControllers) 
            { 
                foreach (GameObject obj in controller.roomSpawners) { Destroy(obj); }
                controller.gameObject.SetActive(true);
            }
        }
    }

    private void DungeonCompleted()
    {
        DungeonGenerationV3.instance.DungeonGenerationCompleted();
    }
}
