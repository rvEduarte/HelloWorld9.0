using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtClosePanel : MonoBehaviour
{
    public void ClosePanel()
    {
        TriggerTutorial.disableMove = true; //disable Move
        TriggerTutorial.disableJump = false; //disable jumping
    }
}
