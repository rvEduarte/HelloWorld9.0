using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeConsoleComputer : MonoBehaviour
{

    [SerializeField] public GameObject ComputerPanel;

    public ComputerDialogueFour computerDialogueFour;

    public GameObject hintText;

    public static bool pickUpAllowed;

    private bool onceTalk;

    public static bool onceTalkDialogue;
    public static bool onceTalkAllowed;

    private void Start()
    {
        hintText.SetActive(false);
        
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            if(!onceTalk)
            {
                Debug.LogError("COMPUTERDIALOGUEFOUR");
                pickUpAllowed = false;
                onceTalk = true;
                computerDialogueFour.StartDialogue();
            }
            else
            {
                LeanTween.scale(ComputerPanel, Vector2.one, 0.5f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.SetActive(true);
            pickUpAllowed = true;
            onceTalkDialogue = true;
            onceTalkAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            hintText.SetActive(false);
            pickUpAllowed = false;
            onceTalkAllowed = false;
        }
    }
}
