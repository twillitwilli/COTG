using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conjurer : MonoBehaviour
{
    public static Conjurer instance;
    [SerializeField] private BowMagicController _bowController;

    private void Start()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
    }

    public BowMagicController GetBowController() { return _bowController; }

    private void OnDestroy()
    {
        if (instance == this) { instance = null; }
    }
}
