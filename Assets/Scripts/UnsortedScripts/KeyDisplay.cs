using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDisplay : MonoBehaviour
{
    public ParticleSystem keyDisplayEffect;

    public void UpdateKeyDisplay(int keyValue)
    {
        var maxParticles = keyDisplayEffect.main;
        maxParticles.maxParticles = keyValue;
        var currentKeys = keyDisplayEffect.emission;
        currentKeys.rateOverTime = keyValue;
    }
}
