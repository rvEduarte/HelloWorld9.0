using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryComputer : MonoBehaviour
{
    [SerializeField] public GameObject ComputerPanel;


    public TMP_InputField inputField;
    public ComputerDialogue computerTrigger;
    public ComputerDialogueTwo computerTriggerTwo;

    public GameObject hintText;
    public GameObject computer;

    public static bool pickUpAllowed;

    public static bool battery1;
    public static bool battery2;

    public bool onceZoom = false;

    public static bool onceZoomCam;

    private void Start()
    {
        battery1 = false;
        battery2 = false;
        inputField.interactable = false;
        hintText.SetActive(false);
        computer.SetActive(false);
        onceZoomCam = true;
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            if(battery1 && battery2 == true)
            {
                inputField.interactable = true;
                LeanTween.scale(ComputerPanel, Vector2.one, 0.5f);
            }
            else if(!onceZoom)
            {
                pickUpAllowed = false;
                computer.SetActive(true);
                Debug.Log("Once");
                onceZoom = true;
                computerTrigger.StartDialogue();
            }
            else
            {
                Debug.Log("YES");
                computerTriggerTwo.StartDialogue();
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
