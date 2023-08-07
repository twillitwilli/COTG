using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public float cooldownTimer;
    [HideInInspector] public float maxTimer;
    [HideInInspector] public bool setCooldown;

    private void Awake()
    {
        maxTimer = cooldownTimer;
    }

    public bool CooldownDone()
    {
        if (setCooldown)
        {
            cooldownTimer = maxTimer;
            setCooldown = false;
        }
        if (cooldownTimer > 0) { cooldownTimer -= Time.deltaTime; }
        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            return true;
        }
        return false;
    }
}
