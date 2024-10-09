using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using TarodevController;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerDisMObj : MonoBehaviour
{
    [SerializeField] private GameObject ComputerPanel;

    [SerializeField] private GameObject hintText, exla;

    private bool pickUpAllowed;

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

            LeanTween.scale(ComputerPanel, Vector2.one, 0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.SetActive(true);
            pickUpAllowed = true;
            exla.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            hintText.SetActive(false);
            pickUpAllowed = false;
            exla.SetActive(true);
        }
    }
}
