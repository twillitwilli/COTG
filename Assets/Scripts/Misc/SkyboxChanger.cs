using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public static SkyboxChanger instance;

    public Material[] skybox;

    public int whichSkybox;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        RenderSettings.skybox = skybox[whichSkybox];
    }

    public void ChangeSkybox()
    {
        RenderSettings.skybox = skybox[whichSkybox];
    }
}
