using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    private void Start()
    {
        MapController.Instance.mapBlocks.Add(gameObject);
    }
}
