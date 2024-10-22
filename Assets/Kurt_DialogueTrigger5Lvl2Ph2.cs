using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_DialogueTrigger5Lvl2Ph2 : MonoBehaviour
{
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;

    public static bool trigger5Ph2;

    private void Start()
    {
        trigger5Ph2 = false;

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
        FindObjectOfType<KurtNew_DialogueManagerLvl2Ph2>().OpenDialogue(message, actor);
    }
}
