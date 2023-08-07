using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingComponents : MonoBehaviour
{
    [SerializeField] private PostProcessLayer _postProcessLayer;
    public PostProcessLayer GetPostProcessingLayer() { return _postProcessLayer; }


    [SerializeField] private PostProcessVolume _postProcessVolume;
    public PostProcessVolume GetPostProcessingVolume() { return _postProcessVolume; }


    private AmbientOcclusion _ambientOcclusionEffect;
    public AmbientOcclusion GetAmbientOcclusion() { return _ambientOcclusionEffect; }


    private Bloom _bloomEffect;
    public Bloom GetBloom() { return _bloomEffect; }


    private ColorGrading _colorGradingEffect;
    public ColorGrading GetColorGrading() { return _colorGradingEffect; }

    private void Awake()
    {
        _postProcessVolume.profile.TryGetSettings(out _ambientOcclusionEffect);
        _postProcessVolume.profile.TryGetSettings(out _bloomEffect);
        _postProcessVolume.profile.TryGetSettings(out _colorGradingEffect);
    }
}
