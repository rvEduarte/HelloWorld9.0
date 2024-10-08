using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_DialogueTriggerLvl2Ph2 : MonoBehaviour
{
    public TriggerMessage2[] firstMessages;  // First conversation
    public TriggerActor2[] firstActors;

    public TriggerMessage2[] secondMessages;  // Second conversation after computer appears
    public TriggerActor2[] secondActors;

    public void StartDialogue(K_NPCLvl2Ph2 npcRef)
    {
        // Start the first dialogue and pass the NPC reference to the manager
        FindObjectOfType<K_DialogueManagerLvl2Ph2>().OpenDialaogue(firstMessages, firstActors, npcRef);
    }

    public void StartSecondDialogue(K_NPCLvl2Ph2 npcRef)
    {
        // Start the second dialogue after the computer appears
        FindObjectOfType<K_DialogueManagerLvl2Ph2>().OpenDialaogue(secondMessages, secondActors, npcRef);
    }
}

[System.Serializable]
public class TriggerMessage2
{
    public int triggerActorId;
    public string triggerMessage;
}

[System.Serializable]
public class TriggerActor2
{
    public string triggerName;
    public Sprite triggerSprite;
}