using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_GameStartLvl : MonoBehaviour
{
    void Start()
    {
        TriggerTutorial.disableMove = true; // Disable movement initially
        TriggerTutorial.disableJump = false; // Enable jumping
    }
}
