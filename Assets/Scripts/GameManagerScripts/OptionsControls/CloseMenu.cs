using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    public void ClosePlayerMenu()
    {
        if (PlayerMenu.instance != null) { Destroy(PlayerMenu.instance.gameObject); }
    }
}