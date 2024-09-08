using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifIconManager : MonoBehaviour
{
    public GameObject backpackRedIcon;
    public GameObject tutorialRedIcon;

    public static bool icon1;
    public static bool icon2;
    public static bool icon3;
    public static bool icon4;
    public static bool icon5;


    private void Update()
    {
        if (icon1 && icon2 == true)
        {
            Debug.Log("First");
        }

        if (icon4 == true)
        {
            Debug.Log("Second");
        }
    }

}
