using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDevOptions : MonoBehaviour
{
    [SerializeField] private PlayerMenu menu;
    [SerializeField] private GameObject devOptionsButton;

    private void OnEnable()
    {
        Invoke("OpenDevOptionsButton", 0.1f);
    }

    private void OpenDevOptionsButton()
    {
        if (LocalGameManager.instance.IsDevMode()) { devOptionsButton.SetActive(true); }
    }
}
