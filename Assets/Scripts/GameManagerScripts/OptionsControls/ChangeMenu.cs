using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ChangeMenu : MonoBehaviour
{
    [SerializeField] public GameObject[] enableObjects;
    [SerializeField] public GameObject[] disableObjects;

    private bool _buttonPress;

    public async void ChangeMenuDisplay()
    {
        if (!_buttonPress)
        {
            _buttonPress = true;

            await EnableNewMenus();

            _buttonPress = false;

            Array.ForEach(disableObjects, disableObj => disableObj.SetActive(false));
        }
    }

    private async Task EnableNewMenus()
    {
        // foreach (GameObject enableObj in enableObjects) { enableObj.SetActive(true); }

        Array.ForEach(enableObjects, enableObj => enableObj.SetActive(true));
    }
}
