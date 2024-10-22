using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_TriggerLvl2Ph2 : MonoBehaviour
{
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;

    public static bool trigger1Ph2;
    private void Start()
    {
        trigger1Ph2 = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasTriggered)
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
