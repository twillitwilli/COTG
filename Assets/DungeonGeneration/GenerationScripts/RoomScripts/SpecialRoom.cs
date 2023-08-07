using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoom : MonoBehaviour
{
    public List<RoomController> roomControllers;
    public List<SpecialRoomSpawnCheck> spawnChecks;

    [HideInInspector] public int specialRoomSpawnCheckers;

    private void Awake()
    {
        DungeonGenerationV3.instance.specialRoomCount++;
        DungeonGenerationV3.instance.spawnedRooms.specialRooms.Add(gameObject);
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
