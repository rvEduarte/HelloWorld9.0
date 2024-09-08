using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideScript : MonoBehaviour
{
    public GameObject bgMask;
    public GameObject backpackPanel;
    public GameObject tutorialPanel;

    public Button button;
    public TextMeshProUGUI text;

    public void StopTimer()
    {
        RunningTimerLevel1Ph1.timerStop = false; //disable time
    }
     public void RunTimer()
    {
        RunningTimerLevel1Ph1.timerStop = true; //enable time
    }

    public void UnlockButton(string description)
    {
        button.interactable = true;
        text.text = description;
    }

    public void ShowPanel(GameObject panelToShow, GameObject panelToHide)
    {
        // Hide the other panel
        if (panelToHide.activeSelf)
        {
            HidePanel(panelToHide);
        }

        // Show the selected panel
        panelToShow.SetActive(true);
        LeanTween.scale(panelToShow, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }

    public void HidePanel(GameObject panelToHide)
    {
        // Hide the panel smoothly
        bgMask.SetActive(false);
        LeanTween.scale(panelToHide, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        StartCoroutine(SetActive(panelToHide));
    }

    IEnumerator SetActive(GameObject panel)
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
    }

    // Method to show backpack panel and hide tutorial panel
    public void OnBackpackButtonPressed()
    {
        TriggerTutorial.disableMove = false; //disable Move
        TriggerTutorial.disableJump = true; //disable jumping
        ShowPanel(backpackPanel, tutorialPanel);
    }

    // Method to show tutorial panel and hide backpack panel
    public void OnTutorialButtonPressed()
    {
        TriggerTutorial.disableMove = false; //disable Move
        TriggerTutorial.disableJump = true; //disable jumping
        ShowPanel(tutorialPanel, backpackPanel);
    }
    public void ShowSpecificPanel(GameObject name)
    {
        bgMask.SetActive(true);
        name.SetActive(true);
        LeanTween.scale(name, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }

    public void HideSpecificPanel(GameObject name)
    {
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
        LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        StartCoroutine(SetActive(name));
    }
}
