using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachments : MonoBehaviour
{
    //wallet, map, 
    [SerializeField] private Transform[] _playerAttachmentLocations;

    public Transform GetAttachmentLocation(int whichAttachment) { return _playerAttachmentLocations[whichAttachment]; }
}
