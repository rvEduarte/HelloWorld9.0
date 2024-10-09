using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointScriptLevel1Ph3 : MonoBehaviour
{
    [SerializeField] public GameObject gameCompletion;

    public RunningTimerLevel1Ph3 timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            gameCompletion.SetActive(true);

            timer.isPicked = true;

        }
    }
}
