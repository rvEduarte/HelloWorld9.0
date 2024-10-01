using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KurT_LoopQuiz : MonoBehaviour
{
    // UI elements for the quiz
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    // UI element for feedback
    public TextMeshProUGUI feedbackText;

    public GameObject quizPanel;

    // Reference to the moving platform script
    public MovingPlatform movingPlatform;  // Link to the MovingPlatform script

    // Reference to the PlayerMovement script (disabling/enabling player movement)
    public MonoBehaviour playerMovement;

    // Delay before proceeding to the next question
    public float delay = 2f;

    // Quiz data structure
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    // List of questions for the quiz
    public List<Question> questions;

    // Index for tracking the current question
    private int currentQuestionIndex = 0;

    void Start()
    {
        if (questions.Count > 0)
        {
            SetupQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.LogError("No questions available in the quiz!");
        }
    }

    // Set up the question and answer buttons for the current question
    void SetupQuestion(int questionIndex)
    {
        Question currentQuestion = questions[questionIndex];
        questionText.text = currentQuestion.questionText;

        // Set up answer buttons with the corresponding answers
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
                int index = i; // Local copy to avoid closure issue
                answerButtons[i].onClick.RemoveAllListeners(); // Remove old listeners
                answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
                answerButtons[i].gameObject.SetActive(true);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false); // Hide buttons that are not needed
            }
        }

        // Hide feedback text initially
        feedbackText.gameObject.SetActive(false);
    }

    // Check if the selected answer is correct
    void CheckAnswer(int index)
    {
        feedbackText.gameObject.SetActive(true); // Show feedback text

        if (index == questions[currentQuestionIndex].correctAnswerIndex)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            StartCoroutine(ProceedToNextQuestionAfterDelay());
        }
        else
        {
            feedbackText.text = "Incorrect. Try again!";
            feedbackText.color = Color.red;
        }
    }

    // Coroutine to proceed to the next question after a delay
    IEnumerator ProceedToNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Hide feedback text before moving to the next question
        feedbackText.gameObject.SetActive(false);

        // Check if there are more questions
        if (currentQuestionIndex < questions.Count - 1)
        {
            currentQuestionIndex++;
            SetupQuestion(currentQuestionIndex);
        }
        else
        {
            // No more questions left, close the quiz panel and trigger the platform movement
            quizPanel.SetActive(false);
            EnablePlayerMovement(); // Re-enable player movement
            StartPlatform(); // Start moving the platform
        }
    }

    // Function to re-enable player movement when the quiz is finished
    private void EnablePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Re-enable the player's movement script
        }
    }

    // Function to start the platform movement when the quiz ends
    private void StartPlatform()
    {
        if (movingPlatform != null)
        {
            movingPlatform.StartMoving(); // Trigger the platform movement
        }
    }
}
