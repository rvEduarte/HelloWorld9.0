using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class KurtClosePanel : MonoBehaviour
{

    public PlayerController playerController; // Reference to the player controller

    public void ClosePanel()
    {
        TriggerTutorial.disableMove = true; //disable Move
        playerController.enabled = true;


        TriggerTutorial.disableJump = false; //disable jumping
    }
}
