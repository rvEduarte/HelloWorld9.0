using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_NPC : MonoBehaviour
{
    public Kurt_DialogueTrigger trigger;
    private bool hasTriggered = false;  // Flag to ensure one-time conversation

    public GameObject gameObj;

    private void Start()
    {
        // Check if the gameObj is assigned before trying to deactivate it
        if (gameObj != null)
        {
            gameObj.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters and if the conversation hasn't been triggered
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            trigger.StartDialogue(this);  // Pass the NPC reference

            // Activate the gameObj only if it's assigned
            if (gameObj != null)
            {
                gameObj.SetActive(true);
            }
        }
    }

    // This method is called when the conversation ends
    public void OnConversationEnd()
    {
        hasTriggered = true;  // Mark conversation as triggered
    }
}
