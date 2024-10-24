using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_DialogueTriggerLaser2Lvl2Ph2 : MonoBehaviour
{
    public CinemachineVirtualCamera laserCameraTrigger2;

    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;

    public static bool laserTrigger2;

    private void Start()
    {
        laserTrigger2 = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {

            // Disable player movement
            TriggerTutorial.disableMove = false;
            TriggerTutorial.disableJump = true;

            laserCameraTrigger2.Priority = 11;
            StartCoroutine(BackCamera(laserCameraTrigger2));
        }
    }

    public void StartDialogue()
    {
        hasTriggered = true;
        FindObjectOfType<KurtNew_DialogueManagerLvl2Ph2>().OpenDialogue(message, actor);
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

        Debug.Log("Time continues");
        yield return new WaitForSeconds(2.5f);
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;

        StartDialogue();

        Debug.Log("Player must move");
        // Enable player movement
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;
    }
}
