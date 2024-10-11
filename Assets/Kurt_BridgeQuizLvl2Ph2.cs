using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_BridgeQuizLvl2Ph2 : MonoBehaviour
{
    public TMP_InputField[] inputFields;  // Array of input fields to handle multiple quizzes dynamically
    public string[][] correctAnswers;  // 2D array where each row contains possible correct answers for the corresponding input field
    public TextMeshProUGUI[] feedbackTexts;  // Array for feedback texts corresponding to input fields

    public GameObject outputPanel;  // Reference to the output panel
    public GameObject errorImage;  // Reference to error image
    public GameObject successImage;  // Reference to success image

    public RvComputer computerScript;  // Reference to the KurtComputer script
    public float scaleDuration = 0.5f;  // Scaling duration for closing panel
    public float closeDelay = 2f;  // Delay before closing the panel
    public float outputPanelDisplayTime = 3f;  // Time to display output panel

    void Start()
    {
        correctAnswers = new string[][]
        {
            new string[] { "char" },  // for first input field
            new string[] { "A", "a", "B", "b", "C", "c", "D", "d", "E","e", "F", "f", "G", "g",
            "H", "h", "I", "i", "J", "j", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q",
            "R", "r", "S", "s", "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z" },  // for second input field
            new string[] { "inputChar" }   // for third input field
        };

        // Attach listeners for Enter key
        foreach (var inputField in inputFields)
        {
            inputField.onEndEdit.AddListener(delegate { CheckEnterKey(inputField); });
        }
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

        // Show the output panel after validation
        ShowOutputPanel(allCorrect);

        // Show feedback based on correctness
        if (allCorrect)
        {
            successImage.SetActive(true);  // Show success image
            StartCoroutine(ClosePanelWithScale());
        }
        else
        {
            errorImage.SetActive(true);  // Show error image
            StartCoroutine(BlinkErrorImage());
        }
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

    // Function to show the output panel
    private void ShowOutputPanel(bool allCorrect)
    {
        outputPanel.SetActive(true);  // Activate the output panel

        // Update the output panel's content based on correctness
        TextMeshProUGUI outputText = outputPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (outputText != null)
        {
            outputText.text = allCorrect ? "All answers are correct!" : "Syntax Denied. Please try again.";
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

        // Trigger the computer activation here if needed
        // Uncomment this line if you want to call it right away.
        // computerScript.CloseJigsawPanel();
    }

    // Call this method to show the computer after the second conversation is closed
    public void ShowComputer()
    {
        computerScript.CloseJigsawPanel();  // Call the method to re-enable player movement
    }
}