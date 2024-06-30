using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public Image image;

    public void FadeImageScale()
    {
        image.color = new Color(255, 255, 255, 0.2f);
    }

    public void BackToColor()
    {
        image.color = new Color(255, 255, 255, 1f);
    }

}
