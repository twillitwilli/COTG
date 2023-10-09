using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCalibrationController : MonoBehaviour
{
    VRPlayer _player;
    PlayerComponents _playerComponents;

    public GameObject menu { get; set; }

    [SerializeField] 
    Text infoDisplay;
    
    int calibrationStage;

    void Start()
    {
        _player = LocalGameManager.Instance.player;
        _playerComponents = _player.GetPlayerComponents();

        _player.playerCalibrationOn = true;

        foreach (GameObject obj in _playerComponents.GetAllOriginPoints()) 
        { 
            obj.GetComponent<MeshRenderer>().enabled = true; 
        }

        infoDisplay.text = "Put your hand to the center of your chest,\nthen press the Grab button to confirm.";
    }

    public void ProgressCalibration(Transform calibrationPosition)
    {
        switch (calibrationStage)
        {
            case 0:
                _playerComponents.GetOriginPoint(0).transform.position = calibrationPosition.position;
                infoDisplay.text = "Put your hand to the center of your waist,\nthen press the Grab button to confirm.";
                break;

            case 1:
                _playerComponents.GetOriginPoint(1).transform.position = calibrationPosition.position;
                infoDisplay.text = "Extend your arms out to the sides of you,\nthen press the Grab button to confirm.";
                break;

            case 2:
                _playerComponents.GetOriginPoint(2).transform.position = _playerComponents.GetHand(0).transform.position;
                _playerComponents.GetOriginPoint(3).transform.position = _playerComponents.GetHand(1).transform.position;
                //_playerComponents.spellCasting.CalibrateSettings();
                Destroy(gameObject);
                break;
        }

        calibrationStage++;
    }

    private void OnDestroy()
    {
        foreach (GameObject obj in _playerComponents.GetAllOriginPoints()) 
        { 
            obj.GetComponent<MeshRenderer>().enabled = false; 
        }

        _player.playerCalibrationOn = false;
        LocalGameManager.Instance.hasCalibrated = true;

        if (menu != null)
            menu.GetComponent<PlayerMenu>().ClosePlayerMenu();
    }
}
