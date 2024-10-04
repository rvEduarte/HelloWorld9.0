  using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishPointScriptLevel1Ph2 : MonoBehaviour
{
    [SerializeField] public GameObject gameCompletion;

    public RunningTimerLevel1Ph2 timer;

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
