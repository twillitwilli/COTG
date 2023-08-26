using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToSpawnLocation : MonoBehaviour
{
    public int whichSpawnLocation;

    public void Start()
    {
        VRPlayerController player = LocalGameManager.Instance.player;
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
