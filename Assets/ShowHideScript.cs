using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideScript : MonoBehaviour
{
    public GameObject bgMaskSettings;
    public GameObject bgMask;
    public GameObject backpackPanel;
    public GameObject tutorialPanel;

    public Button button;
    public TextMeshProUGUI text;

    public static bool stopMovement;

    private void Start()
    {
        stopMovement = true;
        Screen.SetResolution(1920, 1080, true);
    }
    public void StopTimer()
    {
        RunningTimerLevel1Ph1.timerStop = false; //disable time
        RunningTimerLevel1Ph2.timerStopPh2 = false; //disable time
        RunningTimerLevel1Ph3.timerStopPh3 = false;

        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false;
    }
     public void RunTimer()
    {
        RunningTimerLevel1Ph1.timerStop = true; //enable time
        RunningTimerLevel1Ph2.timerStopPh2 = true; //enable time
        RunningTimerLevel1Ph3.timerStopPh3 = true;

        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;
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
        if(!stopMovement)
        {
            LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        }
        else
        {
            TriggerTutorial.disableMove = true; //enable Move
            TriggerTutorial.disableJump = false; //enable jumping
            LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        }

    }
    public void HidePanelEnableMovement(GameObject name)
    {
        stopMovement = true;
        LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }

    public void HidePanelNotAffectedMove(GameObject name)
    {
        bgMask.SetActive(false);
        LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }

    public void ShowSettingsGiveUp(GameObject name)
    {
        Pause.isEnablePause = false;
        bgMaskSettings.SetActive(true);
        LeanTween.scale(name, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
    public void HideSettingsGiveUp(GameObject name)
    {
        Pause.isEnablePause = true;
        bgMaskSettings.SetActive(false);
        LeanTween.scale(name, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
}
