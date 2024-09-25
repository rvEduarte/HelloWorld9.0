using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KurtTextCycler : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;  // Reference to the TextMeshProUGUI component
    public Button nextButton;            // Button to go to the next text
    public Button prevButton;            // Button to go to the previous text

    [TextArea(3, 10)]                    // Allows for multiline text input in the Inspector
    public string[] texts;               // Array to hold the texts you want to display

    private int currentIndex = 0;        // Keeps track of the current text index

    void Start()
    {
        // Set up button listeners
        nextButton.onClick.AddListener(ShowNextText);
        prevButton.onClick.AddListener(ShowPreviousText);

        // Show the first text if available
        if (texts.Length > 0)
        {
            textDisplay.text = texts[currentIndex];
        }
        else
        {
            textDisplay.text = "No texts available!";
            nextButton.interactable = false;
            prevButton.interactable = false;
        }

        UpdateButtonInteractability();
    }

    // Show the next text in the array
    private void ShowNextText()
    {
        if (currentIndex < texts.Length - 1)
        {
            currentIndex++;
            textDisplay.text = texts[currentIndex];
            UpdateButtonInteractability();
        }
    }

    // Show the previous text in the array
    private void ShowPreviousText()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            textDisplay.text = texts[currentIndex];
            UpdateButtonInteractability();
        }
    }

    // Update button interactability based on the current index
    private void UpdateButtonInteractability()
    {
        nextButton.interactable = currentIndex < texts.Length - 1;
        prevButton.interactable = currentIndex > 0;
    }
}
