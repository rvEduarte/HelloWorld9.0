using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public List<UIController> controllers;

    public bool callAppearOnStart;

    void Start()
    {
        if (callAppearOnStart)
        {
            CallAppearOnAllAnimators();
        }
    }
    public void CallAppearOnAllAnimators()
    {
        foreach (UIController anim in controllers)
        {
            anim.gameObject.SetActive(true);
        }
    }

    public void CallDisappearOnAllAnimators()
    {
        foreach (UIController anim in controllers)
        {
            anim.gameObject.SetActive(false);
        }
    }
}
