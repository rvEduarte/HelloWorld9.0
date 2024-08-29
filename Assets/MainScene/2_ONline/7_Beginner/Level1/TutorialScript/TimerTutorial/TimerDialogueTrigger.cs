using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimerMessage
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class TimerActor
{
    public string name;
    public Sprite sprite;
}

public class TimerDialogueTrigger : MonoBehaviour
{
    public TimerMessage[] message;
    public TimerActor[] actor;

    public void StartDialogue()
    {
        FindObjectOfType<TimerDialogueManager>().OpenDialogue(message, actor);
    }
}
