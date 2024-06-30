using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManage quizManager;
    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correst Answer");
            quizManager.correct();
        }
        else
        {
            Debug.Log("Wrong Answer");
            quizManager.wrong();
        }
    }
}
