using UnityEngine;

public class EyeManager : MonoBehaviour
{
    public void EyesOpened()
    {
        Animator EyeAnimations = GetComponent<Animator>();
        EyeAnimations.SetInteger("EyeState", 0);
    }

    public void EyesClosing()
    {
        Animator EyeAnimations = GetComponent<Animator>();
        EyeAnimations.SetInteger("EyeState", 1);
    }

    public void EyesOpening()
    {
        Animator EyeAnimations = GetComponent<Animator>();
        EyeAnimations.SetInteger("EyeState", 2);
    }
}
