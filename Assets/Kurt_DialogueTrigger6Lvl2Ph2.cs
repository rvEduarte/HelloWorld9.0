using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_DialogueTrigger6Lvl2Ph2 : MonoBehaviour
{
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<KurtNew_DialogueManagerLvl2Ph2>().OpenDialogue(message, actor);
        }
    }
}
