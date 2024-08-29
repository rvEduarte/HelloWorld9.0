using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTimer : MonoBehaviour
{
    public TimerDialogueTrigger timerDialogueTrigger;

    public GameObject timerPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            TriggerTutorial.disableMove = false; // disableMove
            timerPanel.SetActive(true);

            StartCoroutine(StartConversationPanel());
        }
    }

    IEnumerator StartConversationPanel()
    {
        // waiting state
        yield return new WaitForSeconds(0.5f);
        timerDialogueTrigger.StartDialogue();
    }

}
