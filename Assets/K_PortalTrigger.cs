using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PortalTrigger : MonoBehaviour
{
    public Message[] message;
    public Actor[] actor;
    private bool hasTriggered = false; // Add a flag to track if the player has triggered the dialogue

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player entered and if the dialogue has not been triggered yet
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            FindObjectOfType<BlockDialogue>().OpenDialogue(message, actor);
            hasTriggered = true; // Set the flag to true after the first trigger
        }
    }
}
