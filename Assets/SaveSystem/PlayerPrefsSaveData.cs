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
            _visualSettings.DefaultLighting();
            _postProcessingController.DefaultSettings();
            _chatManager.DefaultChatSettings();
        }
        else { LoadData(); }
    }

    public void SaveData()
    {
        _playerComponents = _player.GetPlayerComponents();

        SaveBasePlayerSettings();

        PlayerPrefs.SetInt("SavedDungeon", (LocalGameManager.instance.savedDungeon ? 1 : 0));

        _playerComponents.GetHand(0).SaveHandPosition();
        _playerComponents.GetHand(1).SaveHandPosition();

        _playerComponents.SavePlayerOrigins();

        SaveClassInfo();
        SaveMultiplayerSettings();
        SaveVisualSettings();
    }

    public void LoadData()
    {
        _playerComponents = _player.GetPlayerComponents();

        LoadBasePlayerSettings();

        if (CheckIfSaveFileExists("SavedDungeon")) { LocalGameManager.instance.savedDungeon = (PlayerPrefs.GetInt("SavedDungeon") != 0); } 

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
        if (CheckIfSaveFileExists("LeftDeadzone")) { _player.leftJoystickDeadzoneAdjustment = PlayerPrefs.GetFloat("LeftDeadzone"); }
        else { _player.leftJoystickDeadzoneAdjustment = 0.25f; }

        if (CheckIfSaveFileExists("RightDeadzone")) { _player.rightJoystickDeadzoneAdjustment = PlayerPrefs.GetFloat("RightDeadzone"); }
        else { _player.rightJoystickDeadzoneAdjustment = 0.5f; }

        if (CheckIfSaveFileExists("TurnSpeed")) { _player.turnSpeedAdjustment = PlayerPrefs.GetFloat("TurnSpeed"); }
        else { _player.turnSpeedAdjustment = 1; }

        if (CheckIfSaveFileExists("SnapTurnRotation")) { _player.snapTurnRotationAdjustment = PlayerPrefs.GetFloat("SnapTurnRotation"); }
        else { _player.snapTurnRotationAdjustment = 45; }

        if (CheckIfSaveFileExists("HeadOrientation")) { _player.headOrientation = (PlayerPrefs.GetInt("HeadOrientation") != 0); }
        else { _player.headOrientation = true; }

        if (CheckIfSaveFileExists("SnapTurn")) { _player.snapTurnOn = (PlayerPrefs.GetInt("SnapTurn") != 0); }
        else { _player.snapTurnOn = false; }

        if (CheckIfSaveFileExists("PlayMode")) { _player.playerStanding = (PlayerPrefs.GetInt("PlayMode") != 0); }
        else { _player.playerStanding = true; }

        if (CheckIfSaveFileExists("ToggleGrip")) { _player.toggleGrip = (PlayerPrefs.GetInt("ToggleGrip") != 0); }
        else { _player.toggleGrip = false; }

        if (CheckIfSaveFileExists("Roomscale")) { _player.roomScale = (PlayerPrefs.GetInt("Roomscale") != 0); }
        else { _player.roomScale = false; }

        if (CheckIfSaveFileExists("SprintToggle")) { _player.toggleSprint = (PlayerPrefs.GetInt("SprintToggle") != 0); }
        else { _player.toggleSprint = false; }

        if (CheckIfSaveFileExists("PhysicalJumping")) { _player.physicalJumping = (PlayerPrefs.GetInt("PhysicalJumping") != 0); }
        else { _player.physicalJumping = false; }

        if (CheckIfSaveFileExists("PrimaryHand")) { _player.isLeftHanded = (PlayerPrefs.GetInt("PrimaryHand") != 0); }
        else { _player.isLeftHanded = false; }

        if (CheckIfSaveFileExists("TimeFormat")) { _player.militaryTime = (PlayerPrefs.GetInt("TimeFormat") != 0); }
        else { _player.militaryTime = false; }

        _audioController.LoadVolumeStats();

        if (CheckIfSaveFileExists("BackAttachments")) { _playerComponents.belt.backAttachments = PlayerPrefs.GetFloat("BackAttachments"); }
        else { _playerComponents.belt.backAttachments = 0; }
        _playerComponents.belt.AdjustBackAttachments();

        if (CheckIfSaveFileExists("StandingBeltAdjustment")) { _playerComponents.belt.heightStandingPlayer = PlayerPrefs.GetFloat("StandingBeltAdjustment"); }
        else { _playerComponents.belt.heightStandingPlayer = 0.65f; }

        if (CheckIfSaveFileExists("SittingBeltAdjustment")) { _playerComponents.belt.heightSittingPlayer = PlayerPrefs.GetFloat("SittingBeltAdjustment"); }
        else { _playerComponents.belt.heightSittingPlayer = 0.185f; }

        if (CheckIfSaveFileExists("CrouchSittingOffset")) { _playerComponents.belt.zAdjustmentForSittingPlayer = PlayerPrefs.GetFloat("CrouchSittingOffset"); }
        else { _playerComponents.belt.zAdjustmentForSittingPlayer = 0.145f; }
    }

    private void SaveClassInfo()
    {
        PlayerPrefs.SetInt("PlayerClass", (int)_magicController.GetClassType());
        PlayerPrefs.SetInt("MagicType", _magicController.GetCurrentMagicIndex());
        PlayerPrefs.SetInt("CastingType", (int)_magicController.GetCurrentCastingType());
    }

    private void LoadClassInfo()
    {
        if (CheckIfSaveFileExists("PlayerClass")) { _magicController.LoadClass(PlayerPrefs.GetInt("PlayerClass")); }
        else { _magicController.LoadClass(0); }

        if (CheckIfSaveFileExists("MagicType")) { _magicController.SetCurrentMagicIndex(PlayerPrefs.GetInt("MagicType")); }
        else { _magicController.SetCurrentMagicIndex(0); }

        if (CheckIfSaveFileExists("CastingType")) { _magicController.LoadCastingType(PlayerPrefs.GetInt("CastingType")); }
        else { _magicController.LoadCastingType(0); }
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
        if (CheckIfSaveFileExists("TextChat")) { _chatManager.textChat = (PlayerPrefs.GetInt("TextChat") != 0); }
        else { _chatManager.textChat = true; }

        if (CheckIfSaveFileExists("DebugChat")) { _chatManager.allowDebugMessages = (PlayerPrefs.GetInt("DebugChat") != 0); }
        else { _chatManager.allowDebugMessages = true; }

        if (CheckIfSaveFileExists("VoiceChat")) { _chatManager.voiceChat = (PlayerPrefs.GetInt("VoiceChat") != 0); }
        else { _chatManager.voiceChat = false; }

        if (CheckIfSaveFileExists("Notifications")) { _chatManager.notifications = (PlayerPrefs.GetInt("Notifications") != 0); }
        else { _chatManager.notifications = true; }
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
        if (CheckIfSaveFileExists("ShadowType"))
        {
            string shadowType = PlayerPrefs.GetString("ShadowType");

            if (shadowType == "Soft") { _visualSettings.shadowSetting = LightShadows.Soft; }
            else if (shadowType == "Hard") { _visualSettings.shadowSetting = LightShadows.Hard; }
            else { _visualSettings.shadowSetting = LightShadows.None; }
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

        if (CheckIfSaveFileExists("LightRange")) { _visualSettings.lightRange = PlayerPrefs.GetFloat("LightRange"); }
        else { _visualSettings.lightRange = 30; }

        if (CheckIfSaveFileExists("Brightness")) { _visualSettings.brightness = PlayerPrefs.GetFloat("Brightness"); }
        else { _visualSettings.brightness = 1; }

        //Post Processing
        if (CheckIfSaveFileExists("AmbientOcclusion")) { _postProcessingController.ambientOcclusion = (PlayerPrefs.GetInt("AmbientOcclusion") != 0); }
        else { _postProcessingController.ambientOcclusion = true; }

        if (CheckIfSaveFileExists("Bloom")) { _postProcessingController.bloom = (PlayerPrefs.GetInt("Bloom") != 0); }
        else { _postProcessingController.bloom = true; }

        if (CheckIfSaveFileExists("ColorGrading")) { _postProcessingController.colorGrading = (PlayerPrefs.GetInt("ColorGrading") != 0); }
        else { _postProcessingController.colorGrading = true; }

        if (CheckIfSaveFileExists("AOIntensity")) { _postProcessingController.AOIntensity = PlayerPrefs.GetFloat("AOIntensity"); }
        else { _postProcessingController.AOIntensity = 1.15f; }

        if (CheckIfSaveFileExists("Thickness")) { _postProcessingController.thickness = PlayerPrefs.GetFloat("Thickness"); }
        else { _postProcessingController.thickness = 1; }

        if (CheckIfSaveFileExists("BIntensity")) { _postProcessingController.Bintensity = PlayerPrefs.GetFloat("BIntensity"); }
        else { _postProcessingController.Bintensity = 14; }

        if (CheckIfSaveFileExists("Threshold")) { _postProcessingController.threshold = PlayerPrefs.GetFloat("Threshold"); }
        else { _postProcessingController.threshold = 1; }

        if (CheckIfSaveFileExists("Diffusion")) { _postProcessingController.diffusion = PlayerPrefs.GetFloat("Diffusion"); }
        else { _postProcessingController.diffusion = 7; }

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

        if (CheckIfSaveFileExists("Temperature")) { _postProcessingController.temperature = PlayerPrefs.GetFloat("Temperature"); }
        else { _postProcessingController.temperature = -75; }

        if (CheckIfSaveFileExists("Tint")) { _postProcessingController.tint = PlayerPrefs.GetFloat("Tint"); }
        else { _postProcessingController.tint = -55; }

        if (CheckIfSaveFileExists("PostExposure")) { _postProcessingController.postExposure = PlayerPrefs.GetFloat("PostExposure"); }
        else { _postProcessingController.postExposure = 0; }

        if (CheckIfSaveFileExists("HueShift")) { _postProcessingController.hueShift = PlayerPrefs.GetFloat("HueShift"); }
        else { _postProcessingController.hueShift = 0; }

        if (CheckIfSaveFileExists("Saturation")) { _postProcessingController.saturation = PlayerPrefs.GetFloat("Saturation"); }
        else { _postProcessingController.saturation = 100; 
        }
        if (CheckIfSaveFileExists("Contrast")) { _postProcessingController.contrast = PlayerPrefs.GetFloat("Contrast"); }
        else { _postProcessingController.contrast = 20; }

        _postProcessingController.LoadSettings();
    }

    public bool CheckIfSaveFileExists(string saveFile)
    {
        if (PlayerPrefs.HasKey(saveFile)) { return true; }
        else return false;
    }
}
