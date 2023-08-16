using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileSelector : MonoBehaviour
{
    [SerializeField] private int saveFile;
    public Text textBox;
    [HideInInspector] public bool fileExists;

    public void CheckSaveFile()
    {
        if (BinarySaveSystem.LoadTotalStats(saveFile) != null) 
        {
            fileExists = true;
            textBox.text = "Load Save #" + saveFile; 
        }
        else { textBox.text = "Empty Save File #" + saveFile; }
    }
}
