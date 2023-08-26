using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultButton : MonoBehaviour
{
    public enum DefaultOptions
    {
        lighting, ambientOcc, bloom, colorGrading, onScreenText, controllerDeadZone, controllerTurnSpeeds, volume, playerAttachments
    }
    public DefaultOptions defaultOptions;

    private VRPlayerController _player;
    private VisualSettings _visualSettings;
    private PostProcessingController _postProcessingController;

    private void Start()
    {
        _visualSettings = LocalGameManager.Instance.GetVisualSettings();
        _postProcessingController = LocalGameManager.Instance.GetPostProcessingController();
        _player = LocalGameManager.Instance.player;
    }

    public void ResetToDefault()
    {
        switch (defaultOptions)
        {
            case DefaultOptions.lighting:
                _visualSettings.DefaultLighting();
                break;

            case DefaultOptions.ambientOcc:
                _postProcessingController.DefaultAmbientOcc();
                break;

            case DefaultOptions.bloom:
                _postProcessingController.DefaultBloom();
                break;

            case DefaultOptions.colorGrading:
                _postProcessingController.DefaultColorGrading();
                break;

            case DefaultOptions.onScreenText:
                _player.GetPlayerComponents().onScreenText.transform.localPosition = new Vector3(0, 0.05f, 0.1f);
                break;

            case DefaultOptions.controllerDeadZone:
                _player.DefaultControllerDeadZone();
                break;

            case DefaultOptions.controllerTurnSpeeds:
                _player.DefaultTurnSpeeds();
                break;

            case DefaultOptions.volume:
                LocalGameManager.Instance.GetAudioController().DefaultAudioSettings();
                break;

            case DefaultOptions.playerAttachments:
                _player.DefaultAttachmentSettings();
                break;
        }

        if (PlayerMenu.instance != null) 
        {
            Destroy(PlayerMenu.instance.gameObject);
            PlayerMenu.instance = null;
        }
    }
}
