using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public GameObject startingRoom;
    public List<GameObject> dungeonRooms, rooms = new List<GameObject>();

    public void AssignRoomID()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i] != null)
            {
                RoomModel checkRoom = rooms[i].GetComponent<RoomModel>();
                if (!checkRoom.roomIDAssigned)
                {
                    checkRoom.roomID = i;
                    checkRoom.roomIDAssigned = true;
                    if (checkRoom.hasConnectedRooms)
                    {
                        foreach (RoomModel connectedRooms in checkRoom.connectedRoomModels)
                        {
                            connectedRooms.roomID = checkRoom.roomID;
                            connectedRooms.roomIDAssigned = true;
                        }
                    }
                }
            }
            else
            {
                rooms.Remove(rooms[i]);
                i--;
            }
        }
    }
}
