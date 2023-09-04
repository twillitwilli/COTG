using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    [HideInInspector]
    public VRPlayerController player;

    public StartingPortal startingPortal;
    public SaveFileSelector[] saveFileSelectors;

    private void Awake()
    {
        LocalGameManager.playerCreated += CheckSaveFiles;
    }

    public void CheckSaveFiles(VRPlayerController newPlayer)
    {
        player = newPlayer;

        // foreach (SaveFileSelector fileSelectors in saveFileSelectors) { fileSelectors.CheckSaveFile(); }

        Array.ForEach(saveFileSelectors, loadFiles => loadFiles.CheckSaveFile());
    }

    public void LoadFile(int file, bool fileExists)
    {
        PlayerStats.Instance.SetSaveFileIndex(file);

        if (fileExists)
        {
            startingPortal.newSaveFile = false;
            PlayerTotalStats.Instance.LoadPlayerProgress(file);
        }

        else
        {
            startingPortal.newSaveFile = true;
            LocalGameManager.Instance.currentGameMode = LocalGameManager.GameMode.tutorial;
        }

        startingPortal.gameObject.SetActive(false);
    }
}
