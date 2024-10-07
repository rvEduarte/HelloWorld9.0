using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideUniversal : MonoBehaviour
{
    public GameObject bg;
    public void ShowSpecificPanel(GameObject name)
    {
        bg.SetActive(true);
        LeanTween.scale(name, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }

    public void HideSpecificPanel(GameObject name)
    {
        bg.SetActive(false);
        LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
}
