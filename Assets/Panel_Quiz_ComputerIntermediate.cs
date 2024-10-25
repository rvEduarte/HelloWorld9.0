using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_Quiz_ComputerIntermediate : MonoBehaviour
{
    public SpriteRenderer spriteRenderer, spriteRenderer1;
    public float fadeDuration = 2.0f;

    public BoxCollider2D computer;
    public TMP_InputField[] inputFields;     
    public TextMeshProUGUI[] feedbackTexts; 
    public GameObject outputPanel;
    public float scaleDuration = 0.5f;
    public float closeDelay = 2f; 
    public GameObject errorImage;
    public GameObject successImage;
    public GameObject light1, light2, light3, platform, hint;

    [SerializeField] private float upValue;   // 60.77f
    [SerializeField] private float time;      // 4s

    // Hardcoded correct answers
    private string[] correctAnswers = { "bool" };  // Answer for the first input field
    private string[] booleanAnswers = { "true", "false" };  // Valid answers for the second input field
    private string[] correctAnswersForThirdInput = { "Write", "WriteLine" };

    private void Start()
    {
        light2.SetActive(false);
        light3.SetActive(false);
        light1.SetActive(true);
    }

    void Update()
    {
        // Check if Enter key is pressed and call ValidateAnswers
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) && Quiz_ComputerIntermediate.enterAllowed)
        {       
            ValidateAnswers();
        }      
    }
    public void ValidateAnswers()
    {
        // Check if the number of input fields matches the number of correct answers
        if (inputFields.Length != correctAnswers.Length + 2) // Add 2 for boolean and third input
        {
            Debug.LogError("Number of input fields and correct answers do not match!");
            return;
        }

        // Track whether all answers are correct
        bool allCorrect = true;
        bool firstCorrect = true; // Track correctness of the first two inputs

        // Check each input field and provide feedback
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (i == 0) // For the first input field (index 0)
            {
                if (!CheckAnswer(inputFields[i], correctAnswers[i], feedbackTexts[i]))
                {
                    firstCorrect = false; // Mark as incorrect if the first answer is wrong
                    allCorrect = false; // Overall correctness is also false
                }
            }
            else if (i == 1) // For the second input field (boolean input at index 1)
            {
                string answer = inputFields[i].text.ToLower(); // Convert input to lowercase to handle case insensitivity

                if (!System.Array.Exists(booleanAnswers, element => element == answer))
                {
                    feedbackTexts[i].text = "Incorrect! Please enter 'true' or 'false'.";
                    feedbackTexts[i].color = Color.red;
                    firstCorrect = false;
                    allCorrect = false;

                }
                else
                {
                    if (answer == "true")
                    {
                        Debug.Log("TRUE");

                    }
                    else if (answer == "false")
                    {
                        Debug.Log("FALSE");


                    }

                    feedbackTexts[i].text = "Correct!";
                    feedbackTexts[i].color = Color.green;
                }
            }
            else if (i == 2) // For the third input field (index 2)
            {
                string answer = inputFields[i].text;  // Check input without changing case

                if (firstCorrect && System.Array.Exists(correctAnswersForThirdInput, element => element == answer))
                {
                    feedbackTexts[i].text = "Correct!";
                    feedbackTexts[i].color = Color.green;
                }
                else
                {
                    feedbackTexts[i].text = firstCorrect ? "Incorrect!" : "You need to answer the first two questions correctly first!";
                    feedbackTexts[i].color = firstCorrect ? Color.red : Color.yellow; // Yellow if first two are incorrect
                    allCorrect = false;  // Mark as incorrect
                }
            }
        }
        // Show the output panel after validation
        ShowOutputPanel(allCorrect);

        // If all answers are correct (including the special case), start closing the panel
        if (allCorrect)
        {
            successImage.SetActive(true); // Show success image
            StartCoroutine(ClosePanelWithScale());
        }
        else
        {
            errorImage.SetActive(true); // Show error image
            StartCoroutine(BlinkErrorImage());
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

        if (inputField.text == correctAnswer) // Check directly without changing case
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
        successImage.SetActive(false);
        Quiz_ComputerIntermediate.disableE_Intermediate1 = false;

        Debug.Log("Panel Closed...");

        TriggerTutorial.disableMove = true; // Enable movement
        TriggerTutorial.disableJump = false; // Enable jumping

        // Reset the cursor to the default when the panel is closed
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        if (inputFields[1].text.Equals("true"))
        {
            Debug.Log("TRUE");
            light2.SetActive(true);
            light3.SetActive(false);
            light1.SetActive(false);
        }
        else if (inputFields[1].text.Equals("false"))
        {
            computer.enabled = false;
            hint.SetActive(false);
            Debug.Log("FALSE");
            StartCoroutine(Delay());
        }

        foreach (var name in feedbackTexts)
        {
            name.text = "New Text";     // Set the text to "New Text"
            name.color = Color.white;   // Set the color to white
        }

        foreach (var name in inputFields)
        {
            name.text = null;
            name.enabled = false;
        }
    }

    private IEnumerator Delay()
    {
        light3.SetActive(true);
        light2.SetActive(false);
        light1.SetActive(false);

        yield return new WaitForSeconds(2);
        LeanTween.moveLocalY(platform, upValue, time);
        LeanTween.alpha(spriteRenderer.gameObject, 0f, fadeDuration).setEase(LeanTweenType.linear);
        LeanTween.alpha(spriteRenderer1.gameObject, 0f, fadeDuration).setEase(LeanTweenType.linear);

    }
}