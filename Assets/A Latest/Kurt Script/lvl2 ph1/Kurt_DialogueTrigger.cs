using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_DialogueTrigger : MonoBehaviour
{
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false; // To track if the conversation has already occurred

    public static bool natitriggernaAko;

    private void Start()
    {
        natitriggernaAko = false;
    }

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
        FindObjectOfType<Kurt_DialogueManagerLvl2Ph2>().OpenDialogue(message, actor);
    }
}
