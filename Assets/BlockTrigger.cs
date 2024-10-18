using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    public Message[] message;
    public Actor[] actor;

    public DialogueManager kurtTriggerPh1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<BlockDialogue>().OpenDialogue(message, actor);
        }
    }
}
