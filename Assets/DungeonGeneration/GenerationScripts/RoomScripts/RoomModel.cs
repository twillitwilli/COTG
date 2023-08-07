using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomModel : MonoBehaviour
{
    public enum RoomType { normalRoom, deadendRoom, dungeonRoom, dontSpawn, EWRoom, EWCutRoom, SECutRoom, SERoom, SWCutRoom, SWRoom, ECutRoom, ERoom, 
        SCutRoom, SRoom, WCutRoom, WRoom, LongLeftRoom, LongRightRoom, LongRoom, ZigZagRoom }
    public RoomType typeOfRoom;
    public RendererLink rendererLink;
    public GameObject renderedObjects;
    public bool hasConnectedRooms, isEnvironmentController;
    public List<GameObject> walls, wallsWithDoors;
    public List<RoomModel> connectedRoomModels;
    public GameObject[] environmentSpawnGroups;
    [HideInInspector] public bool currentPlayerInRoom, otherPlayerEnteredRoom, roomCleared, roomIDAssigned, disableEnvironmentSpawn;
    [HideInInspector] public int roomID, environmentSpawned;
    [HideInInspector] public Vector3 networkRoomInfo; //roomID, roomType, roomSelection
    [HideInInspector] public Vector2 environmentOrigin = new Vector2(0, 0); //spawnType, objectSpawned
    [HideInInspector] public EnvironmentObjects environmentObjects;

    private void Start()
    {
        DungeonGenerationV3.instance.spawnedRooms.roomModels.Add(this);
    }

    public void SpawnEnvironment()
    {
        if (!disableEnvironmentSpawn && environmentSpawnGroups.Length > 0)
        {
            if (!hasConnectedRooms) { Spawner(); }
            else if (hasConnectedRooms && isEnvironmentController) { Spawner(); }
        }
    }

    private void Spawner()
    {
        environmentSpawned = Random.Range(0, environmentSpawnGroups.Length);
        GameObject newSpawnedGroup = Instantiate(environmentSpawnGroups[environmentSpawned], transform.position, transform.rotation);
        newSpawnedGroup.transform.SetParent(renderedObjects.transform);
        newSpawnedGroup.transform.localPosition = new Vector3(0, 0, 0);
        newSpawnedGroup.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void OnDestroy()
    {
        if (DungeonGenerationV3.instance && DungeonGenerationV3.instance.spawnedRooms.roomModels.Contains(this)) DungeonGenerationV3.instance.spawnedRooms.roomModels.Remove(this);
    }
}
