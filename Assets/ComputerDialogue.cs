using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerDialogue : MonoBehaviour
{
    public ZoomMessage[] message;
    public ZoomActor[] actor;
    
    public void StartDialogue()
    {
        FindObjectOfType<ComputerDialogueManager>().OpenDialogue(message, actor);
    }
}
