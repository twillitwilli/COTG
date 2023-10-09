using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAdjustmentController : MonoBehaviour
{
    VRPlayer _player;
    PlayerComponents _playerComponents;

    public GameObject menu { get; set; }

    public GameObject[] controllerOrigins;
    public HandAdjustment[] handAdjusters;

    void Start()
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

    void OnDestroy()
    {
        PlayerMenu.Instance.ClosePlayerMenu();

        foreach (GameObject obj in controllerOrigins) 
        { 
            Destroy(obj); 
        }
    }
}
