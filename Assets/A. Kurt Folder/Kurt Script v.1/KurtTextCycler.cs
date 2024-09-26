using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KurtTextCycler : MonoBehaviour
{
    public List<GameObject> textDisplays;   // List of GameObjects that include both text and icons
    public Button nextButton;               // Button to go to the next text
    public Button prevButton;               // Button to go to the previous text

    private int currentIndex = 0;           // Keeps track of the current text index

    void Start()
    {
        // Ensure that buttons and text displays are properly assigned
        if (nextButton == null || prevButton == null || textDisplays == null || textDisplays.Count == 0)
        {
            Debug.LogError("Please assign all required references in the Inspector.");
            return;
        }

        // Set up button listeners
        nextButton.onClick.AddListener(ShowNextText);
        prevButton.onClick.AddListener(ShowPreviousText);

        // Hide all text displays initially except the first one
        for (int i = 0; i < textDisplays.Count; i++)
        {
            textDisplays[i].SetActive(i == currentIndex);  // Only activate the first text display
        }

        // Update button interactivity
        UpdateButtonInteractability();
    }

    // Show the next text without hiding the previous ones
    private void ShowNextText()
    {
        if (currentIndex < textDisplays.Count - 1)
        {
            currentIndex++;
            textDisplays[currentIndex].SetActive(true);  // Show the next text display
        }

        UpdateButtonInteractability();
    }

    // Show the previous text (doesn't hide any other text)
    private void ShowPreviousText()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            textDisplays[currentIndex].SetActive(true);  // Ensure the previous text display is visible
        }

        UpdateButtonInteractability();
    }

    // Update button interactability based on the current index
    private void UpdateButtonInteractability()
    {
        nextButton.interactable = currentIndex < textDisplays.Count - 1;  // Disable next if at the last display
        prevButton.interactable = currentIndex > 0;  // Disable previous if at the first display
    }
}
