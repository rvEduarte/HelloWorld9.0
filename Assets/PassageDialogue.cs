using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageDialogue : MonoBehaviour
{
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;

    public static bool passageDialogue;

    private void Start()
    {
        passageDialogue = false;

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
