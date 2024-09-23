using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerLevel1Ph2 : MonoBehaviour
{
    [SerializeField] public RectTransform WriteDown;
    [SerializeField] public RectTransform WriteUp;
    [SerializeField] public RectTransform WriteLineUp;
    [SerializeField] public RectTransform WriteLineDown;

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
            WriteDown.anchoredPosition = new Vector2(-519f, -5f);
            WriteUp.anchoredPosition = new Vector2(-66.35299f, -5f);
            WriteLineUp.anchoredPosition = new Vector2(-66.35299f, -191f);
            WriteLineDown.anchoredPosition = new Vector2(-519f, -191f);
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
