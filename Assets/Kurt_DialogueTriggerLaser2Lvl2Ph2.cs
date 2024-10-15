using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_DialogueTriggerLaser2Lvl2Ph2 : MonoBehaviour
{
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;

    public static bool laserTrigger2_Ph2;

    private void Start()
    {
        laserTrigger2_Ph2 = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        hasTriggered = true;
        FindObjectOfType<Kurt_DialogueManagerLvl2Ph2>().OpenDialogue(message, actor);
    }
}
