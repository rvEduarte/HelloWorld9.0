using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KurtTextCycler : MonoBehaviour
{
    public List<GameObject> textDisplays;  // List of GameObjects containing TextMeshProUGUI components
    public Button nextButton;              // Button to go to the next text
    public Button prevButton;              // Button to go to the previous text

    [TextArea(3, 10)]                      // Allows for multiline text input in the Inspector
    public string[] texts;                 // Array to hold the texts you want to display

    public GameObject nextSection;         // The next section (GameObject) to show when no more texts are left
    public GameObject currentSection;      // The current section (GameObject) to hide when no more texts are left

    private int currentIndex = 0;          // Keeps track of the current text index

    void Start()
    {
        // Set up button listeners
        nextButton.onClick.AddListener(ShowNextText);
        prevButton.onClick.AddListener(ShowPreviousText);

        // Hide all text displays initially
        foreach (GameObject textDisplay in textDisplays)
        {
            textDisplay.SetActive(false);
        }

        // Show the first text if available
        if (texts.Length > 0 && textDisplays.Count > 0)
        {
            textDisplays[currentIndex].SetActive(true); // Set the first text display to active
            textDisplays[currentIndex].GetComponent<TextMeshProUGUI>().text = texts[currentIndex];
        }
        else
        {
            Debug.LogWarning("No texts or text displays available!");
            prevButton.interactable = false;
        }

        UpdateButtonInteractability();
    }

    // Show the next text in a new text display or transition to the next section
    private void ShowNextText()
    {
        if (currentIndex < texts.Length - 1 && currentIndex < textDisplays.Count - 1)
        {
            currentIndex++;
            textDisplays[currentIndex].SetActive(true);  // Set the next text display to active
            textDisplays[currentIndex].GetComponent<TextMeshProUGUI>().text = texts[currentIndex]; // Assign the text
        }
        else
        {
            TransitionToNextSection();  // When the last text is reached, switch to the next section
        }

        UpdateButtonInteractability();
    }

    // Show the previous text by reactivating the previous display
    private void ShowPreviousText()
    {
        if (currentIndex > 0)
        {
            textDisplays[currentIndex].SetActive(false); // Hide the current text display
            currentIndex--; // Move the index back

            // Ensure the previous text display is visible
            textDisplays[currentIndex].SetActive(true);
            textDisplays[currentIndex].GetComponent<TextMeshProUGUI>().text = texts[currentIndex];
        }
        else
        {
            TransitionToPreviousSection();  // Move back to the previous section if needed
        }

        UpdateButtonInteractability();
    }

    // Update button interactability based on the current index
    private void UpdateButtonInteractability()
    {
        nextButton.interactable = true;  // No limiter for "Next" button now
        prevButton.interactable = currentIndex > 0 || currentSection != null;  // Allow going back if not at the first section
    }

    // Function to transition to the next section and hide the current one
    private void TransitionToNextSection()
    {
        if (currentSection != null)
        {
            currentSection.SetActive(false);  // Hide the current section
        }

        if (nextSection != null)
        {
            nextSection.SetActive(true);      // Show the next section
        }
    }

    // Function to transition to the previous section and hide the current one
    private void TransitionToPreviousSection()
    {
        if (nextSection != null)
        {
            nextSection.SetActive(false);     // Hide the current section (next one)
        }

        if (currentSection != null)
        {
            currentSection.SetActive(true);   // Show the previous section (current one)
        }
    }
}
