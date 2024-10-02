 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZoomMessage
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class ZoomActor
{
    public string name;
    public Sprite sprite;
}
public class ZoomDialogueTrigger : MonoBehaviour
{
    public ZoomMessage[] message;
    public ZoomActor[] actor;

    public void StartDialogue()
    {
        FindObjectOfType<ZoomDialogueManager>().OpenDialogue(message, actor);
    }
}
