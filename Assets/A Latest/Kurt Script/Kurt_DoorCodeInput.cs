using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_DoorCodeInput : MonoBehaviour
{
    // Reference to the input fields in the UI
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public TMP_InputField inputField4;
  

    // Correct answers
    private string[] correctAnswers = { "bool", "true", "Console", "WriteLine" };

    // Reference to the feedback texts
    public TextMeshProUGUI feedbackText1;
    public TextMeshProUGUI feedbackText2;
    public TextMeshProUGUI feedbackText3;
    public TextMeshProUGUI feedbackText4;

    // Reference to the moving platform script
    public MovingPlatform movingPlatform;  // Link to the MovingPlatform script

    // Function to validate the answers when Submit button is clicked
    public void ValidateAnswers()
    {
        // Track whether all answers are correct
        bool allCorrect = true;

        // Check each input field and provide feedback
        if (!CheckAnswer(inputField1, correctAnswers[0], feedbackText1)) allCorrect = false;
        if (!CheckAnswer(inputField2, correctAnswers[1], feedbackText2)) allCorrect = false;
        if (!CheckAnswer(inputField3, correctAnswers[2], feedbackText3)) allCorrect = false;
        if (!CheckAnswer(inputField4, correctAnswers[3], feedbackText4)) allCorrect = false;
        //if (!CheckAnswer(inputField5, correctAnswers[4], feedbackText5)) allCorrect = false;

        // If all answers are correct, start moving the platform
        if (allCorrect)
        {
            movingPlatform.StartMoving();
        }
    }

    // Helper function to check each input field's answer
    private bool CheckAnswer(TMP_InputField inputField, string correctAnswer, TextMeshProUGUI feedbackText)
    {
        if (inputField.text == correctAnswer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            return true;
        }
        else
        {
            feedbackText.text = "Incorrect!";
            feedbackText.color = Color.red;
            return false;
        }
    }
}