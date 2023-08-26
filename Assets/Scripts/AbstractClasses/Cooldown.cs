using UnityEngine;
using System.Threading.Tasks;

public abstract class Cooldown : MonoBehaviour
{
    private float _timer;

    public bool CooldownCompleted(float cooldownTimer = 0, bool setTimer = false)
    {
        if (setTimer)
            _timer = cooldownTimer;

        if (_timer > 0)
            _timer -= Time.deltaTime;

        else return true;

        return false;
    }
}
