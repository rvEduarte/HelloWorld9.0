using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Kurt_CodeInputManager : MonoBehaviour
{
    // Reference to the input fields in the UI
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public TMP_InputField inputField4;
    public TMP_InputField inputField5;

    // Correct answers
    private string[] correctAnswers = { "int", "int", "WriteLine", "WriteLine", "targetHeight" };

    // Reference to the feedback texts
    public TextMeshProUGUI feedbackText1;
    public TextMeshProUGUI feedbackText2;
    public TextMeshProUGUI feedbackText3;
    public TextMeshProUGUI feedbackText4;
    public TextMeshProUGUI feedbackText5;

    // Reference to the moving platform script
    public MovingPlatform movingPlatform;  // Link to the MovingPlatform script

    // Function to validate the answers when Submit button is clicked
    public void ValidateAnswers()
    {
        // Track whether all answers are correct
        bool allCorrect = true;

        // Check each input field and provide feedback
        allCorrect &= CheckAnswer(inputField1, correctAnswers[0], feedbackText1);
        allCorrect &= CheckAnswer(inputField2, correctAnswers[1], feedbackText2);
        allCorrect &= CheckAnswer(inputField3, correctAnswers[2], feedbackText3);
        allCorrect &= CheckAnswer(inputField4, correctAnswers[3], feedbackText4);
        allCorrect &= CheckAnswer(inputField5, correctAnswers[4], feedbackText5);

        // If all answers are correct, start moving the platform
        if (allCorrect)
        {
            movingPlatform.StartMoving();
        }
    }

    // Helper function to check each input field's answer
    private bool CheckAnswer(TMP_InputField inputField, string correctAnswer, TextMeshProUGUI feedbackText)
    {
        bool isCorrect = inputField.text == correctAnswer;

        // Change feedback text color and provide feedback
        feedbackText.text = isCorrect ? "Correct!" : "Incorrect!";
        feedbackText.color = isCorrect ? Color.green : Color.red;

        // Change the input field text color based on correctness
        inputField.textComponent.color = isCorrect ? Color.green : Color.red;

        return isCorrect;
    }
}
