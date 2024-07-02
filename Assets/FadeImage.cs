using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public Image image;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;    

    public void FadeImageScale()
    {
        image.color = new Color(255, 255, 255, 0.2f);
        image1.color = new Color(255, 255, 255, 0.2f);
        image2.color = new Color(255, 255, 255, 0.2f);
        image3.color = new Color(255, 255, 255, 0.2f);
        image4.color = new Color(255, 255, 255, 0.2f);
        image5.color = new Color(255, 255, 255, 0.2f);
    }

    public void BackToColor()
    {
        image.color = new Color(255, 255, 255, 1f);
        image1.color = new Color(255, 255, 255, 1f);
        image2.color = new Color(255, 255, 255, 1f);
        image3.color = new Color(255, 255, 255, 1f);
        image4.color = new Color(255, 255, 255, 1f);
        image5.color = new Color(255, 255, 255, 1f);
    }

}
