using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMarker : MonoBehaviour
{
    private DungeonBuildParent _dungeonBuildParent;
    private CompassController _compassController;

    public enum roomType { bossRoom, itemRoom, shopRoom, sacrificeRoom, ritualRoom }
    public roomType dungeonRoomType;

    private void Start()
    {
        _dungeonBuildParent = DungeonBuildParent.instance;
        _compassController = _dungeonBuildParent.GetCompassController();

        if (_dungeonBuildParent != null)
        {
            _compassController.compassIndicators.Add(gameObject);

            switch (dungeonRoomType)
            {
                case roomType.bossRoom:
                    _compassController.bossRoom = gameObject;
                    break;
                case roomType.itemRoom:
                    _compassController.itemRoom = gameObject;
                    break;
                case roomType.shopRoom:
                    _compassController.shopRoom = gameObject;
                    break;
                case roomType.sacrificeRoom:
                    _compassController.sacrificeRoom = gameObject;
                    break;
                case roomType.ritualRoom:
                    _compassController.ritualRoom = gameObject;
                    break;
            }
        }

        
    }
}
