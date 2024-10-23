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
    public GameObject hintPanel;  // Reference to hint panel

    public float fadeDuration = 0.5f;  // Duration for fading in and out
    public float closeDelay = 2f;  // Delay before closing the panel
    public float outputPanelDisplayTime = 3f;  // Time to display output panel

    public GameObject laser2;
    public GameObject laserTrigger2;

    void Start()
    {
        // Attach listener for Enter key
        inputField.onEndEdit.AddListener(delegate { CheckEnterKey(); });
    }

    void Update()
    {
        ShowHideScript.stopMovement = false; //The script block movement
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

        // Hide hint panel when validating
        hintPanel.SetActive(false);

        // Show feedback and update UI based on correctness
        StartCoroutine(FadeInOutputPanel(isCorrect));

        if (isCorrect)
        {
            successImage.SetActive(true);  // Show success image
            StartCoroutine(ClosePanelWithScale(true));  // Close the panel with success behavior
        }
        else
        {
            errorImage.SetActive(true);  // Show error image
            StartCoroutine(BlinkErrorAndShowHint());  // Blink error and show hint after closing
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

    // Function to fade in the output panel
    private IEnumerator FadeInOutputPanel(bool isCorrect)
    {
        CanvasGroup canvasGroup = outputPanel.GetComponent<CanvasGroup>();
        outputPanel.SetActive(true);

        // Fade in
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;

        // Show feedback based on correctness
        TextMeshProUGUI outputText = outputPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (outputText != null)
        {
            outputText.text = isCorrect ? "Correct answer!" : "Syntax Denied. Please try again.";
        }

        yield return new WaitForSeconds(outputPanelDisplayTime);

        // If incorrect, do not close the panel immediately; wait for blink and hint
        if (!isCorrect)
        {
            yield break;
        }

        StartCoroutine(FadeOutOutputPanel());
    }

    // Function to fade out the output panel
    private IEnumerator FadeOutOutputPanel()
    {
        CanvasGroup canvasGroup = outputPanel.GetComponent<CanvasGroup>();
        float time = 0f;

        // Fade out
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        outputPanel.SetActive(false);
    }

    // Coroutine to blink the error image and show the hint panel after it finishes
    private IEnumerator BlinkErrorAndShowHint()
    {
        // Blink error image
        for (int i = 0; i < 3; i++)
        {
            errorImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            errorImage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        // Fade out output panel after blinking
        yield return StartCoroutine(FadeOutOutputPanel());

        // Show hint panel
        hintPanel.SetActive(true);
    }

    // Coroutine to scale down and close the panel after a delay, resetting the cursor if the answer was correct
    private IEnumerator ClosePanelWithScale(bool isCorrect)
    {
        // Wait for the specified delay before starting to close the panel
        yield return new WaitForSeconds(closeDelay);

        // Fade out output panel
        yield return StartCoroutine(FadeOutOutputPanel());

        // Reset the cursor and handle any additional behavior for correct answers
        if (isCorrect)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  // Reset cursor to default
            Destroy(laser2);  // Destroy the laser if the answer is correct
        }

        laserTrigger2.SetActive(true);  // Enable the laser trigger
    }
}
