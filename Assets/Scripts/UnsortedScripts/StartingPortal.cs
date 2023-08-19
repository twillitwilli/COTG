using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPortal : MonoBehaviour
{
    [HideInInspector] public bool newSaveFile;
    [SerializeField] private LoadingPortal portal;
    [SerializeField] private GameObject[] portalObjs;

    private void OnDisable()
    {
        if (newSaveFile)
        {
            portal.movePlayerInScene = false;
            portal.GameMode = LoadingPortal.GameType.Tutorial;
        }
        else
        {
            portal.movePlayerInScene = true;
            portal.GameMode = LoadingPortal.GameType.moveToSpawn;
        }

        //foreach (GameObject objs in portalObjs) { objs.SetActive(true); }

        Array.ForEach(portalObjs, enableObjects => enableObjects.SetActive(true));
    }
}
