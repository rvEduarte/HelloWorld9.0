using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_CharQuiz : MonoBehaviour
{
    public TMP_InputField[] inputFields; 
    public string[][] correctAnswers;  
    public TextMeshProUGUI[] feedbackTexts;  

    public GameObject outputPanel;  
    public CanvasGroup outputPanelCanvasGroup; 
    public GameObject errorImage;  
    public GameObject successImage;  
    public GameObject HintPanel;
    public GameObject destroyDownDialogue;

    public float scaleDuration = 0.5f;  
    public float closeDelay = 2f;  
    public float outputPanelDisplayTime = 3f;  
    public float fadeDuration = 0.5f;  
    public float successFadeDuration = 1f; 

    public GameObject laser;

    public GameObject laserTrigger;


    void Start()
    {
        correctAnswers = new string[][]
        {
            new string[] { "char" },  // for first input field
            new string[] { "'R'", "'r'" },  // for second input field
            new string[] { "missingChar" }   // for third input field
        };

        // Attach listeners for Enter key
        foreach (var inputField in inputFields)
        {
            inputField.onEndEdit.AddListener(delegate { CheckEnterKey(inputField); });
        }

        // Ensure the output panel is hidden initially
        outputPanelCanvasGroup.alpha = 0;
        outputPanel.SetActive(false);

    }

    void Update()
    {
        ShowHideScript.stopMovement = false; // The script blocks movement
    }

    // Method to detect if the Enter key was pressed
    private void CheckEnterKey(TMP_InputField inputField)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ValidateAnswers();  // Trigger answer validation when Enter is pressed
        }
    }

    public void ValidateAnswers()
    {
        // Check if the number of input fields matches the number of correct answer sets
        if (inputFields.Length != correctAnswers.Length)
        {
            Debug.LogError("Number of input fields and correct answers sets do not match!");
            return;
        }

        // Track whether all answers are correct
        bool allCorrect = true;

        // Check each input field and provide feedback
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (!CheckAnswer(inputFields[i], correctAnswers[i], feedbackTexts[i]))
                allCorrect = false;
        }

        // Show the output panel after validation with fade-in
        StartCoroutine(FadeInOutputPanel());

        // Show feedback based on correctness
        if (allCorrect)
        {
            successImage.SetActive(true);  // Show success image
            StartCoroutine(WaitAndFadeOutSuccessImage()); // Wait 1 second then fade out the success image
            StartCoroutine(ClosePanelWithScale(true));  // Pass true when all answers are correct
        }
        else
        {
            errorImage.SetActive(true);  // Show error image
            StartCoroutine(BlinkErrorAndShowHint());  // Show blinking error and hint after
        }
    }

    // Coroutine to wait for a specified duration before fading out the success image
    private IEnumerator WaitAndFadeOutSuccessImage()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        StartCoroutine(FadeOutSuccessImage()); // Fade out the success image
    }

    // Helper function to check each input field's answer (case-insensitive comparison)
    private bool CheckAnswer(TMP_InputField inputField, string[] correctAnswersSet, TextMeshProUGUI feedbackText)
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            feedbackText.text = "Answer required!";
            feedbackText.color = Color.red;
            return false;
        }

        // Check if the input matches any of the possible correct answers (case-insensitive)
        foreach (string correctAnswer in correctAnswersSet)
        {
            if (inputField.text.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase))
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

    // Coroutine to fade in the output panel
    private IEnumerator FadeInOutputPanel()
    {
        outputPanel.SetActive(true);  // Activate the output panel
        HintPanel.SetActive(false);  // Hide the hint panel

        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            outputPanelCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        outputPanelCanvasGroup.alpha = 1;  // Ensure it's fully visible
    }

    // Coroutine to fade out the output panel
    private IEnumerator FadeOutOutputPanel()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            outputPanelCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        outputPanelCanvasGroup.alpha = 0;  // Ensure it's fully invisible
        outputPanel.SetActive(false);  // Deactivate the output panel
    }

    // Coroutine to fade out the success image
    private IEnumerator FadeOutSuccessImage()
    {
        CanvasGroup successCanvasGroup = successImage.GetComponent<CanvasGroup>();
        successCanvasGroup.alpha = 1; // Ensure the image is fully visible
        float elapsedTime = 0;

        while (elapsedTime < successFadeDuration)
        {
            successCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / successFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        successCanvasGroup.alpha = 0;  // Ensure it's fully invisible
        successImage.SetActive(false); // Deactivate the success image
    }

    // Coroutine to blink the error image and show hint after
    private IEnumerator BlinkErrorAndShowHint()
    {
        // Blink the error image
        for (int i = 0; i < 3; i++)
        {
            errorImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            errorImage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        // Hide the output panel with fade-out
        StartCoroutine(FadeOutOutputPanel());

        yield return new WaitForSeconds(fadeDuration);  // Wait for fade out to finish

        // Show hint panel after output panel hides
        HintPanel.SetActive(true);
    }

    // Coroutine to scale down and close the panel after a delay, resetting the cursor if all answers were correct
    private IEnumerator ClosePanelWithScale(bool allCorrect)
    {
        // Wait for the specified delay before starting to close the panel
        yield return new WaitForSeconds(closeDelay);

        // Fade out the output panel
        StartCoroutine(FadeOutOutputPanel());

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

        // Enable player movement
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;

        // Reset the cursor to default if all answers were correct
        if (allCorrect)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            LineRenderer.Destroy(laser);

            destroyDownDialogue.SetActive(false);
        }

        laserTrigger.SetActive(true);
    }
}