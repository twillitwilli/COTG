using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialTrigger : MonoBehaviour
{
    private Animator _animator;
    private bool _sacrificeTriggered;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;
        if (!_sacrificeTriggered && other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            _animator.Play("SpikesUp");
            PlayerStats.Instance.Damage(10);
            _sacrificeTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_sacrificeTriggered && other.gameObject.GetComponent<VRPlayerController>())
        {
            _animator.Play("SpikesDown");
            _sacrificeTriggered = false;
        }
    }
}
