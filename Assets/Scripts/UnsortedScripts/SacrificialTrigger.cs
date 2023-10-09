using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialTrigger : MonoBehaviour
{
    Animator _animator;
    bool _sacrificeTriggered;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        VRPlayer player;

        if (!_sacrificeTriggered && other.gameObject.TryGetComponent<VRPlayer>(out player))
        {
            _animator.Play("SpikesUp");
            PlayerStats.Instance.Damage(10);
            _sacrificeTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_sacrificeTriggered && other.gameObject.GetComponent<VRPlayer>())
        {
            _animator.Play("SpikesDown");
            _sacrificeTriggered = false;
        }
    }
}
