using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpening : MonoBehaviour
{
    public enum Opening { north, south, east, west }
    public Opening openingDirection;
    public RoomController roomController;
    public GameObject mapCounterpart;
    [HideInInspector]
    public bool connectedToRoom;
    private int roomOpeningChecks;

    private void Start()
    {
        DungeonGenerationV3.instance.spawnedRooms.roomOpenings.Add(this);
    }

    public void WhichRoomCheck(bool solidWall)
    {
        switch (openingDirection)
        {
            case Opening.north:
                if (solidWall) { SolidWall(0); }
                else OpeningForRoom(0);
                break;
            case Opening.south:
                if (solidWall) { SolidWall(1); }
                else OpeningForRoom(1);
                break;
            case Opening.east:
                if (solidWall) { SolidWall(2); }
                else OpeningForRoom(2);
                break;
            case Opening.west:
                if (solidWall) { SolidWall(3); }
                else OpeningForRoom(3);
                break;
        }
    }

    private void OpeningForRoom(int roomDirection)
    {
        Destroy(roomController.roomModel.walls[roomDirection].gameObject);
        //roomController.roomModel.walls[roomDirection].gameObject.SetActive(false);
        mapCounterpart.SetActive(true);
        connectedToRoom = true;
        //Destroy(gameObject);
    }

    private void SolidWall(int roomDirection)
    {
        Destroy(roomController.roomModel.wallsWithDoors[roomDirection].gameObject);
        mapCounterpart.SetActive(false);
        connectedToRoom = false;
    }

    public void CheckRoomConnection()
    {
        Collider[] groundobjects = Physics.OverlapBox(transform.position, new Vector3(0.25f, 0.25f, 0.25f), transform.rotation);
        foreach (Collider col in groundobjects) { if (col.gameObject.CompareTag("RoomOpening")) { roomOpeningChecks++; } }
        if(roomOpeningChecks == 2) { WhichRoomCheck(false); }
        else { WhichRoomCheck(true); }
    }

    public void OnDestroy()
    {
        if (DungeonGenerationV3.instance && DungeonGenerationV3.instance.spawnedRooms.roomOpenings.Contains(this)) { DungeonGenerationV3.instance.spawnedRooms.roomOpenings.Remove(this); }
    }
}
