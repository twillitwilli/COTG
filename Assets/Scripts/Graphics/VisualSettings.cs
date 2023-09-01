using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisualSettings : MonoSingleton<VisualSettings>
{
    private VRPlayerController _player;

    [HideInInspector] 
    public PostProcessingController postProcessingController;
    
    [HideInInspector] 
    public LightShadows shadowSetting;
    
    [HideInInspector] 
    public ShadowResolution shadowQuality;
    
    [HideInInspector] 
    public float lightRange = 27.5f, brightness = 1;
    
    [HideInInspector] 
    public enum LightAdjustment 
    { 
        shadowType, 
        shadowResolution, 
        range, 
        intensity 
    }
    
    [HideInInspector] 
    public List<LightPedastal> lightPedastals = new List<LightPedastal>();

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        _player = player;
    }

    public void DefaultLighting()
    {
        shadowSetting = LightShadows.Soft;
        shadowQuality = ShadowResolution.VeryHigh;
        lightRange = 27.5f;
        brightness = 1;
        LoadSettings();
    }

    public void LoadSettings()
    {
        if (lightPedastals.Count > 0)
        {
            foreach (LightPedastal lights in lightPedastals) { lights.AdjustLight(); }
        }
    }

    public void ChangeLightSettings(LightAdjustment adjustmentType)
    {
        if (lightPedastals.Count > 0)
        {
            switch (adjustmentType)
            {
                case LightAdjustment.shadowType:
                    foreach (LightPedastal lights in lightPedastals) { lights.AdjustShadowType(); }
                    break;

                case LightAdjustment.shadowResolution:
                    foreach (LightPedastal lights in lightPedastals) { lights.AdjustShadowResolution(); }
                    break;

                case LightAdjustment.range:
                    foreach (LightPedastal lights in lightPedastals) { lights.AdjustRange(); }
                    break;

                case LightAdjustment.intensity:
                    foreach (LightPedastal lights in lightPedastals) { lights.AdjustIntensity(); }
                    break;
            }
        }
    }
}
