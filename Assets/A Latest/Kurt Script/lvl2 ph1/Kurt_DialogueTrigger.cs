using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_DialogueTrigger : MonoBehaviour
{
    public TriggerMessage[] messages;
    public TriggerActor[] actors;

    public void StartDialogue(Kurt_NPC npcRef)
    {
        // Start the dialogue and pass the NPC reference to the manager
        FindObjectOfType<Kurt_DialogueManager>().OpenDialaogue(messages, actors, npcRef);
    }
}

[System.Serializable]
public class TriggerMessage
{
    public int triggerActorId;
    public string triggerMessage;
}

[System.Serializable]
public class TriggerActor
{
    public string triggerName;
    public Sprite triggerSprite;
}
