using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevModeOnly : MonoBehaviour
{
    private void Start()
    {
        if (!LocalGameManager.instance.IsDevMode()) { Destroy(gameObject); }
    }
}
