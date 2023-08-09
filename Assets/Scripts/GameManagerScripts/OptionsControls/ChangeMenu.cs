using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ChangeMenu : MonoBehaviour
{
    public GameObject[] enableObjects;
    public GameObject[] disableObjects;

    private bool buttonPress;

    public void ChangeMenuDisplay()
    {
        buttonPress = true;



        foreach (GameObject disableObj in disableObjects) { disableObj.SetActive(false); }
    }

    private async void OnDisable()
    {
        if (buttonPress) 
        {
            await Task.Delay(500);

            EnableNewMenus();

            buttonPress = false;
        }
    }

    private void EnableNewMenus()
    {
        foreach (GameObject enableObj in enableObjects) { enableObj.SetActive(true); }

    }
}
