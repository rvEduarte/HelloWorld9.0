using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaserDialogueButtonManager : MonoBehaviour
{
    public GameObject iconShoot;
    public GameObject target;
    public GameObject bg;

    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    LaserMessage[] currentMessage;
    LaserActor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;
    public float textSpeed = 0.05f;

    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    // Define your color dictionary here
    private Dictionary<string, string> colorTags = new Dictionary<string, string>()
    {
        { "jigsaw puzzle", "<color=yellow>jigsaw puzzle</color>" }
    };

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            NextMessage();
        }
    }

    public void OpenDialogue(LaserMessage[] messages, LaserActor[] actors)
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
        LaserMessage messageToDisplay = currentMessage[activeMessage];

        LaserActor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        StopAllCoroutines();
        StartCoroutine(TypeMessage(messageToDisplay.message));

        if (activeMessage == 0)
        {
            target.SetActive(true);
            bg.SetActive(true);
            iconShoot.LeanScale(Vector3.one, 0);
        }
        if (activeMessage == 3)
        {
            target.SetActive(false);
            bg.SetActive(false);
            RunningTimerLevel1Ph2.timerStopPh2 = true; //enable time
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


        }
    }
}
