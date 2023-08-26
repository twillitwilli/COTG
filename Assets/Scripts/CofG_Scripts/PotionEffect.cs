using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private PlayerPotionController _potionController;
    private AudioController _audioController;
    private PlayerTotalStats _totalStats;

    [SerializeField] private PlayerPotionController.PotionType _potionType;
    [SerializeField] private GameObject _potionParent;
    [SerializeField] private string _potionDescription;

    private GrabController grabController;

    private void Start()
    {
        _gameManager = LocalGameManager.Instance;
        _potionController = _gameManager.GetPotionController();
        _audioController = _gameManager.GetAudioController();
        _totalStats = _gameManager.GetTotalStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            _potionController.PotionEffect(_potionType, _potionDescription, false);

            switch (_gameManager.currentGameMode)
            {
                case LocalGameManager.GameMode.normal | LocalGameManager.GameMode.master:
                    _totalStats.AdjustStats(PlayerTotalStats.StatType.potionsDrank);
                    break;
            }

            grabController = GetComponentInParent<GrabController>();

            Destroy(_potionParent);
        }
    }

    private void OnDestroy()
    {
        if (grabController != null) { grabController.ReleaseGrip(); }
    }
}
