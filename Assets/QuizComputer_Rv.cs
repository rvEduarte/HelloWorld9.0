using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizComputer_Rv : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;

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
            playerData.scoreQuizPhase3 = 0;
            playerData.wrongQuizPhase3 = 0;
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
