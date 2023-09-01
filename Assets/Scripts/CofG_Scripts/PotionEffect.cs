using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    [SerializeField] 
    private PlayerPotionController.PotionType _potionType;

    [SerializeField] 
    private GameObject _potionParent;

    [SerializeField] 
    private string _potionDescription;

    private GrabController grabController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            PlayerPotionController.Instance.PotionEffect(_potionType, _potionDescription, false);

            switch (LocalGameManager.Instance.currentGameMode)
            {
                case LocalGameManager.GameMode.normal | LocalGameManager.GameMode.master:
                    PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.potionsDrank);
                    break;
            }

            grabController = GetComponentInParent<GrabController>();

            Destroy(_potionParent);
        }
    }

    private void OnDestroy()
    {
        if (grabController != null)
            grabController.ReleaseGrip();
    }
}
