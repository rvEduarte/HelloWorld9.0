using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointScriptLevel1Ph1_Intermediate : MonoBehaviour
{
    [SerializeField] public GameObject gameCompletion;

    public RunningTimerLevel1Ph1Intermediate timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameCompletion.SetActive(true);
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping
            timer.isPicked = true;
        }
    }
}
