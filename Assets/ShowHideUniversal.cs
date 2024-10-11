using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideUniversal : MonoBehaviour
{
    public GameObject bg;
    public GameObject bgSettings;

    private void Start() // OPTIONAL
    {
        Screen.SetResolution(1920, 1080, true);
    }
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

    public void ShowPanel(GameObject name)
    {
        LeanTween.scale(name, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }

    public void HidePanel(GameObject name)
    {
        LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
    public void ShowPanelLoginBg(GameObject name)
    {
        bgSettings.SetActive(true);
        LeanTween.scale(name, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
    public void HidePanelLoginBg(GameObject name)
    {
        bgSettings.SetActive(false);
        LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
}
