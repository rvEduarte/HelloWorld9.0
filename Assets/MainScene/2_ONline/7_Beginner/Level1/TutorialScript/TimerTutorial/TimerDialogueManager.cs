using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Drawing;

public class TimerDialogueManager : MonoBehaviour
{
    public Button tutorialButton;
    public Button backpackButton;

    public GameObject tutorialArchive;

    public GameObject backpack;

    public GameObject triggerTimerPanel;

    public GameObject objectivePanel;

    public GameObject firstCursorBox;
    public GameObject bg1;

    public GameObject secondCursorBox;
    public GameObject bg2;

    public GameObject thirdCursorBox;
    public GameObject bg3;    
    
    public GameObject fourthCursorBox;
    public GameObject bg4;

    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    TimerMessage[] currentMessage;
    TimerActor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;
    public float textSpeed = 0.05f;

    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    // Define your color dictionary here
    private Dictionary<string, string> colorTags = new Dictionary<string, string>()
    {
        
        { "Everytime we find a piece", "<color=green>Everytime we find a piece</color>" },
        { "Tutorial Archive button", "<color=green>Tutorial Archive button</color>" },
        { "timer counting", "<color=yellow>timer counting</color>" },
        { " time", "<color=yellow> time</color>" },
        { "complete the game", "<color=yellow>complete the game</color>" },
        { "current objective", "<color=yellow>current objective</color>" },
        { "score you can achieve", "<color=yellow>score you can achieve</color>" },
        { "how quickly", "<color=green>how quickly</color>" },
        { "complete each phase", "<color=yellow>complete each phase</color>" },
        { "contains all the tutorials you’ve unlocked so far", "<color=yellow>contains all the tutorials you’ve unlocked so far</color>" },
        { "just tap on it", "<color=green>just tap on it</color>" },
        { "guides are right there", "<color=green>guides are right there</color>" },
        { "review", "<color=yellow>review</color>" },
        { "more tutorials", "<color=yellow>more tutorials</color>" },
        { "added", "<color=yellow>added</color>" },
        { "archive anytime!", "<color=yellow>archive anytime!</color>" },
        { "check the archive anytime!", "<color=green>check the archive anytime!</color>" },
        { "Backpack button", "<color=yellow>Backpack button</color>" },
        { "store all", "<color=green>store all</color>" },
        { "jigsaw puzzle pieces", "<color=yellow>jigsaw puzzle pieces</color>" },
        { "gather all of them to restore the world’s beauty", "<color=yellow>gather all of them to restore the world’s beauty</color>" },
        { "straight to the backpack", "<color=green>straight to the backpack</color>" },
        { "we'll find every piece", "<color=yellow>we'll find every piece</color>" },
        { "restore my memories", "<color=yellow>restore my memories</color>" },
        { "bring the world back to its full beauty", "<color=yellow>bring the world back to its full beauty</color>" }
    };

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        tutorialButton.enabled = false;
        backpackButton.enabled = false;
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

    public void OpenDialogue(TimerMessage[] messages, TimerActor[] actors)
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
        TimerMessage messageToDisplay = currentMessage[activeMessage];

        TimerActor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        StopAllCoroutines();
        StartCoroutine(TypeMessage(messageToDisplay.message));

        if(activeMessage == 1)
        {
            firstCursorBox.SetActive(false);
            bg1.SetActive(false);

            objectivePanel.SetActive(true);
            secondCursorBox.SetActive(true);
            bg2.SetActive(true);
        }
        if(activeMessage == 2)
        {
            secondCursorBox.SetActive(false);
            bg2.SetActive(false);

            tutorialArchive.SetActive(true);
             
        }
        if(activeMessage == 7)
        {
            thirdCursorBox.SetActive(false);
            bg3.SetActive(false);

            backpack.SetActive(true);

        }
        if (activeMessage == 10)
        {
            fourthCursorBox.SetActive(false);
            //bg4.SetActive(false);
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

            RunningTimerLevel1Ph1.timerStop = true; //enable time
            TriggerTutorial.disableMove = true;

            tutorialButton.enabled = true; 
            backpackButton.enabled = true;
            triggerTimerPanel.SetActive(false);
            bg4.SetActive(false);

        }
    }
    void AnimationTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }
}
