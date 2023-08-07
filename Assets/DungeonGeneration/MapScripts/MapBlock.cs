using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    private MapController _mapController;

    private void Start()
    {
        _mapController = DungeonBuildParent.instance.GetMapController();
        _mapController.mapBlocks.Add(gameObject);
    }
}
