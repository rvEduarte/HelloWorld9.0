using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryFuture : MonoBehaviour
{
    [SerializeField] private GameObject battery;
    [SerializeField] private float positionGameObject;
    public ComputerDialogueThree computerDialogueThree;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LeanTween.moveY(battery, positionGameObject, 3f);
            BatteryComputer.battery1 = true;

            if(BatteryComputer.battery1 && BatteryComputer.battery2 == true)
            {
                Debug.Log("StartConversation");
                TriggerTutorial.disableMove = false; //disable Move
                TriggerTutorial.disableJump = true; //disable jumping
                computerDialogueThree.StartDialogue();
            }
        }
    }
}
