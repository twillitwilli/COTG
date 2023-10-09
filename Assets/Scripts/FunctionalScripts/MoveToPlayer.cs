using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    VRPlayer _player;

    [SerializeField] 
    float _speed;

    void Start()
    {
        _player = LocalGameManager.Instance.player;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _speed);
    }
}
