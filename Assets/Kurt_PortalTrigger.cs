using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_PortalTrigger : MonoBehaviour
{
   public KurtMessage[] message;
    public KurtActor[] actor;

    private bool hasTriggered = false;

    public static bool portalTrigger;

    private void Start()
    {
        portalTrigger = false;

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
