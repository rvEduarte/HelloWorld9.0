using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_QuizPanel2 : MonoBehaviour
{
    // Reference to the input fields in the UI
    public TMP_InputField[] inputFields;  // Array of input fields to handle multiple quizzes dynamically

    // Correct answers array (configured in the Inspector)
    public string[] correctAnswers;

    // Reference to the feedback texts
    public TextMeshProUGUI[] feedbackTexts;  // Array for feedback texts corresponding to input fields

    // Reference to the output panel
    public GameObject outputPanel;

    // Reference to the platform GameObject (can have different scripts in different scenes)
    public GameObject platformObject;

    // Scaling duration
    public float scaleDuration = 0.5f;

    // Delay before closing the panel
    public float closeDelay = 2f; // Set this to the desired delay duration in seconds

    // Delay before the platform starts moving up
    public float platformMoveDelay = 3f; // Set this to the desired delay duration before the platform moves

    // Reference to error and success images
    public GameObject errorImage;
    public GameObject successImage;

    // Reference to the KurtComputer script
  //  public KurtComputer computerScript;

    void Start()
    {
        // Attach listeners to input fields to check for Enter key
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

    // Function to validate the answers when Submit button is clicked
    public void ValidateAnswers()
    {
        // Check if the number of input fields matches the number of correct answers
        if (inputFields.Length != correctAnswers.Length)
        {
            Debug.LogError("Number of input fields and correct answers do not match!");
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

        // If all answers are correct, start moving the platform after a delay and close the panel
        if (allCorrect)
        {
            successImage.SetActive(true); // Show success image
            StartCoroutine(ClosePanelWithScale());

            Debug.Log("PLATFORM STARTS TO MOVE");
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

        if (platformObject != null)
        {
            var movingPlatform = platformObject.GetComponent<MonoBehaviour>(); // Find platform movement script

            if (movingPlatform != null)
            {
                var method = movingPlatform.GetType().GetMethod("StartMoving");
                if (method != null)
                {
                    method.Invoke(movingPlatform, null);
                }
                else
                {
                    Debug.LogError("The assigned platform does not have a 'StartMoving' method.");
                }
            }
            else
            {
                Debug.LogError("No platform movement script found on the platform object.");
            }
        }
        else
        {
            Debug.LogError("Platform object reference is not assigned!");
        }
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
            feedbackText.text = "Incorrect!";
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

        // Call the KurtComputer method to re-enable player movement
        // computerScript.CloseJigsawPanel();

        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }
}
