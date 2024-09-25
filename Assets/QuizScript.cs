using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    [Header("Quiz UI Elements")]
    public GameObject quizPanel;           // The panel that shows the quiz
    public Text questionText;              // Text component to display the question
    public Button option1Button;           // Button for the first answer option
    public Button option2Button;           // Button for the second answer option
    public Button option3Button;           // Button for the third answer option
    public Text feedbackText;              // Feedback text to show if the answer is correct or not

    [Header("Platform Control")]
    public GameObject platform;            // The platform that will move up
    public Animator platformAnimator;      // Animator component to control platform movement

    private string correctAnswer = "int";  // The correct answer for the quiz question

    private void Start()
    {
        // Hide the quiz panel and feedback at the start
        quizPanel.SetActive(false);
        feedbackText.gameObject.SetActive(false);

        // Add listeners to the option buttons
        option1Button.onClick.AddListener(() => CheckAnswer(option1Button.GetComponentInChildren<Text>().text));
        option2Button.onClick.AddListener(() => CheckAnswer(option2Button.GetComponentInChildren<Text>().text));
        option3Button.onClick.AddListener(() => CheckAnswer(option3Button.GetComponentInChildren<Text>().text));
    }

    // Function to show the quiz panel
    public void ShowQuiz()
    {
        quizPanel.SetActive(true);
        feedbackText.gameObject.SetActive(false);
        questionText.text = "Which keyword is used to declare an integer variable in C#?";
        option1Button.GetComponentInChildren<Text>().text = "string";
        option2Button.GetComponentInChildren<Text>().text = "float";
        option3Button.GetComponentInChildren<Text>().text = "int";
    }

    // Function to check if the selected answer is correct
    private void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            feedbackText.text = "Correct! The platform will now rise.";
            feedbackText.color = Color.green;
            ActivatePlatform(); // Call function to move the platform
        }
        else
        {
            feedbackText.text = "Incorrect. Try again!";
            feedbackText.color = Color.red;
        }
        feedbackText.gameObject.SetActive(true);
    }

    // Function to activate the platform's upward movement
    private void ActivatePlatform()
    {
        quizPanel.SetActive(false); // Hide the quiz panel
        platform.SetActive(true); // Make sure the platform is active
        platformAnimator.SetTrigger("Rise"); // Trigger the platform's rise animation
    }
}
