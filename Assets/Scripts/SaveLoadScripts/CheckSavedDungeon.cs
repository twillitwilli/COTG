using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSavedDungeon : MonoBehaviour
{
    public GameObject portal;

    private void Start()
    {
        bool activatePortal = BinarySaveSystem.LoadDungeon(PlayerStats.Instance.saveFile) != null ? true : false;
        portal.SetActive(activatePortal);
    }
}
