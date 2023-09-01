using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomSpawnCheck : MonoBehaviour
{
    public SpecialRoom specialRoom;

    private void Awake()
    {
        specialRoom.specialRoomSpawnCheckers++;
    }

    private void Start()
    {
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            DungeonGenerationV3.Instance.spawnedRooms.specialRooms.Remove(specialRoom.gameObject);
            DungeonGenerationV3.Instance.specialRoomCount--;
            Destroy(specialRoom.gameObject);
        }
    }

    private void OnDestroy()
    {
        specialRoom.specialRoomSpawnCheckers--;
        specialRoom.CanSpawnRoomCheck();
    }
}
