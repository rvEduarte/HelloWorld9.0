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

    // UI elements for feedback
    public TextMeshProUGUI feedbackText;
    public GameObject feedbackOutputPanel; // Panel or object for displaying feedback

    public GameObject quizPanel;

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

    // Delay before closing the feedback or proceeding to the next question
    public float delay = 2f;

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

        // Hide feedback panel initially
        feedbackOutputPanel.SetActive(false);
    }

    // Check if the selected answer is correct
    void CheckAnswer(int index)
    {
        feedbackOutputPanel.SetActive(true); // Show feedback panel

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

        // Hide feedback panel before moving to the next question
        feedbackOutputPanel.SetActive(false);

        // Check if there are more questions
        if (currentQuestionIndex < questions.Count - 1)
        {
            currentQuestionIndex++;
            SetupQuestion(currentQuestionIndex);
        }
        else
        {
            // No more questions left, close the quiz panel
            quizPanel.SetActive(false);
        }
    }
}
