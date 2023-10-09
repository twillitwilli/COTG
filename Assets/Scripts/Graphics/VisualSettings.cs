using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using QTArts.AbstractClasses;

public class VisualSettings : MonoSingleton<VisualSettings>
{
    VRPlayer _player;

    public PostProcessingController postProcessingController { get; set; }
    public LightShadows shadowSetting { get; set; }
    public ShadowResolution shadowQuality { get; set; }
    
    [HideInInspector] 
    public float 
        lightRange = 27.5f, 
        brightness = 1;
    
    public enum LightAdjustment 
    { 
        shadowType, 
        shadowResolution, 
        range, 
        intensity 
    }
    
    [HideInInspector] 
    public List<LightPedastal> lightPedastals = new List<LightPedastal>();

    public override void Awake()
    {
        base.Awake();

        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public void NewPlayerCreated(VRPlayer player)
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
                    foreach (LightPedastal lights in lightPedastals) 
                    { 
                        lights.AdjustShadowType(); 
                    }
                    break;

                case LightAdjustment.shadowResolution:
                    foreach (LightPedastal lights in lightPedastals) 
                    { 
                        lights.AdjustShadowResolution(); 
                    }
                    break;

                case LightAdjustment.range:
                    foreach (LightPedastal lights in lightPedastals) 
                    { 
                        lights.AdjustRange(); 
                    }
                    break;

                case LightAdjustment.intensity:
                    foreach (LightPedastal lights in lightPedastals) 
                    { 
                        lights.AdjustIntensity(); 
                    }
                    break;
            }
        }
    }
}
