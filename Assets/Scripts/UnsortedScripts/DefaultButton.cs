using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultButton : MonoBehaviour
{
    public enum DefaultOptions
    {
        lighting, 
        ambientOcc, 
        bloom, 
        colorGrading, 
        onScreenText, 
        controllerDeadZone, 
        controllerTurnSpeeds, 
        volume, 
        playerAttachments
    }

    public DefaultOptions defaultOptions;

    public void ResetToDefault()
    {
        switch (defaultOptions)
        {
            case DefaultOptions.lighting:
                VisualSettings.Instance.DefaultLighting();
                break;

            case DefaultOptions.ambientOcc:
                PostProcessingController.Instance.DefaultAmbientOcc();
                break;

            case DefaultOptions.bloom:
                PostProcessingController.Instance.DefaultBloom();
                break;

            case DefaultOptions.colorGrading:
                PostProcessingController.Instance.DefaultColorGrading();
                break;

            case DefaultOptions.onScreenText:
                LocalGameManager.Instance.player.GetPlayerComponents().onScreenText.transform.localPosition = new Vector3(0, 0.05f, 0.1f);
                break;

            case DefaultOptions.controllerDeadZone:
                LocalGameManager.Instance.player.DefaultControllerDeadZone();
                break;

            case DefaultOptions.controllerTurnSpeeds:
                LocalGameManager.Instance.player.DefaultTurnSpeeds();
                break;

            case DefaultOptions.volume:
                AudioController.Instance.DefaultAudioSettings();
                break;

            case DefaultOptions.playerAttachments:
                LocalGameManager.Instance.player.DefaultAttachmentSettings();
                break;
        }

        if (PlayerMenu.Instance != null)
            PlayerMenu.Instance.ClosePlayerMenu();
    }
}
