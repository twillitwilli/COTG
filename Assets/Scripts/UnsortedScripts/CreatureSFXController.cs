using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSFXController : MonoBehaviour
{
    public float creatureVolume;

    public void AdjustVolume(float adjustmentValue)
    {
        creatureVolume += adjustmentValue;
        if (creatureVolume < 0) { creatureVolume = 0; }
        else if (creatureVolume > 1) { creatureVolume = 1; }
    }
}
