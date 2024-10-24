using System.Collections;
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
    public GameObject quizBg;  // Reference to the quiz background
    public GameObject bridgeQuiz;  // Reference to the bridgeQuiz GameObject

    public float scaleDuration = 0.5f;  // Scaling duration for closing panel
    public float closeDelay = 2f;  // Delay before closing the panel
    public float outputPanelDisplayTime = 3f;  // Time to display output panel
    public float fadeDuration = 1f;  // Duration for fading out the bridgeQuiz and successImage

    public KurtUpwardPlatform kurtUpwardPlatform;  // Reference to platform controller

    void Start()
    {
        correctAnswers = new string[][]
        {
            new string[] { "char" },  // for first input field
            new string[] { "'E'", "'e'" },  // for second input field
            new string[] { "inputChar" }   // for third input field
        };

        // Hide all feedback texts initially
        foreach (var feedbackText in feedbackTexts)
        {
            feedbackText.gameObject.SetActive(false);
        }

        // Attach listeners for Enter key
        foreach (var inputField in inputFields)
        {
            inputField.onEndEdit.AddListener(delegate { CheckEnterKey(inputField); });
        }
    }

    void Update()
    {
        ShowHideScript.stopMovement = false; //The script block movement
    }

    private void CheckEnterKey(TMP_InputField inputField)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Enter key pressed");
            ValidateAnswers();  // Trigger answer validation when Enter is pressed
        }
    }

    public void ValidateAnswers()
    {
        if (inputFields.Length != correctAnswers.Length)
        {
            Debug.LogError("Number of input fields and correct answers sets do not match!");
            return;
        }

        bool allCorrect = true;

        for (int i = 0; i < inputFields.Length; i++)
        {
            if (!CheckAnswer(inputFields[i], correctAnswers[i], feedbackTexts[i]))
                allCorrect = false;
        }

        ShowOutputPanel(allCorrect);

        if (allCorrect)
        {
            successImage.SetActive(true);  // Show success image
            StartCoroutine(FadeOutQuizAndSuccessThenClose());
        }
        else
        {
            errorImage.SetActive(true);  // Show error image
            StartCoroutine(BlinkErrorImage());
        }
    }

    private bool CheckAnswer(TMP_InputField inputField, string[] correctAnswersSet, TextMeshProUGUI feedbackText)
    {
        feedbackText.gameObject.SetActive(true);  // Show the feedback text when checking the answer

        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            feedbackText.text = "Answer required!";
            feedbackText.color = Color.red;
            return false;
        }

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

    private void ShowOutputPanel(bool allCorrect)
    {
        outputPanel.SetActive(true);  // Activate the output panel

        TextMeshProUGUI outputText = outputPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (outputText != null)
        {
            outputText.text = allCorrect ? "All answers are correct!" : "Syntax Denied. Please try again.";
        }

        StartCoroutine(HideOutputPanelAfterDelay(outputPanelDisplayTime));
    }

    private IEnumerator HideOutputPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        outputPanel.SetActive(false);  // Hide the output panel
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

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

    private IEnumerator FadeOutQuizAndSuccessThenClose()
    {

        // Now close the panel after fading out
        yield return new WaitForSeconds(closeDelay);  // Optional delay before closing

        // Fade out the quizBg and successImage first
       // yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut(successImage));

        //yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeOut(quizBg));

        kurtUpwardPlatform.StartMoving();  // Assuming this method starts the platform movement

        Debug.Log("Player movement enabled after conversation");
        TriggerTutorial.disableMove = true; // Enable movement
        TriggerTutorial.disableJump = false; // Enable jumping

        // Scale down the panel
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float time = 0;

        while (time < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, time / scaleDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        gameObject.SetActive(false);

        Debug.Log("Panel Closed...");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError($"CanvasGroup component is missing on {obj.name}.");
            yield break;
        }

        float startAlpha = 1f;
        float time = 0;

        // Fade out logic
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            canvasGroup.alpha = alpha;
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure fully faded out
        canvasGroup.alpha = 0;
        obj.SetActive(false);  // Optionally deactivate the GameObject after fading
    }
}
