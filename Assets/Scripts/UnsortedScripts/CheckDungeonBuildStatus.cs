using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDungeonBuildStatus : MonoBehaviour
{
    public List<GameObject> portal;
    private bool buildStatusCompleted;

    public void LateUpdate()
    {
        if (!buildStatusCompleted && LocalGameManager.instance.dungeonBuildCompleted)
        {
            foreach (GameObject obj in portal) { obj.SetActive(true); }
            buildStatusCompleted = true;
        }
    }
}
