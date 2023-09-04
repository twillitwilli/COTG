using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{
    public GameObject healthDisplay;
    public bool isBoss;
    public Text visualDisplay;

    private void Awake()
    {
        healthDisplay.SetActive(false);
    }

    private void Start()
    {
        if (LocalGameManager.Instance.currentGameMode != LocalGameManager.GameMode.master || EnemyTrackerController.Instance.hasEnemyHealthReveal)
            healthDisplay.SetActive(true);

        if (isBoss)
            healthDisplay.SetActive(true);
    }

    public void UpdateDisplay(int currentHealth, int maxHealth)
    {
        visualDisplay.text = currentHealth + "/" + maxHealth;
    }
}
