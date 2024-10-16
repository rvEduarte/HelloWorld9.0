using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScriptV2_RV : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManageV2 quizManager;
    public Image buttonCol;

    void Start()
    {
        // Store the original color of the button when the script starts
        buttonCol.color = Color.white;
    }
    public void Answer()
    {
        if (isCorrect)
        {
            buttonCol.color = Color.green;
            Debug.Log("Correst Answer");
            quizManager.correct();
        }
        else
        {
            buttonCol.color = Color.red;
            Debug.Log("Wrong Answer");
            quizManager.wrong();
        }

        quizManager.StartResetCoroutine(buttonCol);

        /*if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ResetButtonColor());
        }*/
    }
    /*IEnumerator ResetButtonColor()
    {
        // Wait for 1 second before resetting the button color
        yield return new WaitForSeconds(0.1f);

        // Reset the button color to the original color (white in this case)
        buttonCol.color = Color.white;
    }*/
}
