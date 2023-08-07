using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSavedDungeon : MonoBehaviour
{
    public GameObject portal;

    private void Start()
    {
        if (LocalGameManager.instance.savedDungeon) { portal.SetActive(true); }
        else { portal.SetActive(false); }
    }
}
