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

        foreach (SaveFileSelector fileSelectors in saveFileSelectors) { fileSelectors.CheckSaveFile(); }
    }

    public void LoadFile(int file)
    {
        _playerStats.SetSaveFileIndex(file);

        if (saveFileSelectors[file - 1].fileExists)
        {
            startingPortal.newSaveFile = false;
            _playerTotalStats.Load(_playerStats.GetSaveFileIndex());
        }

        else
        {
            startingPortal.newSaveFile = true;
            LocalGameManager.instance.inTutorial = true;
        }

        startingPortal.gameObject.SetActive(false);
    }
}
