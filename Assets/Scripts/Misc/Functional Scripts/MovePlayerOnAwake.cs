using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnAwake : MonoBehaviour
{
    VRPlayer _player;

    void Awake()
    {
        _player = LocalGameManager.Instance.player;

        _player.transform.position = transform.position;
        _player.transform.rotation = transform.rotation;
    }
}
