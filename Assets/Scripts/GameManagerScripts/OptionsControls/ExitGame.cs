using UnityEngine;


public class ExitGame : MonoBehaviour
{
    public bool resetPlayer;

    public void QuitGame()
    {
        if (resetPlayer) { PlayerPrefs.SetInt("NewPlayer", (false ? 1 : 0)); }

        Application.Quit();
    }
}
