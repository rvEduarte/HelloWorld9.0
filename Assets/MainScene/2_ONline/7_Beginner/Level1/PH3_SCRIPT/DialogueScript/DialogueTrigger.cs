using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}

public class DialogueTrigger : MonoBehaviour
{
    public Message[] message;
    public Actor[] actor;

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(message, actor);
    }
}
