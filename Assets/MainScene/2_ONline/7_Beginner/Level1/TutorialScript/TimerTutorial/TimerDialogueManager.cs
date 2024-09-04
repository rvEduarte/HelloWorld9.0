using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TimerDialogueManager : MonoBehaviour
{
    public GameObject triggerTimerPanel;

    public GameObject objectivePanel;

    public GameObject firstCursorBox;
    public GameObject bg1;

    public GameObject secondCursorBox;
    public GameObject bg2;

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

            RunningTimerLevel1Ph1.timerStop = true; //enable time
            TriggerTutorial.disableMove = true;

            triggerTimerPanel.SetActive(false);
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
}
