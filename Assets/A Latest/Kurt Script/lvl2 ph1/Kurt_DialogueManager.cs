using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Kurt_DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    TriggerMessage[] currentMessage;
    TriggerActor[] currentActor;
    int activeMessage = 0;
    public static bool isActive = false;
    private Kurt_NPC npcRef;  // Reference to the NPC that triggered the dialogue

    public void OpenDialaogue(TriggerMessage[] messages, TriggerActor[] actors, Kurt_NPC npcReference)
    {
        currentMessage = messages;
        currentActor = actors;
        npcRef = npcReference;  // Store the NPC reference
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started Conversation! Loaded message: " + messages.Length);
        DisplayMessage();

        backgroundBox.LeanScale(Vector3.one, 0.5f);

        // Disable player movement when dialogue starts
        TriggerTutorial.disableMove = false;
    }

    void DisplayMessage()
    {
        TriggerMessage messageToDisplay = currentMessage[activeMessage];
        messageText.text = messageToDisplay.triggerMessage;

        TriggerActor actorToDisplay = currentActor[activeMessage];
        actorName.text = actorToDisplay.triggerName;
        actorImage.sprite = actorToDisplay.triggerSprite;

        AnimatedTextColor();
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessage.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended!");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInExpo();
            isActive = false;

            // Re-enable player movement when dialogue ends
            TriggerTutorial.disableMove = true;

            // Notify the NPC that the conversation has ended
            if (npcRef != null)
            {
                npcRef.OnConversationEnd();
            }
        }
    }

    void AnimatedTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    public void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            NextMessage();
        }
    }
}
