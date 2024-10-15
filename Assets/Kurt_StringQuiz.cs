using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_StringQuiz : MonoBehaviour
{
    public TMP_InputField inputField;  // Single input field
    public string correctAnswer = "\"Hello World\"";  // Single correct answer
    public TextMeshProUGUI feedbackText;  // Single feedback text

    public GameObject outputPanel;  // Reference to the output panel
    public GameObject errorImage;  // Reference to error image
    public GameObject successImage;  // Reference to success image

    public float scaleDuration = 0.5f;  // Scaling duration for closing panel
    public float closeDelay = 2f;  // Delay before closing the panel
    public float outputPanelDisplayTime = 3f;  // Time to display output panel

    public GameObject laser2;
    public GameObject laserTrigger2;

    void Start()
    {
        // Attach listener for Enter key
        inputField.onEndEdit.AddListener(delegate { CheckEnterKey(); });
    }

    // Method to detect if the Enter key was pressed
    private void CheckEnterKey()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ValidateAnswer();  // Trigger answer validation when Enter is pressed
        }
    }

    public void ValidateAnswer()
    {
        // Check if the input field matches the correct answer
        bool isCorrect = CheckAnswer(inputField.text, correctAnswer);

        // Show feedback and update UI based on correctness
        ShowOutputPanel(isCorrect);

        if (isCorrect)
        {
            successImage.SetActive(true);  // Show success image
            StartCoroutine(ClosePanelWithScale(true));  // Close the panel with success behavior
        }
        else
        {
            errorImage.SetActive(true);  // Show error image
            StartCoroutine(BlinkErrorImage());
        }
    }

    // Helper function to check the input field's answer
    private bool CheckAnswer(string input, string correctAnswer)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            feedbackText.text = "Answer required!";
            feedbackText.color = Color.red;
            return false;
        }

        // Check if the input matches the correct answer (case-insensitive)
        if (input.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase))
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            return true;
        }

        feedbackText.text = "Incorrect!";
        feedbackText.color = Color.red;
        return false;
    }

    // Function to show the output panel
    private void ShowOutputPanel(bool isCorrect)
    {
        outputPanel.SetActive(true);  // Activate the output panel

        // Update the output panel's content based on correctness
        TextMeshProUGUI outputText = outputPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (outputText != null)
        {
            outputText.text = isCorrect ? "Correct answer!" : "Syntax Denied. Please try again.";
        }

        // Start the coroutine to hide the panel after a delay
        StartCoroutine(HideOutputPanelAfterDelay(outputPanelDisplayTime));
    }

    // Coroutine to hide the output panel after a delay
    private IEnumerator HideOutputPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        outputPanel.SetActive(false);  // Hide the output panel
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

    // Coroutine to scale down and close the panel after a delay, resetting the cursor if the answer was correct
    private IEnumerator ClosePanelWithScale(bool isCorrect)
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

        // Reset the cursor and handle any additional behavior for correct answers
        if (isCorrect)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  // Reset cursor to default
            Destroy(laser2);  // Destroy the laser if the answer is correct
        }

        laserTrigger2.SetActive(true);  // Enable the laser trigger
    }
}
