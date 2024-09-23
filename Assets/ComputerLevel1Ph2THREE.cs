using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerLevel1Ph2THREE : MonoBehaviour
{
    [SerializeField] public RectTransform Write;
    [SerializeField] public RectTransform WriteLine;

    [SerializeField] public GameObject ComputerPanel;

    public GameObject hintText;

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
            Write.anchoredPosition = new Vector2(-519f, -5f);
            WriteLine.anchoredPosition = new Vector2(-66.35299f, -5f);
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
