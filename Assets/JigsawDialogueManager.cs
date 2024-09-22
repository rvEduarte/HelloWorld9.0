using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JigsawDialogueManager : MonoBehaviour
{
    public Animator anim;

    public CinemachineVirtualCamera portalCamera;
    public GameObject portal;

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

    // Define your color dictionary here
    private Dictionary<string, string> colorTags = new Dictionary<string, string>()
    {
        { "jigsaw puzzle", "<color=yellow>jigsaw puzzle</color>" },
        { "Console.WriteLine", "<color=green>Console.WriteLine</color>" },
        { "Console.Write", "<color=green>Console.Write</color>" },
        { "adding letters or words", "<color=green>adding letters or words</color>" },
        { "same line without pressing Enter", "<color=yellow>same line without pressing Enter</color>" },
        { "all lined up next to each other", "<color=yellow>all lined up next to each other</color>" },
        { "typing and then pressing Enter after each line", "<color=yellow>typing and then pressing Enter after each line</color>" },
        { "next thing ", "<color=green>next thing </color>" },
        { "output numbers", "<color=yellow>output numbers</color>" },
        { "even perform calculations directly", "<color=yellow>even perform calculations directly</color>" },
        { "display", "<color=green>display</color>" },
        { "appear on a new line", "<color=green>appear on a new line</color>" },
        { "results of those calculations too", "<color=yellow>results of those calculations too</color>" }
    };

    void Start()
    {
        portal.SetActive(false);
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

        string characters = "";

        // Type out the message
        foreach (char letter in message.ToCharArray())
        {
            characters += letter;
            messageText.text = ReplaceWordsWithColorTags(characters);
            yield return new WaitForSeconds(textSpeed);

            Debug.Log("inside loop: " + messageText.text);
            if (!isTyping) // If the user clicks during typing
            {
                messageText.text = ReplaceWordsWithColorTags(message); // Complete the message immediately
                break;
            }
        }
        Debug.Log("outside loop: " + messageText.text);
        textFullyDisplayed = true;
        isTyping = false;
        AnimationTextColor();
    }

    string ReplaceWordsWithColorTags(string message)
    {
        foreach (var entry in colorTags)
        {
            message = message.Replace(entry.Key, entry.Value);
        }
        return message;
    }
    void AnimationTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
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
            StartCoroutine(MoveCameraPortal());
            //zoom
            //activate portal
            //activate idle portal animation

        }
    }

    IEnumerator MoveCameraPortal()
    {
        yield return new WaitForSeconds(2);
        portalCamera.Priority = 12;

        portal.SetActive(true);
        anim.SetTrigger("TriggerShow");
        anim.SetTrigger("TriggerIdle");
        StartCoroutine(BackCamera());
    }

    IEnumerator BackCamera()
    {
        yield return new WaitForSeconds(4);
        portalCamera.Priority = 0;

        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping

        tutorialButton.enabled = true; //enable clickable
        backpackButton.enabled = true; // enable clickable
        ComputerLevel1Ph1.disableInteract = true;
    }

    public void YesAndNoButton()  // BUTTON YES AND NO AFTER THE QUESTION
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
