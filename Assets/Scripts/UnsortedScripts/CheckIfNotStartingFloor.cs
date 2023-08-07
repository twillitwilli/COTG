using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfNotStartingFloor : MonoBehaviour
{
    private void Start()
    {
        if (LocalGameManager.instance.currentLevel == 1) { Destroy(gameObject); }
    }
}
