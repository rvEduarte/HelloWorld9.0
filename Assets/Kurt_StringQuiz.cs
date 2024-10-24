using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_StringQuiz : MonoBehaviour
{
    public TMP_InputField inputField;  
    public string correctAnswer = "\"Hello World\"";  

    public GameObject errorImage;  
    public GameObject successImage;  
    public GameObject hintPanel;  
    public GameObject quizPanel; 

    public float fadeDuration = 0.5f;  
    public float closeDelay = 2f;  

    public GameObject laser2;
    public GameObject laserTrigger2;
    public GameObject removeDownDialogue;

    void Start()
    {
        // Attach listener for Enter key
        inputField.onEndEdit.AddListener(delegate { CheckEnterKey(); });
    }

    void Update()
    {
        ShowHideScript.stopMovement = false; // The script block movement
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

        // Show success or error image based on correctness
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
            return false; // Answer required
        }

        // Check if the input matches the correct answer (case-insensitive)
        return input.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase);
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

    }

    // Coroutine to scale down and close the panel after a delay, resetting the cursor if the answer was correct
    private IEnumerator ClosePanelWithScale(bool isCorrect)
    {
        // Wait for the specified delay before starting to close the panel
        yield return new WaitForSeconds(closeDelay);

        // Fade out the success image and quiz panel together
        StartCoroutine(FadeOutElementsTogether());

        // Reset the cursor and handle any additional behavior for correct answers
        if (isCorrect)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  // Reset cursor to default
            Destroy(laser2);  // Destroy the laser if the answer is correct
        }

        laserTrigger2.SetActive(true);  // Enable the laser trigger
    }

    // Function to fade out the success image and quiz panel together
    private IEnumerator FadeOutElementsTogether()
    {
        CanvasGroup successGroup = successImage.GetComponent<CanvasGroup>();
        CanvasGroup quizGroup = quizPanel.GetComponent<CanvasGroup>();
        CanvasGroup hintGroup = hintPanel.GetComponent<CanvasGroup>();

        float time = 0f;

        // Fade out all elements together
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            successGroup.alpha = alpha;
            quizGroup.alpha = alpha;  
            hintGroup.alpha = alpha;
            yield return null;
        }

        successGroup.alpha = 0f;
        quizGroup.alpha = 0f;

        successImage.SetActive(false);
        quizPanel.SetActive(false);
        hintPanel.SetActive(false);

        removeDownDialogue.SetActive(false);
    }
}
