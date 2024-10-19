using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Kurt_DialogueManagerLvl2Ph1 : MonoBehaviour
{
    public CinemachineVirtualCamera Cam1; // trigger 1
    public CinemachineVirtualCamera Cam2; // trigger 2

    public SpriteRenderer playerSprite;

    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    KurtMessage[] currentMessage;
    KurtActor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;

    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    public GameObject portalToOpen;

    public RunningTimerLevel2Ph2 runningTimer; // Reference to the timer script

    void Start()
    {
        portalToOpen.SetActive(false);
        backgroundBox.transform.localScale = Vector3.zero;
    }

    void OnEnable()
    {
        // Reset dialogue triggers
        Kurt_TriggerLvl2Ph1.triggerPh1 = false;
        Kurt_triggerLvl2Ph1_2.trigger2Ph1 = false;
        Kurt_PortalTrigger.portalTrigger = false;


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            NextMessage();
            MouseLeftClick();
        }
    }

    void MouseLeftClick()
    {
        Debug.Log("Left mouse button clicked.");
    }

    public void OpenDialogue(KurtMessage[] messages, KurtActor[] actors)
    {
        currentMessage = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        // Pause the timer when dialogue starts
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false; // Use the class name for static access

        Debug.Log("Started Conversation! Loaded Messages: " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f);

        // Disable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;
    }

    void DisplayMessage()
    {
        KurtMessage messageToDisplay = currentMessage[activeMessage];
        KurtActor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        StopAllCoroutines();
        StartCoroutine(TypeMessage(messageToDisplay.message));
    }

    IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        textFullyDisplayed = false;
        messageText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return null; // Default typing speed can be adjusted here if needed

            if (!isTyping)
            {
                messageText.text = message;
                break;
            }
        }

        textFullyDisplayed = true;
        isTyping = false;
        AnimationTextColor();
    }

    public void NextMessage()
    {
        if (isTyping)
        {
            isTyping = false;
            return;
        }

        if (!textFullyDisplayed) return;

        activeMessage++;

        if (activeMessage < currentMessage.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended!");
            isActive = false;
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();

            // Resume the timer when dialogue ends
            RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true; // Corrected to true to resume the timer

            if (!Kurt_TriggerLvl2Ph1.triggerPh1)
            {
                Kurt_TriggerLvl2Ph1.triggerPh1 = true;
                Cam1.Priority = 11;
                StartCoroutine(BackCamera(Cam1));

                Debug.Log("First Conversation");
            }

            else if (!Kurt_triggerLvl2Ph1_2.trigger2Ph1)
            {
                Kurt_triggerLvl2Ph1_2.trigger2Ph1 = true;
                Debug.Log("Second Conversation");
                // Enable player movement
                TriggerTutorial.disableMove = true;
                TriggerTutorial.disableJump = false;
            }

            else if (!Kurt_PortalTrigger.portalTrigger)
            {
                Kurt_PortalTrigger.portalTrigger = true;
                Cam2.Priority = 11;
                portalToOpen.SetActive(true);
                StartCoroutine(BackCamera(Cam2));
                Debug.Log("Third Conversation");
            }

            else
            {
                Debug.LogError("FUTA");
                // Enable player movement
                TriggerTutorial.disableMove = true;
                TriggerTutorial.disableJump = false;
            }
        }
    }

    void AnimationTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    IEnumerator BackCamera(CinemachineVirtualCamera name)
    {
        // Pause the timer when dialogue starts
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false;

        yield return new WaitForSeconds(4);
        name.Priority = 0;

        Debug.Log("Disable Movement");
        
        // Disable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;

        playerSprite.flipX = false; // flip the player

        yield return new WaitForSeconds(2.5f);
        Debug.Log("Player must move");
        // Enable player movement
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;
    }
    IEnumerator Portal(CinemachineVirtualCamera portal)
    {
        // Pause the timer when dialogue starts
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false;

        yield return new WaitForSeconds(4);
        portal.Priority = 0;

        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;

        Debug.Log("Disable movement");
        // Enable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        playerSprite.flipX = false; // flip the player

        yield return new WaitForSeconds(2.5f);
        Debug.Log("Player must move");
        // Enable player movement
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;
    }

}
