using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFade : MonoBehaviour
{
    private VRPlayer _player;

    public GameObject backing;
    public Material fog;
    public Color solidColor, transparentColor;
    private bool backingOn;

    private void Start()
    {
        _player = LocalGameManager.Instance.player;

        backingOn = true;
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 6)
        {
            if (backingOn)
            {
                backing.SetActive(false);
                backingOn = false;
            }
            float distance = Mathf.Clamp(0, 1, (Vector3.Distance(transform.position, _player.transform.position) - 1) / 5);
            fog.SetColor("_Color", Color.Lerp(solidColor, transparentColor, distance));
        }
        else if (!backingOn && Vector3.Distance(transform.position, _player.transform.position) > 6.5)
        {
            backing.SetActive(true);
            backingOn = true;
        }
    }
}
