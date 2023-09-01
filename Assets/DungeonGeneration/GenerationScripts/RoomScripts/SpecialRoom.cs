using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoom : MonoBehaviour
{
    public List<RoomController> roomControllers;
    public List<SpecialRoomSpawnCheck> spawnChecks;

    [HideInInspector] 
    public int specialRoomSpawnCheckers;

    private void Awake()
    {
        DungeonGenerationV3.Instance.specialRoomCount++;
        DungeonGenerationV3.Instance.spawnedRooms.specialRooms.Add(gameObject);
    }

    public void CanSpawnRoomCheck()
    {
        if (specialRoomSpawnCheckers == 0)
        {
            foreach (RoomController controllers in roomControllers)
            {
                controllers.gameObject.SetActive(true);
            }
        }
    }
}
