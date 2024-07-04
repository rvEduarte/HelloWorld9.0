using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class QuizManage : MonoBehaviour
{
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
    int scoreCount = 0;
    int scoreBoard = 0;

    private void Start()
    {
        //movingPlatform.SetActive(false);
        totalQuestions = QnA.Count;
        completionPanel.SetActive(false);
        
        generateQuestion();
        //QnA.RemoveAt(currentQuestion);
    }

    public void GameOver()
    {
        completionPanel.SetActive(true);
        quizPanel.SetActive(false);
        quizScoreText.text = scoreCount +"/"+ totalQuestions;
        scoreBoardText.text = scoreBoard.ToString();
    }

    public void correct()
    {
        scoreCount += 1;
        scoreBoard += 10;

        StartCoroutine(DisplayResultAndNext(true));
    }

    public void wrong()
    {
        // Change button color for wrong answer
        foreach (var option in options)
        {
            var answerScript = option.GetComponent<AnswersScript>();
            if (!answerScript.isCorrect)
            {
                option.GetComponent<Image>().color = Color.red;
            }
        }

        StartCoroutine(DisplayResultAndNext(false));
    }

    private IEnumerator DisplayResultAndNext(bool isCorrect)
    {
        // Wait for 0.2 seconds before resetting button colors
        yield return new WaitForSeconds(0.2f);

        // Reset button colors
        foreach (var option in options)
        {
            option.GetComponent<Image>().color = Color.white;
        }

        // Remove current question and generate the next one
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswer ()
    {    
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswersScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrestAnswer == i+1)
            {
                options[i].GetComponent<AnswersScript>().isCorrect = true;
            } 
        }
    }

    void generateQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            // Clear previous questions
            foreach (Transform child in questionsContainer)
            {
                Destroy(child.gameObject);
            }

            // Instantiate new question
            TMP_Text newQuestion = Instantiate(questionPrefab, questionsContainer);
            newQuestion.text = QnA[currentQuestion].QuestionText.text;
            newQuestion.font = QnA[currentQuestion].QuestionText.font; // Copy font settings if needed

            SetAnswer();
        }
        else
        {
            Debug.Log("Out of question");
            GameOver();
        }
    }

    public void StartResetCoroutine(Image button)
    {
        StartCoroutine(ResetButtonColor(button));
    }

    IEnumerator ResetButtonColor(Image button)
    {
        // Wait for 0.1 seconds before resetting the button color
        yield return new WaitForSeconds(0.2f);

        // Reset the button color to the original color (white in this case)
        button.color = Color.white;
    }
}