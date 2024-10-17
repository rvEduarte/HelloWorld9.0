using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButtonScript : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject notif, notif2;
    public GameObject bgMask;
    public void UnlockButton(string description)
    {
        button.interactable = true;
        text.text = description;
        notif.SetActive(true);
        notif2.SetActive(true);
    }
    public void HidePanelShowNotif(GameObject panelToHide)
    {
        bgMask.SetActive(false);
        LeanTween.scale(panelToHide, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }

    public void HideUnlockButton(GameObject panelToHide)
    {
        notif.SetActive(false);
        notif2.SetActive(false);
        // Hide the panel smoothly
        bgMask.SetActive(false);
        LeanTween.scale(panelToHide, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }

    public void HidePanelDisableMove(GameObject panelToHide)
    {
        bgMask.SetActive(false);
        LeanTween.scale(panelToHide, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
}
