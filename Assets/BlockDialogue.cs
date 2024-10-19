using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockDialogue : MonoBehaviour
{

    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessage;
    Actor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;
    public float textSpeed = 0.05f;
    [SerializeField] public float fadeDuration = 0.5f; // Duration for the fade effect

    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        TriggerTutorial.disableMove = false; // disableMove
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            NextMessage();
            MouseLeftClick();
        }
    }

    void MouseLeftClick()
    {
        Debug.Log("Left mouse button clicked.");
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessage = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started Conversation! Loaded Messages: " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f);

        // Disable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessage[activeMessage];
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
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
            yield return new WaitForSeconds(textSpeed);

            if (!isTyping) // If the user clicks during typing
            {
                messageText.text = message; // Complete the message immediately
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
            isTyping = false; // Stop typing and display the whole message
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

            // Enable player movement
            TriggerTutorial.disableMove = true;
            TriggerTutorial.disableJump = false;
        }
    }

    void AnimationTextColor()

    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    public void enableMove()
    {
        TriggerTutorial.disableMove = true; // enable move
    }

}
