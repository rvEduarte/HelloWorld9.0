using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishPointScript : MonoBehaviour
{
    [SerializeField] public GameObject gameCompletion;

    public Button tutorialButton;
    public Button backpackButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameCompletion.SetActive(true);
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            tutorialButton.interactable = false;
            backpackButton.interactable = false;

        }
    }
}
