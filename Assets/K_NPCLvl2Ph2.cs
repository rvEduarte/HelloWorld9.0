using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_NPCLvl2Ph2 : MonoBehaviour
{
    public K_DialogueTriggerLvl2Ph2 trigger;
    public GameObject computer;  // Reference to the computer object
    private bool hasTriggeredFirstConversation = false;  // Flag to ensure one-time first conversation
    private bool hasTriggeredSecondConversation = false; // Flag to ensure one-time second conversation

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters and if the first conversation hasn't been triggered
        if (collision.gameObject.CompareTag("Player") && !hasTriggeredFirstConversation)
        {
            trigger.StartDialogue(this);  // Pass the NPC reference
        }
    }

    // This method is called when the first conversation ends
    public void OnConversationEnd()
    {
        hasTriggeredFirstConversation = true;  // Mark first conversation as triggered

        // Start the sequence: delay -> set computer active -> start second conversation
        StartCoroutine(ShowComputerAndStartSecondDialogue());
    }

    private IEnumerator ShowComputerAndStartSecondDialogue()
    {
        yield return new WaitForSeconds(2f);  // Delay before showing computer (adjust as needed)

        // Set the computer active
        computer.SetActive(true);

        yield return new WaitForSeconds(1f);  // Short delay before starting the second conversation

        // Start the second conversation only if it hasn't been triggered before
        if (!hasTriggeredSecondConversation)
        {
            trigger.StartSecondDialogue(this);
            hasTriggeredSecondConversation = true;  // Mark second conversation as triggered
        }
    }
}
