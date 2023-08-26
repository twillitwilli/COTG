using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCalibrationCheck : MonoBehaviour
{
    public GameObject SaveFileSelector;

    private void LateUpdate()
    {
        if (LocalGameManager.Instance.hasCalibrated)
        {
            SaveFileSelector.SetActive(true);
            Destroy(gameObject);
        }
    }
}
