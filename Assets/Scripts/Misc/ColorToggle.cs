using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToggle : MonoBehaviour
{
    public Material InvisibleMaterial = null;
    public Material VisibleMaterial = null;

    private bool isInvisible = true;

    public void ToggleColor()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        isInvisible = !isInvisible;
        if (isInvisible)
        {
            renderer.material = InvisibleMaterial;
        }
        else
        {
            renderer.material = VisibleMaterial;
        }
    }
}
