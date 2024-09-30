using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    // Reference to the output panel
    public GameObject outputPanel;

    // Reference to the moving platform script
    public MovingPlatform movingPlatform;  // Link to the MovingPlatform script

    // Scaling duration
    public float scaleDuration = 0.5f;

    // Delay before closing the panel
    public float closeDelay = 2f; // Set this to the desired delay duration in seconds

    // Delay before the platform starts moving up
    public float platformMoveDelay = 3f; // Set this to the desired delay duration before the platform moves

    // Reference to error and success images
    public GameObject errorImage;
    public GameObject successImage;

    // Reference to Kurt_GameStartLvl script
    public Kurt_GameStartLvl kurt_GameStartLvl;

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
        yield return new WaitForSeconds(platformMoveDelay);
        movingPlatform.StartMoving();
    }

    // Helper function to check each input field's answer
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
            feedbackText.text = "Incorrect Input. Try again.";
            feedbackText.color = Color.red;
            return false;
        }
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
        for (int i = 0; i < 2; i++)
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
        if (kurt_GameStartLvl != null)
        {
            TriggerTutorial.disableMove = false;
            Debug.Log("Player movement enabled");
        }

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
    }
}
