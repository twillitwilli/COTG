using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : MonoBehaviour
{
    public static Warlock instance;

    private void Start()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
    }

    private void OnDestroy()
    {
        if (instance == this) { instance = null; }
    }
}
