using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightPedastal : MonoBehaviour
{
    private Light lightSettings;
    private VisualSettings _visualSettings;

    private void Awake()
    {
        lightSettings = GetComponent<Light>();
    }

    private void Start()
    {
        _visualSettings = LocalGameManager.Instance.GetVisualSettings();
        _visualSettings.lightPedastals.Add(this);
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
        lightSettings.range = _visualSettings.lightRange;
    }

    public void AdjustIntensity()
    {
        lightSettings.intensity = _visualSettings.brightness;
    }

    public void AdjustShadowType()
    {
        lightSettings.shadows = _visualSettings.shadowSetting;
    }

    public void AdjustShadowResolution()
    {
        lightSettings.shadowResolution = (LightShadowResolution)_visualSettings.shadowQuality;
    }
}
