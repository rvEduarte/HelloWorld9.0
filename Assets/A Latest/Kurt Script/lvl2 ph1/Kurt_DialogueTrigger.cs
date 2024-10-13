using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_DialogueTrigger : MonoBehaviour
{
    public KurtMessage[] message;
    public KurtActor[] actor;

    private bool hasTriggered = false; // To track if the conversation has already occurred

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered) // Only trigger if conversation hasn't happened
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        hasTriggered = true; // Set flag to true so it only triggers once
        FindObjectOfType<Kurt_DialogueManagerLvl2Ph1>().OpenDialogue(message, actor);
    }
}
