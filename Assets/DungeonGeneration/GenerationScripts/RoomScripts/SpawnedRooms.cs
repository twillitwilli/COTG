using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedRooms : MonoBehaviour
{
    public GameObject startingRoom;

    //room index (0 - starting, 1 - boss, 2 - item, 3 - shop, 4 - sacrifice, 5 - ritual)
    public List<GameObject> dungeonRooms;
    public List<GameObject> spawnedRooms, deadendRooms, specialRooms, hallwayConnectionCheck = new List<GameObject>();
    public List<RoomOpening> roomOpenings = new List<RoomOpening>();
    public List<RoomModel> roomModels = new List<RoomModel>();
}
