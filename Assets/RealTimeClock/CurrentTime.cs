using UnityEngine;

public class CurrentTime : MonoBehaviour
{
    public static CurrentTime instance;

    public int hour, minutes;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        hour = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
    }
}
