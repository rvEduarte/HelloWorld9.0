using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarterLevel1Phase2 : MonoBehaviour
{
    void Start()
    {
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }

}
