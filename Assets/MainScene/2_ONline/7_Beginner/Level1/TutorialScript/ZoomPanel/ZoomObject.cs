using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomObject : MonoBehaviour
{
    public CinemachineVirtualCamera jigsawCamera;
    public GameObject triggerZoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = false; //disable jumping
            RunningTimerLevel1Ph1.timerStop = false; // disable time
            jigsawCamera.Priority = 11;


            StartCoroutine(backCamera());
        }
    }

    IEnumerator backCamera()
    {
        yield return new WaitForSeconds(5);
        jigsawCamera.Priority = 0;

        StartCoroutine (enableMovement());
    }
    IEnumerator enableMovement()
    {
        yield return new WaitForSeconds(5);
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
        RunningTimerLevel1Ph1.timerStop = true; //enable time
        triggerZoom.SetActive(false); //disable trigger
    }

}
