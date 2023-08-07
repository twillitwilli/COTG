using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHandColor : MonoBehaviour
{
    private SkinnedMeshRenderer meshRender;

    public Material normal, canGrab, handShouldntBeThere;

    private void Awake()
    {
        meshRender = GetComponent<SkinnedMeshRenderer>();
    }

    public void MaterialChange(int handState, int hand)
    {
        if (handState == 0)
        {
            if (hand == 0)
            {
                meshRender.material = normal;
            }
            else
            {
                meshRender.materials[1] = normal;
            }
        }
        else if (handState == 1)
        {
            if (hand == 0)
            {
                meshRender.material = canGrab;
            }
            else
            {
                meshRender.materials[1] = canGrab;
            }
        }
        else
        {
            if (hand == 0)
            {
                meshRender.material = handShouldntBeThere;
            }
            else
            {
                meshRender.materials[1] = handShouldntBeThere;
            }
        }
    }
}
