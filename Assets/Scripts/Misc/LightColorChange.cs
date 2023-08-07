using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorChange : MonoBehaviour
{
    public bool lightOn;

    public Color offColor, onColor;

    private Light thisLight;

    private void Awake()
    {
        thisLight = GetComponent<Light>();
    }

    public void ChangeLightColor()
    {
        if (!lightOn)
        {
            thisLight.color = offColor;
        }

        else if (lightOn)
        {
            thisLight.color = onColor;
        }
    }
}
