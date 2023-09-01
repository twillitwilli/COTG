using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMagicController : MonoBehaviour
{
    private VRPlayerController _player;
    private PlayerComponents _playerComponenets;

    private PlayerStaff _currentStaff;

    private void OnEnable()
    {
        _player = LocalGameManager.Instance.player;
        _playerComponenets = _player.GetPlayerComponents();
    }

    public void GrabStaff(VRPlayerHand hand, GameObject staffObj)
    {
        Vector3 pos;
        Vector3 rot;
        Vector3 scale;

        if (_player.isLeftHanded)
        {
            pos = new Vector3(-0.01449917f, -0.01172045f, -0.03753005f);
            rot = new Vector3(0.614f, 78.681f, 0.947f);
            scale = new Vector3(0.2675978f, 0.2675976f, 0.2675977f);
        }

        else
        {
            pos = new Vector3(-0.0414262f, -0.03348699f, -0.1072287f);
            rot = new Vector3(0.143f, 79.329f, -179.371f);
            scale = new Vector3(0.7645649f, 0.7645643f, 0.7645646f);
        }

        hand.ParentObjectToFixedHandPosition(staffObj, pos, rot, scale);
    }

    public void SpawnStaff()
    {
        GameObject newStaff = Instantiate(MasterManager.playerMagicController.staffs[MagicController.Instance.magicIdx]);
        _currentStaff = newStaff.GetComponent<PlayerStaff>();
        ResetOnBack();
    }

    public void ResetStaff()
    {
        for (int i = 0; i < 2; i++)
        {
            VRPlayerHand hand = _playerComponenets.GetHand(i);
            hand.EmptyHand();
        }

        if (_currentStaff != null) { Destroy(_currentStaff.gameObject); }

        SpawnStaff();
    }

    public void ResetOnBack()
    {

    }

    public PlayerStaff GetPlayerStaff() { return _currentStaff; }

    private void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            _playerComponenets.GetHand(i).EmptyHand();
        }

        if (_currentStaff != null) Destroy(_currentStaff.gameObject);
    }
}
