using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_NPC : MonoBehaviour
{
    public Kurt_DialogueTrigger trigger;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            trigger.StartDialogue();
        }

    }

}
