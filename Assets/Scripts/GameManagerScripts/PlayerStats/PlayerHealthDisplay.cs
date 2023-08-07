using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    private PlayerStats _playerStats;
    private SkinnedMeshRenderer _meshRenderer;
    public Text textBox;

    private void Start()
    {
        _playerStats = LocalGameManager.instance.GetPlayerStats();
        _meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void AdjustHealthDisplay(float healthPercentage)
    {
        if (healthPercentage > 50f)
        {
            _meshRenderer.SetBlendShapeWeight(1, 0);
            float blendValue = 100 - healthPercentage;
            _meshRenderer.SetBlendShapeWeight(0, (blendValue / 50) * 100);
        }
        else
        {
            _meshRenderer.SetBlendShapeWeight(0, 100);
            _meshRenderer.SetBlendShapeWeight(1, 100 - ((healthPercentage / 50) * 100));
        }
        if (textBox != null)
        {
            textBox.text = _playerStats.GetCurrentHealth() + "/" + _playerStats.GetMaxHealth();
        }
    }
}
