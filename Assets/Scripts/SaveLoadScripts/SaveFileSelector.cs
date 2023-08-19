using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileSelector : MonoBehaviour
{
    [SerializeField] private int _saveFile;
    [SerializeField] private Text _textBox;

    public void CheckSaveFile()
    {
        string text = BinarySaveSystem.LoadPlayerProgressStats(_saveFile) != null ? "Load Save #" + _saveFile : "Empty Save File";
        _textBox.text = text;
    }
}
