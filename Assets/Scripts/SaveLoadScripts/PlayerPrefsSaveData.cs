using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerPrefsSaveData : MonoBehaviour
{
    [SerializeField] private MagicController _magicController;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private VisualSettings _visualSettings;
    [SerializeField] private PostProcessingController _postProcessingController;
    [SerializeField] private ChatManager _chatManager;

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
            _audioController.LoadVolumeStats();
            _visualSettings.DefaultLighting();
            _postProcessingController.DefaultSettings();
            _chatManager.DefaultChatSettings();
        }
        else { await LoadPlayerPrefs(); }
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

        _audioController.SaveVolumeStats();

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
        PlayerPrefs.SetInt("PlayerClass", (int)_magicController.GetClassType());
        PlayerPrefs.SetInt("MagicType", (int)_magicController.GetMagicType());
        PlayerPrefs.SetInt("CastingType", (int)_magicController.GetCurrentCastingType());
    }

    private void LoadClassInfo()
    {
        _magicController.LoadClass(CheckIfSaveFileExists("PlayerClass") ? PlayerPrefs.GetInt("PlayerClass") : 0);

        if (CheckIfSaveFileExists("MagicType")) { _magicController.UpdateMagic(true, PlayerPrefs.GetInt("MagicType")); }
        else { _magicController.UpdateMagic(); }

        _magicController.LoadCastingType(CheckIfSaveFileExists("CastingType") ? PlayerPrefs.GetInt("CastingType") : 0);
    }

    private void SaveMultiplayerSettings()
    {
        PlayerPrefs.SetInt("TextChat", (_chatManager.textChat ? 1 : 0));
        PlayerPrefs.SetInt("DebugChat", (_chatManager.allowDebugMessages ? 1 : 0));
        PlayerPrefs.SetInt("VoiceChat", (_chatManager.voiceChat ? 1 : 0));
        PlayerPrefs.SetInt("Notifications", (_chatManager.notifications ? 1 : 0));
    }

    private void LoadMultiplayerSettings()
    {
        _chatManager.textChat = CheckIfSaveFileExists("TextChat") ? (PlayerPrefs.GetInt("TextChat") != 0) : true;
        _chatManager.allowDebugMessages = CheckIfSaveFileExists("DebugChat") ? (PlayerPrefs.GetInt("DebugChat") != 0) : false;
        _chatManager.voiceChat = CheckIfSaveFileExists("VoiceChat") ? (PlayerPrefs.GetInt("VoiceChat") != 0) : true;
        _chatManager.notifications = CheckIfSaveFileExists("Notifications") ? (PlayerPrefs.GetInt("Notifications") != 0) : true;
    }

    private void SaveVisualSettings()
    {
        switch (_visualSettings.shadowSetting)
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
        switch (_visualSettings.shadowQuality)
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
        PlayerPrefs.SetFloat("LightRange", _visualSettings.lightRange);
        PlayerPrefs.SetFloat("Brightness", _visualSettings.brightness);

        //post processing
        PlayerPrefs.SetInt("AmbientOcclusion", (_postProcessingController.ambientOcclusion ? 1 : 0));
        PlayerPrefs.SetInt("Bloom", (_postProcessingController.bloom ? 1 : 0));
        PlayerPrefs.SetInt("ColorGrading", (_postProcessingController.colorGrading ? 1 : 0));

        PlayerPrefs.SetFloat("AOIntensity", _postProcessingController.AOIntensity);
        PlayerPrefs.SetFloat("Thickness", _postProcessingController.thickness);

        PlayerPrefs.SetFloat("BIntensity", _postProcessingController.Bintensity);
        PlayerPrefs.SetFloat("Threshold", _postProcessingController.threshold);
        PlayerPrefs.SetFloat("Diffusion", _postProcessingController.diffusion);

        switch (_postProcessingController.tonemapping)
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

        PlayerPrefs.SetFloat("Temperature", _postProcessingController.temperature);
        PlayerPrefs.SetFloat("Tint", _postProcessingController.tint);
        PlayerPrefs.SetFloat("PostExposure", _postProcessingController.postExposure);
        PlayerPrefs.SetFloat("HueShift", _postProcessingController.hueShift);
        PlayerPrefs.SetFloat("Saturation", _postProcessingController.saturation);
        PlayerPrefs.SetFloat("Contrast", _postProcessingController.contrast);
    }

    private void LoadVisualSettings()
    {
        // Lighting //

        _visualSettings.lightRange = CheckIfSaveFileExists("LightRange") ? PlayerPrefs.GetFloat("LightRange") : 30;
        _visualSettings.brightness = CheckIfSaveFileExists("Brightness") ? PlayerPrefs.GetFloat("Brightness") : 1;

        if (CheckIfSaveFileExists("ShadowType"))
        {
            string shadowType = PlayerPrefs.GetString("ShadowType");

            switch (shadowType)
            {
                case "Soft":
                    _visualSettings.shadowSetting = LightShadows.Soft;
                    break;

                case "Hard":
                    _visualSettings.shadowSetting = LightShadows.Hard;
                    break;

                default:
                    _visualSettings.shadowSetting = LightShadows.None;
                    break;
            }
        }
        else { _visualSettings.shadowSetting = LightShadows.Soft; }

        if (CheckIfSaveFileExists("ShadowQuality"))
        {
            string shadowQuality = PlayerPrefs.GetString("ShadowQuality");

            switch (shadowQuality)
            {
                case "VeryHigh":
                    _visualSettings.shadowQuality = ShadowResolution.VeryHigh;
                    break;

                case "High":
                    _visualSettings.shadowQuality = ShadowResolution.High;
                    break;

                case "Medium":
                    _visualSettings.shadowQuality = ShadowResolution.Medium;
                    break;

                default: _visualSettings.shadowQuality = ShadowResolution.Low;
                    break;
            }
        }
        else { _visualSettings.shadowQuality = ShadowResolution.VeryHigh; }

        //Post Processing //

        /// Ambient Occlusion ///
        
        _postProcessingController.ambientOcclusion = CheckIfSaveFileExists("AmbientOcclusion") ? (PlayerPrefs.GetInt("AmbientOcclusion") != 0) : true;
        _postProcessingController.AOIntensity = CheckIfSaveFileExists("AOIntensity") ? PlayerPrefs.GetFloat("AOIntensity") : 1.15f;
        _postProcessingController.thickness = CheckIfSaveFileExists("Thickness") ? PlayerPrefs.GetFloat("Thickness") : 1;

        /// Bloom ///

        _postProcessingController.bloom = CheckIfSaveFileExists("Bloom") ? (PlayerPrefs.GetInt("Bloom") != 0) : true;
        _postProcessingController.Bintensity = CheckIfSaveFileExists("BIntensity") ? PlayerPrefs.GetFloat("BIntensity") : 14;
        _postProcessingController.threshold = CheckIfSaveFileExists("Threshold") ? PlayerPrefs.GetFloat("Threshold") : 1;
        _postProcessingController.diffusion = CheckIfSaveFileExists("Diffusion") ? PlayerPrefs.GetFloat("Diffusion") : 7;

        /// Color Grading ///

        _postProcessingController.colorGrading = CheckIfSaveFileExists("ColorGrading") ? (PlayerPrefs.GetInt("ColorGrading") != 0) : true;
        _postProcessingController.temperature = CheckIfSaveFileExists("Temperature") ? PlayerPrefs.GetFloat("Temperature") : -75;
        _postProcessingController.tint = CheckIfSaveFileExists("Tint") ? PlayerPrefs.GetFloat("Tint") : -55;
        _postProcessingController.postExposure = CheckIfSaveFileExists("PostExposure") ? PlayerPrefs.GetFloat("PostExposure") : 0;
        _postProcessingController.hueShift = CheckIfSaveFileExists("HueShift") ? PlayerPrefs.GetFloat("HueShift") : 0;
        _postProcessingController.saturation = CheckIfSaveFileExists("Saturation") ? PlayerPrefs.GetFloat("Saturation") : 100;
        _postProcessingController.contrast = CheckIfSaveFileExists("Contrast") ? PlayerPrefs.GetFloat("Contrast") : 20;

        if (CheckIfSaveFileExists("Tonemapper")) 
        {
            string tonemapping = PlayerPrefs.GetString("Tonemapper");

            switch (tonemapping)
            {
                case "None":
                    _postProcessingController.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.None;
                    break;

                case "Neutral":
                    _postProcessingController.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral;
                    break;

                default: _postProcessingController.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.ACES;
                    break;
            }
        }
        else { _postProcessingController.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.ACES; }

        _postProcessingController.LoadSettings();
    }

    public bool CheckIfSaveFileExists(string saveFile)
    {
        bool fileExists = PlayerPrefs.HasKey(saveFile) ? true : false;
        return fileExists;
    }
}
