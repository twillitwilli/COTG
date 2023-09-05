using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightPedastal : MonoBehaviour
{
    private Light lightSettings;

    private void Awake()
    {
        lightSettings = GetComponent<Light>();
    }

    private void Start()
    {
        VisualSettings.Instance.lightPedastals.Add(this);

        AdjustLight();
    }

    public void AdjustLight()
    {
        AdjustRange();
        AdjustIntensity();
        AdjustShadowType();
        AdjustShadowResolution();
    }

    public void AdjustRange()
    {
        lightSettings.range = VisualSettings.Instance.lightRange;
    }

    public void AdjustIntensity()
    {
        lightSettings.intensity = VisualSettings.Instance.brightness;
    }

    public void AdjustShadowType()
    {
        lightSettings.shadows = VisualSettings.Instance.shadowSetting;
    }

    public void AdjustShadowResolution()
    {
        lightSettings.shadowResolution = (LightShadowResolution)VisualSettings.Instance.shadowQuality;
    }
}
