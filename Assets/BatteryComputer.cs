using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryComputer : MonoBehaviour
{
    [SerializeField] public GameObject ComputerPanel;

    public ComputerDialogue computerTrigger;

    public GameObject hintText;
    public GameObject computer;

    private bool pickUpAllowed;

    public static bool battery1;
    public static bool battery2;

    public bool onceZoom;

    public static bool onceZoomCam;

    private void Start()
    {
        hintText.SetActive(false);
        computer.SetActive(false);
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            if(battery1 && battery2 == true)
            {
                LeanTween.scale(ComputerPanel, Vector2.one, 0.5f);
            }
            else if(!onceZoom)
            {
                computer.SetActive(true);
                Debug.Log("Once");
                onceZoom = true;
                computerTrigger.StartDialogue();
            }
            else
            {
                Debug.Log("YES");
                TriggerTutorial.disableMove = true; //enable Move
                TriggerTutorial.disableJump = false; //enable jumping
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            hintText.SetActive(false);
            pickUpAllowed = false;
        }
    }
}
