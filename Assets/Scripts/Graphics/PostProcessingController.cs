using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    private PostProcessLayer _postProcessLayer;
    private PostProcessVolume _postProcessVolume;
    private AmbientOcclusion _ambientOcclusionEffect;
    private Bloom _bloomEffect;
    private ColorGrading _colorGradingEffect;

    //Post Processing Options

    [HideInInspector] public enum PostEffectAdjustment { ambientOcc, bloomEffect, color }

    //Ambient Occlusion Settings
    public bool ambientOcclusion { get; set; }
    [HideInInspector] public float AOIntensity = 1.15f;
    [HideInInspector] public float thickness = 1;

    //Bloom Settings
    public bool bloom { get; set; }
    [HideInInspector] public float Bintensity = 14;
    [HideInInspector] public float threshold = 1, diffusion = 7;

    //Color Grading Settings
    public bool colorGrading { get; set; }
    [HideInInspector] public Tonemapper tonemapping;
    [HideInInspector] public float temperature = -75, tint = -55, postExposure = 0, hueShift = 0, saturation = 100, contrast = 20;

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        PostProcessingComponents postProcessingComponents = player.GetPlayerComponents().postProcessingComponents;

        _postProcessLayer = postProcessingComponents.GetPostProcessingLayer();
        _postProcessVolume = postProcessingComponents.GetPostProcessingVolume();
        _ambientOcclusionEffect = postProcessingComponents.GetAmbientOcclusion();
        _bloomEffect = postProcessingComponents.GetBloom();
        _colorGradingEffect = postProcessingComponents.GetColorGrading();
    }

    public void TogglePostProcessing(bool postProcessingOn)
    {
        if (postProcessingOn) { _postProcessLayer.enabled = true; }
        else _postProcessLayer.enabled = false;
    }

    public void DefaultSettings()
    {
        DefaultAmbientOcc();
        DefaultBloom();
        DefaultColorGrading();
    }

    public void DefaultAmbientOcc()
    {
        ambientOcclusion = true;
        AOIntensity = 1.15f;
        thickness = 1;
        AmbientOcclusionSettings();
    }

    public void DefaultBloom()
    {
        bloom = true;
        Bintensity = 14;
        threshold = 1;
        diffusion = 7;
        BloomSettings();
    }

    public void DefaultColorGrading()
    {
        colorGrading = true;
        tonemapping = Tonemapper.ACES;
        temperature = -75;
        tint = -55;
        postExposure = 0;
        hueShift = 0;
        saturation = 100;
        contrast = 20;
        ColorGradingSettings();
    }

    public void ChangePostProcessingSettings(PostEffectAdjustment adjustmentType)
    {
        switch (adjustmentType)
        {
            case PostEffectAdjustment.ambientOcc:
                AmbientOcclusionSettings();
                break;

            case PostEffectAdjustment.bloomEffect:
                BloomSettings();
                break;

            case PostEffectAdjustment.color:
                ColorGradingSettings();
                break;
        }
    }

    public void LoadSettings()
    {
        AmbientOcclusionSettings();
        BloomSettings();
        ColorGradingSettings();
    }

    public void AmbientOcclusionSettings()
    {


        if (_ambientOcclusionEffect.enabled.value && !ambientOcclusion) { _ambientOcclusionEffect.enabled.value = false; }
        else if (!_ambientOcclusionEffect.enabled.value && ambientOcclusion) { _ambientOcclusionEffect.enabled.value = true; }

        _ambientOcclusionEffect.intensity.value = AOIntensity;
        _ambientOcclusionEffect.thicknessModifier.value = thickness;
    }

    public void BloomSettings()
    {
        if (_bloomEffect.enabled.value && !bloom) { _bloomEffect.enabled.value = false; }
        else if (!_bloomEffect.enabled.value && bloom) { _bloomEffect.enabled.value = true; }

        _bloomEffect.intensity.value = Bintensity;
        _bloomEffect.threshold.value = threshold;
        _bloomEffect.diffusion.value = diffusion;
    }

    public void ColorGradingSettings()
    {
        if (_colorGradingEffect.enabled.value && !colorGrading) { _colorGradingEffect.enabled.value = false; }
        else if (!_colorGradingEffect.enabled.value && colorGrading) { _colorGradingEffect.enabled.value = true; }

        _colorGradingEffect.tonemapper.value = tonemapping;
        _colorGradingEffect.temperature.value = temperature;
        _colorGradingEffect.tint.value = tint;
        _colorGradingEffect.postExposure.value = postExposure;
        _colorGradingEffect.hueShift.value = hueShift;
        _colorGradingEffect.saturation.value = saturation;
        _colorGradingEffect.contrast.value = contrast;
    }
}
