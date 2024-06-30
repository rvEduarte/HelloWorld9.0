using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFx : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip myFxClip;
    public AudioClip clickFx;

    public void HoverSound()
    {
        myFx.PlayOneShot(myFxClip);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }

}
