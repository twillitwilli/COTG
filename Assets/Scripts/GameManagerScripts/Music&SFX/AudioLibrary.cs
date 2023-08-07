using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    public AudioClip[] BackgroundMusic;

    //0 = none, 1 = boughtShopItem, 2 = dash, 3 = getscrollsfx, 4 = death, 5 = menusfx, 6 = pickupdropsfx, 7 = player moving
    //8 = potiondrinkingsfx
    public AudioClip[] SFXClips;
}
