using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotHardMode : MonoBehaviour
{
    private void Start()
    {
        if (!LocalGameManager.instance.hardMode) { gameObject.SetActive(false); }
    }
}
