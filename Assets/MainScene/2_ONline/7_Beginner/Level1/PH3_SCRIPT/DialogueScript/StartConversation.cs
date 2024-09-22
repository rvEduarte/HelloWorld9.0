using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConversation : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;

    private void Start()
    {
        StartCoroutine(StartConversationPanel());
    }

    IEnumerator StartConversationPanel()
    {
        // waiting state
        yield return new WaitForSeconds(1);
        dialogueTrigger.StartDialogue();
    }
}
