using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneLocation : MonoBehaviour
{
    public enum sceneLocation { startingArea, tutorial, dungeon}
    public sceneLocation playerLocation;

    private void Awake()
    {
        SwitchScene();
    }

    public void SwitchScene()
    {
        switch (playerLocation)
        {
            case sceneLocation.startingArea:
                break;
            case sceneLocation.tutorial:
                break;
            case sceneLocation.dungeon:
                break;
        }
    }
}
