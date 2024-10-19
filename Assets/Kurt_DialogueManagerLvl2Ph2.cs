using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Kurt_DialogueManagerLvl2Ph2 : MonoBehaviour
{
    public GameObject collider1; // For collider 
    public CinemachineVirtualCamera vCam1; // trigger 1
    public CinemachineVirtualCamera vCam2; // trigger 2
    public CinemachineVirtualCamera billBoardCam; // trigger 3
            
    public Tilemap tilemapToFade; // reference the tilemap
    public float fadeDuration = 2f;  // Duration of the fade-in effect

    public SpriteRenderer playerSprite;

    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    KurtMessageLvl2Ph2[] currentMessage;
    KurtActorLvl2Ph2[] currentActors;
    int activeMessage = 0;

    public GameObject passageTrigger;
    public GameObject textObj;
    public GameObject showComputer;

    public static bool isActive = false;

    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    public RunningTimerLevel2Ph2 runningTimer; // Reference to the timer script

    void Start()
    {
        collider1.SetActive(true);
        backgroundBox.transform.localScale = Vector3.zero;
        textObj.SetActive(false);
    }

    void OnEnable()
    {
        // Reset dialogue triggers
        Kurt_Trigger2Lvl2Ph2.addedTriggerPh2 = false;
        Kurt_DialogueTriggerLvl2Ph2.trigger1Ph2 = false;
        Kurt_DialogueTrigger2Lvl2Ph2.trigger2Ph2 = false;
        PassageDialogue.passageDialogue = false;
        Kurt_DialogueTrigger3Lvl2Ph2.trigger3Ph2 = false;
        Kurt_DialogueTrigger4Lvl2Ph2.trigger4Ph2 = false;
        Kurt_DialogueTrigger5Lvl2Ph2.trigger5Ph2 = false;
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

    public void OpenDialogue(KurtMessageLvl2Ph2[] messages, KurtActorLvl2Ph2[] actors)
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
        KurtMessageLvl2Ph2 messageToDisplay = currentMessage[activeMessage];
        KurtActorLvl2Ph2 actorToDisplay = currentActors[messageToDisplay.actorId];
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

    /** ====================================================================================================================== **/

    // Coroutine for fading in the tilemap smoothly
    IEnumerator FadeInTilemap()
    {
        float elapsedTime = 0f;
        Color tilemapColor = tilemapToFade.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolate the alpha value from 0 to 1 over time
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Update the tilemap's color with the new alpha value
            tilemapColor.a = newAlpha;
            tilemapToFade.color = tilemapColor;

            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is set to 1 after the transition
        tilemapColor.a = 0f;
        tilemapToFade.color = tilemapColor;
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

        
        if (!Kurt_Trigger2Lvl2Ph2.addedTriggerPh2)
        {
            Debug.Log("First Convo");

            Kurt_Trigger2Lvl2Ph2.addedTriggerPh2 = true;

            TriggerTutorial.disableMove = true;
            TriggerTutorial.disableJump = false;
        }

        else if (!Kurt_DialogueTriggerLvl2Ph2.trigger1Ph2)
        {
            Debug.Log("Second Convo");

            vCam1.Priority = 11;
            Kurt_DialogueTriggerLvl2Ph2.trigger1Ph2 = true;
            StartCoroutine(BackCamera(vCam1));
        }

        else if (!Kurt_DialogueTrigger2Lvl2Ph2.trigger2Ph2)
        {
            Debug.Log("Third Convo");

            vCam2.Priority = 11;
            Kurt_DialogueTrigger2Lvl2Ph2.trigger2Ph2 = true;
            StartCoroutine(BackCamera(vCam2));
            StartCoroutine(FadeInTilemap());
            playerSprite.flipX = true; // flip the player

            TriggerElevV2.enableElev = false; // disable elev
            collider1.SetActive(false);
        }

        else if (!PassageDialogue.passageDialogue)
        {
            Debug.Log("Passage In");
            PassageDialogue.passageDialogue = true;

            // Enable player movement
            TriggerTutorial.disableMove = true;
            TriggerTutorial.disableJump = false;
        }

        else if (!Kurt_DialogueTrigger3Lvl2Ph2.trigger3Ph2)
        {
            Debug.Log("Fourth Convo");

            Kurt_DialogueTrigger3Lvl2Ph2.trigger3Ph2 = true;

            // Enable player movement
            TriggerTutorial.disableMove = true;
            TriggerTutorial.disableJump = false;
        }

        else if (!Kurt_DialogueTrigger4Lvl2Ph2.trigger4Ph2)
        {
            Debug.Log("Fifth Convo");
            Kurt_DialogueTrigger4Lvl2Ph2.trigger4Ph2 = true;
            billBoardCam.Priority = 11;

            textObj.SetActive(true);
            StartCoroutine(BillboardCam(billBoardCam));
        }

        else if (!Kurt_DialogueTrigger5Lvl2Ph2.trigger5Ph2)
        {
            Debug.Log("Sixth Convo");
            Kurt_DialogueTrigger5Lvl2Ph2.trigger5Ph2 = true;
            playerSprite.flipX = false;
            showComputer.SetActive(true);

            // Enable player movement  
            TriggerTutorial.disableMove = true;
            TriggerTutorial.disableJump = false;
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

        Debug.Log("Player disable to move");
        // Disable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        playerSprite.flipX = false; // flip the player

        Debug.Log("Player must move");
        // Enable player movement
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;

        Debug.Log("Time continues");
        yield return new WaitForSeconds(2f);
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;
    }

    IEnumerator BillboardCam(CinemachineVirtualCamera name2)
    {
        // Pause the timer when dialogue starts
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false; 
        yield return new WaitForSeconds(5);
        name2.Priority = 0;
     

        Debug.Log("Player disable to move");
        // Disable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        playerSprite.flipX = false; // flip the player

        yield return new WaitForSeconds(2.5f);
        Debug.Log("Player must move");
        // Enable player movement
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;

        Debug.Log("Time continues");
        yield return new WaitForSeconds(3f);
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;
    }
}
