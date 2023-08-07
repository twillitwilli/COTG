using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialToggle : MonoBehaviour
{
    public GameObject toggleMaterialOnThis;

    public Material mat1, mat2;

    public string tagToLookFor;

    private bool materialSwap;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = toggleMaterialOnThis.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            if (materialSwap)
            {
                meshRenderer.material = mat2;
                materialSwap = false;
            }

            else if (!materialSwap)
            {
                meshRenderer.material = mat1;
                materialSwap = true;
            }
        }
    }
}
