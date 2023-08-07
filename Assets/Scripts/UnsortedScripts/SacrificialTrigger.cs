using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialTrigger : MonoBehaviour
{
    private Animator _animator;
    private bool _sacrificeTriggered;

    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _playerStats = _gameManager.GetPlayerStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_sacrificeTriggered && other.gameObject.GetComponent<VRPlayerController>())
        {
            _animator.Play("SpikesUp");
            VRPlayerController player = other.gameObject.GetComponent<VRPlayerController>();
            _playerStats.AdjustHealth(-10, "Sacrificed Your Life");
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
