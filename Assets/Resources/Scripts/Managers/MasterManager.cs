using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "CreateNewMasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    //Instance References
    public GameSceneManager gameSceneManager;
    public static GameSceneManager sceneManager { get { return Instance.gameSceneManager; } }
    public NetworkAssetManager networkAssets;
    public static NetworkAssetManager networkAssetsManager { get { return Instance.networkAssets; } }
    public PlayerManager playerManagerObject;
    public static PlayerManager playerManager { get { return Instance.playerManagerObject; } }
    public CheckControllerType controllerSettings;
    public static CheckControllerType controllerType { get { return Instance.controllerSettings; } }
    [HideInInspector] public GameTimer gameTimer;
    public static GameTimer gameTime { get { return Instance.gameTimer; } }
    public ItemPools itemPools;
    public static ItemPools itemPool { get { return Instance.itemPools; } }
    public EnemyPools enemyPools;
    public static EnemyPools enemyPool { get { return Instance.enemyPools; } }
    public BossArenas bossArenas;
    public static BossArenas bossArena { get { return Instance.bossArenas; } }
    public PlayerClasses playerClasses;
    public static PlayerClasses playerClass { get { return Instance.playerClasses; } }
    public PlayerMagicController magicController;
    public static PlayerMagicController playerMagicController { get { return Instance.magicController; } }
    public Pets pets;
    public static Pets pet { get { return Instance.pets; } }
    public MenuSelectionMaterials menuMaterials;
    public static MenuSelectionMaterials menuMats { get { return Instance.menuMaterials; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    private static void FirstInitialize()
    {
        Debug.Log("This message will output before Awake.");
    }
}
