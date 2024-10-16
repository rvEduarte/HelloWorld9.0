using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManageV2 : MonoBehaviour
{
    public GameObject computer;
    public ElevatorScript elev;
    public PlayerScoreScriptableObject playerData;

    public TMP_Text completionText;

    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public TMP_Text questionTxt;
    public TextMeshProUGUI quizScoreText;
    public TextMeshProUGUI scoreBoardText;

    public GameObject completionPanel;
    public GameObject quizPanel;

    public TMP_Text questionPrefab; // Prefab for question text
    public Transform questionsContainer; // Parent container for questions

    int totalQuestions = 0;
    public static int scoreCount = 0;

    // List to keep track of asked questions
    private List<int> askedQuestions = new List<int>();

    public void StartComputer()
    {
        // Reset the score and asked questions at the start of each quiz attempt
        ResetQuiz();
        generateQuestion();
    }

    public void GameOver()
    {
        LeanTween.scale(quizPanel, Vector2.zero, 0.5f);
        LeanTween.scale(completionPanel, Vector2.one, 0.5f);
        quizScoreText.text = scoreCount + "/" + totalQuestions;
        scoreBoardText.text = playerData.scoreQuizPhase3.ToString();

        // Reset asked questions so the quiz can be retaken
        askedQuestions.Clear();

        if (scoreCount > 2)
        {
            Debug.Log("Passed");
            completionText.text = "Passed!";
            completionText.color = Color.green;
        }
        else
        {
            Debug.Log("Failed");
            completionText.text = "Failed!";
            completionText.color = Color.red;
        }
    }

    public void correct()
    {
        scoreCount += 1;
        int pointsToAdd = 10;

        playerData.scoreQuizPhase3 += pointsToAdd;

        StartCoroutine(DisplayResultAndNext(true));
    }

    public void wrong()
    {
        int pointsToDeduct = 1;

        playerData.wrongQuizPhase3 += pointsToDeduct;

        StartCoroutine(DisplayResultAndNext(false));
    }

    private IEnumerator DisplayResultAndNext(bool isCorrect)
    {
        yield return new WaitForSeconds(0.2f);

        foreach (var option in options)
        {
            option.GetComponent<Image>().color = Color.white;
        }

        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScriptV2_RV>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrestAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScriptV2_RV>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (askedQuestions.Count < QnA.Count)
        {
            // Keep generating a random question until we find one that hasn't been asked yet
            do
            {
                currentQuestion = Random.Range(0, QnA.Count);
            } while (askedQuestions.Contains(currentQuestion));

            // Mark this question as asked
            askedQuestions.Add(currentQuestion);

            // Clear previous questions
            foreach (Transform child in questionsContainer)
            {
                Destroy(child.gameObject);
            }

            // Instantiate new question
            TMP_Text newQuestion = Instantiate(questionPrefab, questionsContainer);
            newQuestion.text = QnA[currentQuestion].QuestionText.text;
            newQuestion.font = QnA[currentQuestion].QuestionText.font;

            SetAnswer();
        }
        else
        {
            Debug.Log("Out of questions");
            GameOver();
        }
    }

    public void StartResetCoroutine(Image button)
    {
        StartCoroutine(ResetButtonColor(button));
    }

    IEnumerator ResetButtonColor(Image button)
    {
        yield return new WaitForSeconds(0.2f);
        button.color = Color.white;
    }

    public void CallElevator()
    {
        if (scoreCount > 2)
        {
            Debug.Log("Passed");
            computer.SetActive(false);
            elev.FinishQuiz();
        }
        else
        {
            completionText.color = Color.red;
        }
    }

    // Method to reset quiz state
    private void ResetQuiz()
    {
        scoreCount = 0; // Reset score
        askedQuestions.Clear(); // Clear asked questions
        totalQuestions = QnA.Count; // Get the total questions
    }
}
