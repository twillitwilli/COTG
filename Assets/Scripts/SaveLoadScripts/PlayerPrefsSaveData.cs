using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerPrefsSaveData : MonoSingleton<PlayerPrefsSaveData>
{
    private VRPlayerController _player;
    private PlayerComponents _playerComponents;

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public async void NewPlayerCreated(VRPlayerController player)
    {
        _player = player;

        await Task.Delay(5000);

        await SaveFileCheck();
    }

    public async Task SaveFileCheck()
    {
        if (!CheckIfSaveFileExists("ReturningPlayer"))
        {
            Debug.Log("New Player");
             _player.DefaultPlayerSettings();
        }

        else
            await LoadPlayerPrefs();
    }

    public async Task SavePlayerPrefs()
    {
        _playerComponents = _player.GetPlayerComponents();

        SaveBasePlayerSettings();

        _playerComponents.GetHand(0).SaveHandPosition();
        _playerComponents.GetHand(1).SaveHandPosition();

        _playerComponents.SavePlayerOrigins();

        SaveClassInfo();
        SaveMultiplayerSettings();
        SaveVisualSettings();
    }

    public async Task LoadPlayerPrefs()
    {
        _playerComponents = _player.GetPlayerComponents();

        LoadBasePlayerSettings();

        _playerComponents.GetHand(0).LoadHandPosition();
        _playerComponents.GetHand(1).LoadHandPosition();

        _playerComponents.LoadPlayerOrigins();
        LoadClassInfo();
        LoadMultiplayerSettings();
        LoadVisualSettings();
    }

    public void SaveBasePlayerSettings()
    {
        PlayerPrefs.SetInt("ReturningPlayer", (true ? 1 : 0));

        PlayerPrefs.SetFloat("LeftDeadzone", _player.leftJoystickDeadzoneAdjustment);
        PlayerPrefs.SetFloat("RightDeadzone", _player.rightJoystickDeadzoneAdjustment);
        PlayerPrefs.SetFloat("TurnSpeed", _player.turnSpeedAdjustment);
        PlayerPrefs.SetFloat("SnapTurnRotation", _player.snapTurnRotationAdjustment);
        PlayerPrefs.SetInt("HeadOrientation", (_player.headOrientation ? 1 : 0));
        PlayerPrefs.SetInt("SnapTurn", (_player.snapTurnOn ? 1 : 0));
        PlayerPrefs.SetInt("PlayMode", (_player.playerStanding ? 1 : 0));
        PlayerPrefs.SetInt("ToggleGrip", (_player.toggleGrip ? 1 : 0));
        PlayerPrefs.SetInt("Roomscale", (_player.roomScale ? 1 : 0));
        PlayerPrefs.SetInt("SprintToggle", (_player.toggleSprint ? 1 : 0));
        PlayerPrefs.SetInt("PhysicalJumping", (_player.physicalJumping ? 1 : 0));
        PlayerPrefs.SetInt("PrimaryHand", (_player.isLeftHanded ? 1 : 0));
        PlayerPrefs.SetInt("TimeFormat", (_player.militaryTime ? 1 : 0));

        AudioController.Instance.SaveVolumeStats();

        PlayerPrefs.SetFloat("BackAttachments", _playerComponents.belt.backAttachments);
        PlayerPrefs.SetFloat("StandingBeltAdjustment", _playerComponents.belt.heightStandingPlayer);
        PlayerPrefs.SetFloat("SittingBeltAdjustment", _playerComponents.belt.heightSittingPlayer);
        PlayerPrefs.SetFloat("CrouchSittingOffset", _playerComponents.belt.zAdjustmentForSittingPlayer);
    }

    public void LoadBasePlayerSettings()
    {
        _player.leftJoystickDeadzoneAdjustment = CheckIfSaveFileExists("LeftDeadzone") ? PlayerPrefs.GetFloat("LeftDeadzone") : 0.25f;
        _player.rightJoystickDeadzoneAdjustment = CheckIfSaveFileExists("RightDeadzone") ? PlayerPrefs.GetFloat("RightDeadzone") : 0.5f;
        _player.turnSpeedAdjustment = CheckIfSaveFileExists("TurnSpeed") ? PlayerPrefs.GetFloat("TurnSpeed") : 1;
        _player.snapTurnRotationAdjustment = CheckIfSaveFileExists("SnapTurnRotation") ? PlayerPrefs.GetFloat("SnapTurnRotation") : 45;
        _player.headOrientation = CheckIfSaveFileExists("HeadOrientation") ? (PlayerPrefs.GetInt("HeadOrientation") != 0) : true;
        _player.snapTurnOn = CheckIfSaveFileExists("SnapTurn") ? (PlayerPrefs.GetInt("SnapTurn") != 0) : false;
        _player.playerStanding = CheckIfSaveFileExists("PlayMode") ? (PlayerPrefs.GetInt("PlayMode") != 0) : true;
        _player.toggleGrip = CheckIfSaveFileExists("ToggleGrip") ? (PlayerPrefs.GetInt("ToggleGrip") != 0) : false;
        _player.roomScale = CheckIfSaveFileExists("Roomscale") ? (PlayerPrefs.GetInt("Roomscale") != 0) : false;
        _player.toggleSprint = CheckIfSaveFileExists("SprintToggle") ? (PlayerPrefs.GetInt("SprintToggle") != 0) : false;
        _player.physicalJumping = CheckIfSaveFileExists("PhysicalJumping") ? (PlayerPrefs.GetInt("PhysicalJumping") != 0) : false;
        _player.isLeftHanded = CheckIfSaveFileExists("PrimaryHand") ? (PlayerPrefs.GetInt("PrimaryHand") != 0) : false;
        _player.militaryTime = CheckIfSaveFileExists("TimeFormat") ? (PlayerPrefs.GetInt("TimeFormat") != 0) : false;

        PlayerBelt belt = _playerComponents.belt;
        belt.backAttachments = CheckIfSaveFileExists("BackAttachments") ? PlayerPrefs.GetFloat("BackAttachments") : 0;
        belt.heightStandingPlayer = CheckIfSaveFileExists("StandingBeltAdjustment") ? PlayerPrefs.GetFloat("StandingBeltAdjustment") : 0.65f;
        belt.heightSittingPlayer = CheckIfSaveFileExists("SittingBeltAdjustment") ? PlayerPrefs.GetFloat("StandingBeltAdjustment") : 0.185f;
        belt.zAdjustmentForSittingPlayer = CheckIfSaveFileExists("CrouchSittingOffset") ? PlayerPrefs.GetFloat("CrouchSittingOffset") : 0.145f;
        _playerComponents.belt.AdjustBackAttachments();
    }

    private void SaveClassInfo()
    {
        PlayerPrefs.SetInt("PlayerClass", (int)MagicController.Instance.currentClass);
        PlayerPrefs.SetInt("MagicType", (int)MagicController.Instance.currentMagic);
    }

    private void LoadClassInfo()
    {
        MagicController.Instance.LoadClass(CheckIfSaveFileExists("PlayerClass") ? PlayerPrefs.GetInt("PlayerClass") : 0);

        if (CheckIfSaveFileExists("MagicType"))
            MagicController.Instance.UpdateMagic(true, PlayerPrefs.GetInt("MagicType"));

        else
            MagicController.Instance.UpdateMagic();
    }

    private void SaveMultiplayerSettings()
    {
        PlayerPrefs.SetInt("TextChat", (ChatManager.Instance.textChat ? 1 : 0));
        PlayerPrefs.SetInt("DebugChat", (ChatManager.Instance.allowDebugMessages ? 1 : 0));
        PlayerPrefs.SetInt("VoiceChat", (ChatManager.Instance.voiceChat ? 1 : 0));
        PlayerPrefs.SetInt("Notifications", (ChatManager.Instance.notifications ? 1 : 0));
    }

    private void LoadMultiplayerSettings()
    {
        ChatManager.Instance.textChat = CheckIfSaveFileExists("TextChat") ? (PlayerPrefs.GetInt("TextChat") != 0) : true;
        ChatManager.Instance.allowDebugMessages = CheckIfSaveFileExists("DebugChat") ? (PlayerPrefs.GetInt("DebugChat") != 0) : false;
        ChatManager.Instance.voiceChat = CheckIfSaveFileExists("VoiceChat") ? (PlayerPrefs.GetInt("VoiceChat") != 0) : true;
        ChatManager.Instance.notifications = CheckIfSaveFileExists("Notifications") ? (PlayerPrefs.GetInt("Notifications") != 0) : true;
    }

    private void SaveVisualSettings()
    {
        switch (VisualSettings.Instance.shadowSetting)
        {
            case LightShadows.Soft:
                PlayerPrefs.SetString("ShadowType", "Soft");
                break;

            case LightShadows.Hard:
                PlayerPrefs.SetString("ShadowType", "Hard");
                break;

            case LightShadows.None:
                PlayerPrefs.SetString("ShadowType", "None");
                break;
        }

        switch (VisualSettings.Instance.shadowQuality)
        {
            case ShadowResolution.VeryHigh:
                PlayerPrefs.SetString("ShadowQuality", "VeryHigh");
                break;
            case ShadowResolution.High:
                PlayerPrefs.SetString("ShadowQuality", "High");
                break;
            case ShadowResolution.Medium:
                PlayerPrefs.SetString("ShadowQuality", "Medium");
                break;
            case ShadowResolution.Low:
                PlayerPrefs.SetString("ShadowQuality", "Low");
                break;
        }

        PlayerPrefs.SetFloat("LightRange", VisualSettings.Instance.lightRange);
        PlayerPrefs.SetFloat("Brightness", VisualSettings.Instance.brightness);

        //post processing
        PlayerPrefs.SetInt("AmbientOcclusion", (PostProcessingController.Instance.ambientOcclusion ? 1 : 0));
        PlayerPrefs.SetInt("Bloom", (PostProcessingController.Instance.bloom ? 1 : 0));
        PlayerPrefs.SetInt("ColorGrading", (PostProcessingController.Instance.colorGrading ? 1 : 0));

        PlayerPrefs.SetFloat("AOIntensity", PostProcessingController.Instance.AOIntensity);
        PlayerPrefs.SetFloat("Thickness", PostProcessingController.Instance.thickness);

        PlayerPrefs.SetFloat("BIntensity", PostProcessingController.Instance.Bintensity);
        PlayerPrefs.SetFloat("Threshold", PostProcessingController.Instance.threshold);
        PlayerPrefs.SetFloat("Diffusion", PostProcessingController.Instance.diffusion);

        switch (PostProcessingController.Instance.tonemapping)
        {
            case UnityEngine.Rendering.PostProcessing.Tonemapper.None:
                PlayerPrefs.SetString("Tonemapper", "None");
                break;

            case UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral:
                PlayerPrefs.SetString("Tonemapper", "Neutral");
                break;

            case UnityEngine.Rendering.PostProcessing.Tonemapper.ACES:
                PlayerPrefs.SetString("Tonemapper", "ACES");
                break;
        }

        PlayerPrefs.SetFloat("Temperature", PostProcessingController.Instance.temperature);
        PlayerPrefs.SetFloat("Tint", PostProcessingController.Instance.tint);
        PlayerPrefs.SetFloat("PostExposure", PostProcessingController.Instance.postExposure);
        PlayerPrefs.SetFloat("HueShift", PostProcessingController.Instance.hueShift);
        PlayerPrefs.SetFloat("Saturation", PostProcessingController.Instance.saturation);
        PlayerPrefs.SetFloat("Contrast", PostProcessingController.Instance.contrast);
    }

    private void LoadVisualSettings()
    {
        // Lighting //

        VisualSettings.Instance.lightRange = CheckIfSaveFileExists("LightRange") ? PlayerPrefs.GetFloat("LightRange") : 30;
        VisualSettings.Instance.brightness = CheckIfSaveFileExists("Brightness") ? PlayerPrefs.GetFloat("Brightness") : 1;

        if (CheckIfSaveFileExists("ShadowType"))
        {
            string shadowType = PlayerPrefs.GetString("ShadowType");

            switch (shadowType)
            {
                case "Soft":
                    VisualSettings.Instance.shadowSetting = LightShadows.Soft;
                    break;

                case "Hard":
                    VisualSettings.Instance.shadowSetting = LightShadows.Hard;
                    break;

                default:
                    VisualSettings.Instance.shadowSetting = LightShadows.None;
                    break;
            }
        }

        else
            VisualSettings.Instance.shadowSetting = LightShadows.Soft;

        if (CheckIfSaveFileExists("ShadowQuality"))
        {
            string shadowQuality = PlayerPrefs.GetString("ShadowQuality");

            switch (shadowQuality)
            {
                case "VeryHigh":
                    VisualSettings.Instance.shadowQuality = ShadowResolution.VeryHigh;
                    break;

                case "High":
                    VisualSettings.Instance.shadowQuality = ShadowResolution.High;
                    break;

                case "Medium":
                    VisualSettings.Instance.shadowQuality = ShadowResolution.Medium;
                    break;

                default:
                    VisualSettings.Instance.shadowQuality = ShadowResolution.Low;
                    break;
            }
        }

        else
            VisualSettings.Instance.shadowQuality = ShadowResolution.VeryHigh;

        //Post Processing //

        /// Ambient Occlusion ///

        PostProcessingController.Instance.ambientOcclusion = CheckIfSaveFileExists("AmbientOcclusion") ? (PlayerPrefs.GetInt("AmbientOcclusion") != 0) : true;
        PostProcessingController.Instance.AOIntensity = CheckIfSaveFileExists("AOIntensity") ? PlayerPrefs.GetFloat("AOIntensity") : 1.15f;
        PostProcessingController.Instance.thickness = CheckIfSaveFileExists("Thickness") ? PlayerPrefs.GetFloat("Thickness") : 1;

        /// Bloom ///

        PostProcessingController.Instance.bloom = CheckIfSaveFileExists("Bloom") ? (PlayerPrefs.GetInt("Bloom") != 0) : true;
        PostProcessingController.Instance.Bintensity = CheckIfSaveFileExists("BIntensity") ? PlayerPrefs.GetFloat("BIntensity") : 14;
        PostProcessingController.Instance.threshold = CheckIfSaveFileExists("Threshold") ? PlayerPrefs.GetFloat("Threshold") : 1;
        PostProcessingController.Instance.diffusion = CheckIfSaveFileExists("Diffusion") ? PlayerPrefs.GetFloat("Diffusion") : 7;

        /// Color Grading ///

        PostProcessingController.Instance.colorGrading = CheckIfSaveFileExists("ColorGrading") ? (PlayerPrefs.GetInt("ColorGrading") != 0) : true;
        PostProcessingController.Instance.temperature = CheckIfSaveFileExists("Temperature") ? PlayerPrefs.GetFloat("Temperature") : -75;
        PostProcessingController.Instance.tint = CheckIfSaveFileExists("Tint") ? PlayerPrefs.GetFloat("Tint") : -55;
        PostProcessingController.Instance.postExposure = CheckIfSaveFileExists("PostExposure") ? PlayerPrefs.GetFloat("PostExposure") : 0;
        PostProcessingController.Instance.hueShift = CheckIfSaveFileExists("HueShift") ? PlayerPrefs.GetFloat("HueShift") : 0;
        PostProcessingController.Instance.saturation = CheckIfSaveFileExists("Saturation") ? PlayerPrefs.GetFloat("Saturation") : 100;
        PostProcessingController.Instance.contrast = CheckIfSaveFileExists("Contrast") ? PlayerPrefs.GetFloat("Contrast") : 20;

        if (CheckIfSaveFileExists("Tonemapper")) 
        {
            string tonemapping = PlayerPrefs.GetString("Tonemapper");

            switch (tonemapping)
            {
                case "None":
                    PostProcessingController.Instance.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.None;
                    break;

                case "Neutral":
                    PostProcessingController.Instance.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral;
                    break;

                default:
                    PostProcessingController.Instance.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.ACES;
                    break;
            }
        }

        else
            PostProcessingController.Instance.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.ACES;

        PostProcessingController.Instance.LoadSettings();
    }

    public bool CheckIfSaveFileExists(string saveFile)
    {
        bool fileExists = PlayerPrefs.HasKey(saveFile) ? true : false;
        return fileExists;
    }
}
