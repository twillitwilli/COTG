using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBombKeyDisplay : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Text textBox;

    public void AdjustDisplay(int currentValue, int maxValue)
    {
        float percentage = (currentValue / maxValue) * 100;
        if (percentage > 50)
        {
            meshRenderer.SetBlendShapeWeight(3, 0);
            float blendValue = 100 - percentage;
            meshRenderer.SetBlendShapeWeight(2, (blendValue / 50) * 100);
        }
        else
        {
            meshRenderer.SetBlendShapeWeight(2, 100);
            meshRenderer.SetBlendShapeWeight(3, 100 - ((percentage / 50) * 100));
        }
        textBox.text = currentValue + "/" + maxValue;
    }
}
