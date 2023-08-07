using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelector : MonoBehaviour
{
    private MagicController _magicController;

    [SerializeField] private MagicController.ClassType _classType;

    public GameObject[] enableObjs, disableObjs;

    private void Start()
    {
        _magicController = LocalGameManager.instance.GetMagicController();
    }

    public void ChangeClass()
    {
        _magicController.ChangeClass(_classType);

        foreach (GameObject obj in enableObjs) { obj.SetActive(true); }
        foreach (GameObject obj in disableObjs) { obj.SetActive(false); }
    }
}
