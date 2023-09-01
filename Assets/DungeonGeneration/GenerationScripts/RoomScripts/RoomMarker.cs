using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMarker : MonoBehaviour
{
    public enum roomType 
    { 
        bossRoom, 
        itemRoom, 
        shopRoom, 
        sacrificeRoom, 
        ritualRoom 
    }
    public roomType dungeonRoomType;

    private void Start()
    {
        if (DungeonBuildParent.Instance != null)
        {
            CompassController.Instance.compassIndicators.Add(gameObject);

            switch (dungeonRoomType)
            {
                case roomType.bossRoom:
                    CompassController.Instance.bossRoom = gameObject;
                    break;

                case roomType.itemRoom:
                    CompassController.Instance.itemRoom = gameObject;
                    break;

                case roomType.shopRoom:
                    CompassController.Instance.shopRoom = gameObject;
                    break;

                case roomType.sacrificeRoom:
                    CompassController.Instance.sacrificeRoom = gameObject;
                    break;

                case roomType.ritualRoom:
                    CompassController.Instance.ritualRoom = gameObject;
                    break;
            }
        }
    }
}
