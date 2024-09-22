using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject moveTutorialPanel;

    public Image backgroundImage; // make it transparent
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessage;
    Actor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;
    public float textSpeed = 0.05f;
    [SerializeField]public float fadeDuration = 0.5f; // Duration for the fade effect

    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        // Set the background image to be fully opaque initially
        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 1);

        LeanTween.scale(moveTutorialPanel, Vector3.zero, 0f);
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
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessage[activeMessage];
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        StopAllCoroutines();
        StartCoroutine(TypeMessage(messageToDisplay.message));

        // Fade the background image if it's not the first message
        if (activeMessage == 1) // Trigger fade on the second message
        {
            LeanTween.alpha(backgroundImage.rectTransform, 0, fadeDuration);
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

            StartCoroutine(EnableMoveTutorial());
        }
    }

    void AnimationTextColor()

    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    public void enableMove()
    {
        LeanTween.scale(moveTutorialPanel, Vector3.zero, 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        TriggerTutorial.disableMove = true; // enable move
        StartCoroutine(DisableMoveTutorial());
    }

    IEnumerator DisableMoveTutorial()
    {
        // waiting state
        yield return new WaitForSeconds(0.5f);
        moveTutorialPanel.SetActive(false);
    }

    IEnumerator EnableMoveTutorial()
    {
        // waiting state
        yield return new WaitForSeconds(1.5f);
        moveTutorialPanel.SetActive(true);
        LeanTween.scale(moveTutorialPanel, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
    }
}
