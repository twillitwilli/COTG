using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    private PlayerStats _playerStats;
    private PlayerTotalStats _playerTotalStats;

    public StartingPortal startingPortal;
    public SaveFileSelector[] saveFileSelectors;

    private void Awake()
    {
        LocalGameManager.playerCreated += CheckSaveFiles;
    }

    public void CheckSaveFiles(VRPlayerController player)
    {
        _playerStats = LocalGameManager.instance.GetPlayerStats();
        _playerTotalStats = LocalGameManager.instance.GetTotalStats();

        // foreach (SaveFileSelector fileSelectors in saveFileSelectors) { fileSelectors.CheckSaveFile(); }

        Array.ForEach(saveFileSelectors, loadFiles => loadFiles.CheckSaveFile());
    }

    public void LoadFile(int file, bool fileExists)
    {
        _playerStats.SetSaveFileIndex(file);

        if (fileExists)
        {
            startingPortal.newSaveFile = false;
            _playerTotalStats.LoadPlayerProgress(file);
        }

        else
        {
            startingPortal.newSaveFile = true;
            LocalGameManager.instance.currentGameMode = LocalGameManager.GameMode.tutorial;
        }

        startingPortal.gameObject.SetActive(false);
    }
}
