using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarot : MonoBehaviour
{
    public static Tarot instance;

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
