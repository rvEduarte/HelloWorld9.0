using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JigsawDialogueManager : MonoBehaviour
{
    public Button tutorialButton;
    public Button backpackButton;

    private bool enableClick = false;

    public GameObject dialogueBox;
    public Image dialogueImage;
    public Image avatar;

    public GameObject yesButton;
    public GameObject noButton;

    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    JigsawMessage[] currentMessage;
    JigsawActor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;
    public float textSpeed = 0.05f;

    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    public void OpenDialogue(JigsawMessage[] messages, JigsawActor[] actors)
    {
        tutorialButton.enabled = false; //disable clickable
        backpackButton.enabled = false; // disable clickable
        currentMessage = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started Conversation! Loaded Messages: " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f);
    }

    void DisplayMessage()
    {
        JigsawMessage messageToDisplay = currentMessage[activeMessage];

        JigsawActor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        StopAllCoroutines();
        StartCoroutine(TypeMessage(messageToDisplay.message));

        if (activeMessage == 0)
        {
            // This will now be checked after typing is done
            //StartCoroutine(CheckTextFullyDisplayed());
        }

        if (activeMessage == 1)
        {
            dialogueImage.color = new Color(1f, 1f, 1f, 0f);   //instant transparent
            avatar.color = new Color(1f, 1f, 1f, 0f);   //instant transparent
            LeanTween.scale(dialogueBox, new Vector3(0, 0, 0), 0.5f);

            yesButton.SetActive(true);
            noButton.SetActive(true);
            yesButton.LeanScale(Vector3.one, 0.5f);
            noButton.LeanScale(Vector3.one, 0.5f);

        }
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

            TriggerTutorial.disableMove = true; //enable Move
            TriggerTutorial.disableJump = false; //enable jumping

            tutorialButton.enabled = true; //enable clickable
            backpackButton.enabled = true; // enable clickable

        }
    }

    void AnimationTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        yesButton.LeanScale(Vector3.zero, 0f);
        noButton.LeanScale(Vector3.zero, 0f);
    }

    void Update()
    {
        if (textFullyDisplayed && activeMessage == 0)
        {
            // Detect mouse click only for the first message (index 0)
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clicked at index 0");
                NextMessage();
            }
        }

        if (!enableClick) return;
        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            NextMessage();
        }
    }

    public void YesAndNoButton()
    {
        yesButton.LeanScale(Vector3.zero, 0.5f);
        noButton.LeanScale(Vector3.zero, 0.5f);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        dialogueImage.color = new Color(1f, 1f, 1f, 1f);   //instant transparent
        avatar.color = new Color(1f, 1f, 1f, 1f);   //instant transparent
        LeanTween.scale(dialogueBox, new Vector3(1, 1, 1), 0.5f);
        NextMessage();

        enableClick = true;
    }
}
