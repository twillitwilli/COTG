using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    private bool isPlayerOn, reduceDurability;
    private float cooldownTimer;
    private float cooldown = 5f;

    private void Update()
    {
        if (isPlayerOn)
        {
            if (cooldownTimer > 0)
            {
                reduceDurability = false;
                cooldownTimer -= Time.deltaTime;
            }

            if (cooldownTimer <= 0)
            {
                cooldownTimer = 0f;
                reduceDurability = true;
            }

            if (reduceDurability)
            {
                UpdatePlatform();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
        }
    }

    private void UpdatePlatform()
    {
        Animator BreakablePlatform = GetComponent<Animator>();
        BreakablePlatform.SetFloat("PlatformDurability", (-20f));

        cooldownTimer = cooldown;
    }

    public void PlatformWasDestroy()
    {
        Invoke("ResetPlatform", 3);
    }

    private void ResetPlatform()
    {
        Animator BreakablePlatform = GetComponent<Animator>();
        BreakablePlatform.SetFloat("PlatformDurability", 100f);
    }
}
