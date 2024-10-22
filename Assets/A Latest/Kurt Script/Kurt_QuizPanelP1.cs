using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Kurt_QuizPanelP1 : MonoBehaviour
{
    public TMP_InputField[] inputFields;  // Array of input fields to handle multiple quizzes dynamically
    public TextMeshProUGUI[] feedbackTexts;  // Array for feedback texts corresponding to input fields
    public GameObject outputPanel;
    public GameObject platformObject;
    public float scaleDuration = 0.5f;
    public float closeDelay = 2f; // Set this to the desired delay duration in seconds
    public float platformMoveDelay = 3f; // Set this to the desired delay duration before the platform moves
    // Reference to error and success images
    public GameObject errorImage;
    public GameObject successImage;

    void Update()
    {
        // Check if Enter key is pressed and call ValidateAnswers
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ValidateAnswers();
        }

        ShowHideScript.stopMovement = false; //The script block movement

    }

    public void ValidateAnswers()
    {
        // Check if the number of input fields matches the expected number
        if (inputFields.Length != 5)
        {
            Debug.LogError("Expected 5 input fields!");
            return;
        }

        // Track whether all answers are correct
        bool allCorrect = true;

        // Validate each input field with specific answers
        allCorrect &= CheckAnswer(inputFields[0], "int", feedbackTexts[0]);       // First input field: 'int'
        allCorrect &= CheckAnswer(inputFields[1], "int", feedbackTexts[1]);       // Second input field: 'int'
        allCorrect &= CheckMultipleAnswers(inputFields[2], new string[] { "Write", "WriteLine" }, feedbackTexts[2]); // Third input field: 'Write' or 'WriteLine'
        allCorrect &= CheckMultipleAnswers(inputFields[3], new string[] { "Write", "WriteLine" }, feedbackTexts[3]); // Fourth input field: 'Write' or 'WriteLine'
        allCorrect &= CheckAnswer(inputFields[4], "bridgeHeight", feedbackTexts[4]);  // Fifth input field: 'bridgeHeight'

        // Show the output panel after validation
        ShowOutputPanel(allCorrect);

        // If all answers are correct, start moving the platform after a delay and close the panel
        if (allCorrect)
        {
            successImage.SetActive(true); // Show success image
            StartCoroutine(ClosePanelWithScale());
            StartCoroutine(StartPlatformAfterDelay());
        }
        else
        {
            errorImage.SetActive(true); // Show error image
            StartCoroutine(BlinkErrorImage());
        }
    }

    // Coroutine to start the platform after a delay
    private IEnumerator StartPlatformAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.moveLocalY(platformObject, -0.54f, 2.5f);
    }

    // Helper function to check if the answer matches the expected single correct answer
    private bool CheckAnswer(TMP_InputField inputField, string correctAnswer, TextMeshProUGUI feedbackText)
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            feedbackText.text = "Answer required!";
            feedbackText.color = Color.red;
            return false;
        }

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

    // Helper function to check if the answer matches any of the multiple correct answers
    private bool CheckMultipleAnswers(TMP_InputField inputField, string[] correctAnswers, TextMeshProUGUI feedbackText)
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            feedbackText.text = "Answer required!";
            feedbackText.color = Color.red;
            return false;
        }

        foreach (string correctAnswer in correctAnswers)
        {
            if (inputField.text == correctAnswer)
            {
                feedbackText.text = "Correct!";
                feedbackText.color = Color.green;
                return true;
            }
        }

        feedbackText.text = "Incorrect!";
        feedbackText.color = Color.red;
        return false;
    }

    // Function to show the output panel
    private void ShowOutputPanel(bool allCorrect)
    {
        outputPanel.SetActive(true); // Activate the output panel

        // Update the output panel's content based on correctness
        TextMeshProUGUI outputText = outputPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (outputText != null)
        {
            outputText.text = allCorrect ? "All answers are correct!" : "Syntax Denied. Please try again.";
        }
    }

    // Coroutine to blink the error image
    private IEnumerator BlinkErrorImage()
    {
        for (int i = 0; i < 3; i++)
        {
            errorImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            errorImage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Coroutine to scale down and close the panel after a delay
    private IEnumerator ClosePanelWithScale()
    {
        // Wait for the specified delay before starting to close the panel
        yield return new WaitForSeconds(closeDelay);

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float time = 0;

        // Smoothly scale down the panel
        while (time < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, time / scaleDuration);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure final scale is zero and deactivate the panel
        transform.localScale = targetScale;
        gameObject.SetActive(false);

        Debug.Log("Panel Closed...");

        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping

        // Reset the cursor to the default when the panel is closed
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
