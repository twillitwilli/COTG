using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    public GameObject dentonatingEffect;
    public float timer;
    public bool startTimer;
    [HideInInspector] public bool disableBomb;
    [HideInInspector] public VRPlayerController player;

    public void Update()
    {
        if (startTimer) 
        {
            if (!dentonatingEffect.activeSelf) { dentonatingEffect.SetActive(true); }
            Destroy(gameObject, timer); 
        }
    }

    public void OnDestroy()
    {
        if(!disableBomb)
        {
            GameObject bombExplosion = Instantiate(MasterManager.playerMagicController.arcaneBombExplosion, transform.position, transform.rotation);
            bombExplosion.transform.SetParent(null);
            bombExplosion.transform.localScale = new Vector3(1, 1, 1);
            bombExplosion.GetComponentInChildren<BombTrigger>().player = player;
            bombExplosion.GetComponentInChildren<EnemyHealthModifier>().player = player;
        }   
    }
}
