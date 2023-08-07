using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    public GameObject[] enableObjects;
    public GameObject[] disableObjects;

    private bool buttonPress;

    public void ChangeMenuDisplay()
    {
        Debug.Log("Changing Menu");
        buttonPress = true;
        foreach (GameObject disableObj in disableObjects) { disableObj.SetActive(false); }
    }

    private void OnDisable()
    {
        if (buttonPress) 
        { 
            Invoke("EnableNewMenus", 0.5f);
            buttonPress = false;
        }
    }

    private void EnableNewMenus()
    {
        foreach (GameObject enableObj in enableObjects) { enableObj.SetActive(true); }

    }
}
