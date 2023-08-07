using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighlighted : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    [HideInInspector] public MenuRaycast menuRaycastHitting;
    private MenuSelectionMaterials menuMaterials;

    private void Awake()
    {
        menuMaterials = MasterManager.menuMats;
    }

    public void LateUpdate()
    {
        if (menuRaycastHitting != null && meshRenderer.material != menuMaterials.highlightedBackground)
        {
            meshRenderer.material = menuMaterials.highlightedBackground;
        }
        else if (menuRaycastHitting == null && meshRenderer.material != menuMaterials.normalMenuBackground)
        {
            meshRenderer.material = menuMaterials.normalMenuBackground;
        }
    }
}
