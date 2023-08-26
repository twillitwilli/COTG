using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{
    private LocalGameManager _gameManager;

    public GameObject healthDisplay;
    public bool isBoss;
    public Text visualDisplay;

    private void Awake()
    {
        _gameManager = LocalGameManager.Instance;

        healthDisplay.SetActive(false);
    }

    private void Start()
    {
        if (_gameManager.currentGameMode != LocalGameManager.GameMode.master || _gameManager.player.GetPlayerComponents().dungeonGear.hasEnemyHealthReveal) { healthDisplay.SetActive(true); }
        if (isBoss) { healthDisplay.SetActive(true); }
    }

    public void UpdateDisplay(int currentHealth, int maxHealth)
    {
        visualDisplay.text = currentHealth + "/" + maxHealth;
    }
}
