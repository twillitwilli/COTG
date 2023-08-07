using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteAdjustment : MonoBehaviour
{
    [SerializeField] private Animator vignette;

    public void ChangeColor(int vignetteState)
    {
        vignette.SetInteger("VignetteState", vignetteState);
    }
}
