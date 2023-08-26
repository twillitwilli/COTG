using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAdjustmentController : MonoBehaviour
{
    private VRPlayerController _player;
    private PlayerComponents _playerComponents;

    [HideInInspector] public GameObject menu;

    public CloseMenu closeMenuButton;
    public GameObject[] controllerOrigins;
    public HandAdjustment[] handAdjusters;

    private void Start()
    {
        _player = LocalGameManager.Instance.player;

        for (int i = 0; i < controllerOrigins.Length; i++)
        {
            controllerOrigins[i].transform.SetParent(_playerComponents.GetHand(i).transform);
            controllerOrigins[i].transform.localPosition = new Vector3(0, 0, 0);
            controllerOrigins[i].transform.localEulerAngles = new Vector3(0, 0, 0);
            controllerOrigins[i].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }

        foreach (HandAdjustment adjusters in handAdjusters)
        {
            adjusters.player = _player;
            adjusters.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        VRPlayerHand leftHand = _playerComponents.GetHand(0);
        VRPlayerHand rightHand = _playerComponents.GetHand(1);
        closeMenuButton.ClosePlayerMenu();
        foreach (GameObject obj in controllerOrigins) { Destroy(obj); }
    }
}
