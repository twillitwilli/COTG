using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    public TargetController targetController;
    public GameObject effect, targetDestroyedEffect;
    public bool destroyOnHit;
    public float cooldownBeforeRespawn;

    private BoxCollider boxCollider;
    private bool targetCooldown, setCooldown;
    private float cooldownTimer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (targetController != null)
        {
            targetController.targets.Add(gameObject);
            targetController.totalTargets++;
        }
    }

    public void Start()
    {
        effect.SetActive(true);
    }

    private void LateUpdate()
    {
        if (targetCooldown)
            targetCooldown = TargetCooldown();
    }

    public void TargetHit()
    {
        if (targetController != null)
            targetController.TargetHit(gameObject);

        if (destroyOnHit) 
        {
            boxCollider.enabled = false;
            effect.SetActive(false);
            targetDestroyedEffect.SetActive(true);
            Destroy(gameObject, 3); 
        }

        else
        {
            boxCollider.enabled = false;
            effect.SetActive(false);
            targetDestroyedEffect.SetActive(true);
            setCooldown = true;
            targetCooldown = true;
        }
    }

    public bool TargetCooldown()
    {
        if (setCooldown)
        {
            cooldownTimer = cooldownBeforeRespawn;
            setCooldown = false;
        }

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            targetDestroyedEffect.SetActive(false);
            boxCollider.enabled = true;
            effect.SetActive(true);
            return false;
        }

        return true;
    }
}
