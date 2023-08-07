using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSelector : MonoBehaviour
{
    [SerializeField] private MagicController.MagicType magicSelection;
    private MagicController _magicController;

    public GameObject[] enableObjs, disableObjs;

    private void Start()
    {
        _magicController = LocalGameManager.instance.GetMagicController();
    }

    public void ChangeMagic()
    {
        _magicController.SetToSpecificMagic(magicSelection);

        foreach (GameObject obj in enableObjs) { obj.SetActive(true); }
        foreach (GameObject obj in disableObjs) { obj.SetActive(false); }
    }
}
