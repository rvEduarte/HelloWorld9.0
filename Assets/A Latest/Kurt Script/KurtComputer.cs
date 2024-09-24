using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtComputer : MonoBehaviour
{
    [SerializeField] public GameObject tite; // panel

    public GameObject hintText;

    private bool pickUpAllowed;

    public static bool disableInteract;

    private void Start()
    {
        hintText.SetActive(false);
        disableInteract = false;
    }

    private void Update()
    {
        if (!disableInteract) return;

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            disableInteract = false;
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            LeanTween.scale(tite, Vector2.one, 0.5f); // variable para sa panel
           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!disableInteract) return;
        if (collision.gameObject.name.Equals("Player"))
        {
            hintText.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            hintText.SetActive(false);
            pickUpAllowed = false;
        }
    }
}
