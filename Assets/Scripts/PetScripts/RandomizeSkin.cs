using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSkin : MonoBehaviour
{
    [SerializeField] private Material[] _skins;

    private void Start()
    {
        SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();
        renderer.materials[0] = _skins[Random.Range(0, _skins.Length)];
    }
}
