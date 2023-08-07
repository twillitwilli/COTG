using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuideSpawner : MonoBehaviour
{
    [SerializeField] private PressurePlateTrigger pressurePlate;
    [SerializeField] private GameObject enlil;
    [SerializeField] private EnlilController controller;
    private bool spawnedEnlil;

    private void LateUpdate()
    {
        if (!spawnedEnlil && pressurePlate.pressurePlateState == 1)
        {
            enlil.SetActive(true);
            spawnedEnlil = true;
        }
        else if (spawnedEnlil && pressurePlate.pressurePlateState == 0)
        {
            enlil.SetActive(false);
            spawnedEnlil = false;
        }
    }
}
