using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionalScript : MonoBehaviour
{
    public int tite = 1;
    public void EnableMovement()
    {
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }
}
