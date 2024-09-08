using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class JigsawMessage
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class JigsawActor
{
    public string name;
    public Sprite sprite;
}
public class JigsawTriggerPanel : MonoBehaviour
{
    public JigsawMessage[] message;
    public JigsawActor[] actor;

    public void StartDialogue()
    {
        FindObjectOfType<JigsawDialogueManager>().OpenDialogue(message, actor);
    }
}
