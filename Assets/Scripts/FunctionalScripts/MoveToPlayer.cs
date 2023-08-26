using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private VRPlayerController _player;
    [SerializeField] private float _speed;

    private void Start()
    {
        _player = LocalGameManager.Instance.player;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _speed);
    }
}
