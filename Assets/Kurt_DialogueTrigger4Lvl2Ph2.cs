using Cinemachine;
using System.Collections;
using UnityEngine;

public class Kurt_DialogueTrigger4Lvl2Ph2 : MonoBehaviour
{
    public CinemachineVirtualCamera billBoard;
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;
    public static bool trigger4Ph2;

    public SpriteRenderer playerSprite;
    public GameObject textObj;
    public GameObject showBridgeComputer;

    private KurtNew_DialogueManagerLvl2Ph2 dialogueManager;

    private void Start()
    {
        trigger4Ph2 = false;
        dialogueManager = FindObjectOfType<KurtNew_DialogueManagerLvl2Ph2>();  // Reference to the dialogue manager
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            billBoard.Priority = 11;
            StartCoroutine(BillboardCam(billBoard));

            Debug.Log("Player movement disabled");
            // Disable player movement at the start of the conversation
            TriggerTutorial.disableMove = false;
            TriggerTutorial.disableJump = true;

            hasTriggered = true; // Ensure the trigger happens only once
        }
    }

    IEnumerator BillboardCam(CinemachineVirtualCamera name2)
    {
        // Stop the game timer when dialogue starts
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false;

        textObj.SetActive(true);

        yield return new WaitForSeconds(5); // Billboard stays active for 5 seconds
        name2.Priority = 0;

        yield return new WaitForSeconds(2f);
        StartDialogue();  // Start the conversation

        playerSprite.flipX = false; // Ensure the player faces the correct direction

        // Wait for the dialogue to finish
        yield return StartCoroutine(WaitForDialogueToEnd());

        Debug.Log("Player movement enabled after dialogue ends");
        showBridgeComputer.SetActive(true);  // Activate the next object (e.g., a bridge)

        // Enable player movement after the conversation ends
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;
    }

    void StartDialogue()
    {
        dialogueManager.OpenDialogue(message, actor); // Start dialogue through the manager
    }

    IEnumerator WaitForDialogueToEnd()
    {
        // Keep checking if the dialogue has ended
        while (!dialogueManager.IsDialogueFinished())
        {
            yield return null; // Wait until the next frame
        }
    }
}
