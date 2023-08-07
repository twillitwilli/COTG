using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFile : MonoBehaviour
{
    [SerializeField] private SaveFileManager fileManager;
    public int saveFile;

    public void LoadSelectedFile()
    {
        fileManager.LoadFile(saveFile);
    }
}
