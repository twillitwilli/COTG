using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFile : MonoBehaviour
{
    [SerializeField] private SaveFileManager _fileManager;
    [SerializeField] private int _saveFile;

    public void LoadSelectedFile()
    {
        bool fileExists = BinarySaveSystem.LoadPlayerProgressStats(_saveFile) != null ? true : false;
        _fileManager.LoadFile(_saveFile, fileExists);
    }
}
