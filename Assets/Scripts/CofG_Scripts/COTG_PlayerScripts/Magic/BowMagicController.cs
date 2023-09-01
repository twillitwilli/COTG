using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMagicController : MonoBehaviour
{
    private VRPlayerController _player;
    private PlayerComponents _playerComponenets;

    private PlayerBow _currentBow;

    private void OnEnable()
    {
        _player = LocalGameManager.Instance.player;
        _playerComponenets = _player.GetPlayerComponents();
    }

    public void GrabBow(VRPlayerHand hand, GameObject bowObj)
    {
        Vector3 pos;
        Vector3 rot;
        Vector3 scale;

        if (!hand.IsRightHand())
        {
            pos = new Vector3(-0.01449916f, 0.05057901f, -0.03727524f);
            rot = new Vector3(0.23f, -10.672f, 179.957f);
            scale = new Vector3(0.006689941f, 0.006689937f, 0.006689939f);
        }

        else
        {
            pos = new Vector3(-0.04296442f, 0.1225004f, -0.1038278f);
            rot = new Vector3(-179.77f, -10.672f, 0.04299927f);
            scale = new Vector3(0.01911412f, 0.01911411f, -0.01911411f);
        }

        hand.ParentObjectToFixedHandPosition(bowObj, pos, rot, scale);
    }

    public void SpawnBow()
    {
        GameObject newBow = Instantiate(MasterManager.playerMagicController.bows[MagicController.Instance.magicIdx]);

        _currentBow = newBow.GetComponent<PlayerBow>();
        ResetToBack();
    }

    public void ResetBow()
    {
        for (int i = 0; i < 2; i++)
        {
            VRPlayerHand hand = _playerComponenets.GetHand(i);
            hand.EmptyHand();
        }

        if (_currentBow != null) { Destroy(_currentBow.gameObject); }

        SpawnBow();
    }

    public void ResetToBack()
    {
        if (!_player.isLeftHanded)
        {

        }

        else
        {

        }
    }

    public PlayerBow GetBow() { return _currentBow; }

    private void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            _playerComponenets.GetHand(i).EmptyHand();
        }

        if (_currentBow != null) Destroy(_currentBow.gameObject);
    }
}
