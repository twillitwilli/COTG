using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private VRPlayerController _player;

    public float rotationSpeed;
    public bool lockPosition;
    public Vector3 position;
    public bool lockRotationAxis;
    public bool lockX, lockY, lockZ;
    public Vector3 lockedRotation;

    private Transform target;
    private Vector3 newRotation;

    private void Start()
    {
        _player = LocalGameManager.instance.player;
        target = _player.head;
    }

    private void Update()
    {
        target = _player.head;
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        if (lockPosition) { transform.localPosition = position; }
        if (lockRotationAxis)
        {
            newRotation = transform.localEulerAngles;
            if (lockX) { newRotation = new Vector3(lockedRotation.x, newRotation.y, newRotation.z); }
            if (lockY) { newRotation = new Vector3(newRotation.x, lockedRotation.y, newRotation.z); }
            if (lockZ) { newRotation = new Vector3(newRotation.x, newRotation.y, lockedRotation.z); }
            transform.localEulerAngles = newRotation;
        }
    }
}
