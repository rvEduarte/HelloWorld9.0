using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomObject : MonoBehaviour
{
    public GameObject bgMask;
    public Button tutorialButton;
    public Button backpackButton;

    public GameObject tutorialPanel;
    public GameObject backpackPanel;

    public CinemachineVirtualCamera jigsawCamera;
    public GameObject triggerZoom;

    public ZoomDialogueTrigger zoomDialogueTrigger;

    public ZoomDialogueManager zoomDialogueManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            bgMask.SetActive(false);
            LeanTween.scale(tutorialPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            LeanTween.scale(backpackPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            tutorialButton.enabled = false;
            backpackButton.enabled = false;
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = false; //disable jumping
            RunningTimerLevel1Ph1.timerStop = false; // disable time
            jigsawCamera.Priority = 11;

            zoomDialogueTrigger.StartDialogue();

            StartCoroutine(backCamera());
        }
    }

    IEnumerator backCamera()
    {
        yield return new WaitForSeconds(4);
        jigsawCamera.Priority = 0;

        StartCoroutine (enableMovement());
    }
    IEnumerator enableMovement()
    {
        yield return new WaitForSeconds(4);
        zoomDialogueManager.NextMessage();
        tutorialButton.enabled = true;
        backpackButton.enabled = true;
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
        RunningTimerLevel1Ph1.timerStop = true; //enable time
        triggerZoom.SetActive(false); //disable trigger

    }

}
