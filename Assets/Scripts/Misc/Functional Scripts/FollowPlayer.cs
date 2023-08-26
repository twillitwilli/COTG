using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private VRPlayerController _player;

    public float heightLevel;
    public bool randomPlayer;
    public int whichPlayer = 0;

    private void Awake()
    {
        _player = LocalGameManager.Instance.player;
    }

    private void LateUpdate()
    {
        Vector3 playerLocation = new Vector3(_player.head.position.x, heightLevel, _player.head.position.z);
        transform.position = playerLocation;

        Vector3 playerFacingDirection = new Vector3(0, _player.head.rotation.y, 0);
        transform.localEulerAngles = playerFacingDirection;
    }
}
