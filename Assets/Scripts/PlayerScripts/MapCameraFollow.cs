using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraFollow : MonoBehaviour
{
    VRPlayer _player;

    void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public void NewPlayerCreated(VRPlayer player)
    {
        _player = player;
    }

    private void LateUpdate()
    {
        if (_player != null)
            transform.position = new Vector3(_player.transform.position.x, 152, _player.transform.position.z);
    }
}
