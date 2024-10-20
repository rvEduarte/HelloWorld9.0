using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        trigger4Ph2 = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            billBoard.Priority = 11;
            StartCoroutine(BillboardCam(billBoard));

            Debug.Log("Player disable to move");
            // Disable player movement
            TriggerTutorial.disableMove = false;
            TriggerTutorial.disableJump = true;

        }
    }

    IEnumerator BillboardCam(CinemachineVirtualCamera name2)
    {
        // Pause the timer when dialogue starts
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false;

        textObj.SetActive(true);

        yield return new WaitForSeconds(5);
        name2.Priority = 0;

        yield return new WaitForSeconds(2f);
        StartDialogue();

        Debug.Log("Player disable to move");
        // Disable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        playerSprite.flipX = false; // flip the player

        Debug.Log("Time continues");
        yield return new WaitForSeconds(2.5f);
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;

        Debug.Log("Player must move");
        yield return new WaitForSeconds(1f);
        // Enable player movement
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;
    }
    public void StartDialogue()
    {
        hasTriggered = true;
        FindObjectOfType<KurtNew_DialogueManagerLvl2Ph2>().OpenDialogue(message, actor);
    }

}
