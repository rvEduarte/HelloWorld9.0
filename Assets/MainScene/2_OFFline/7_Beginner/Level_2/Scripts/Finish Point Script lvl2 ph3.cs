using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointScriptlvl2ph3 : MonoBehaviour
{
    [SerializeField] public GameObject gameCompletion;

    public RunningTimerLvl2Ph3 timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameCompletion.SetActive(true);

            timer.isPicked = true;

        }
    }
}
