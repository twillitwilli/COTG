using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    public bool shieldBreakable;
    public Material normalMat, shieldHitMat;
    public float currentShield, maxShield;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ShieldHit(float shieldDamage)
    {
        ChangeShieldMaterial();
        if(shieldBreakable)
        {
            currentShield -= shieldDamage;
            if (currentShield <= 0) { Destroy(gameObject); }
        }
    }

    private void ChangeShieldMaterial()
    {
        meshRenderer.material = shieldHitMat;
        Invoke("ReturnShieldToNormal", 1);
    }

    private void ReturnShieldToNormal()
    {
        meshRenderer.material = normalMat;
    }
}
