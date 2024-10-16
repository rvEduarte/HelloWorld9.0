using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_TriggerLvl2Ph1 : MonoBehaviour
{
    public KurtMessage[] message;
    public KurtActor[] actor;

    private bool hasTriggered = false;

    public static bool triggerPh1;

    private void Start()
    {
        triggerPh1 = false;

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
        FindObjectOfType<Kurt_DialogueManagerLvl2Ph1>().OpenDialogue(message, actor);
    }
}
