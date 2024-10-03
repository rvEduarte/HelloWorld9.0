using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kurt_NextPreviousButtonHandler : MonoBehaviour
{
    // List of GameObjects to toggle between
    public List<GameObject> objects;

    // Index to keep track of the current active object
    private int currentIndex = 0;

    // Reference to the Next and Previous buttons
    public Button nextButton;
    public Button previousButton;

    void Start()
    {
        // Attach button listeners
        nextButton.onClick.AddListener(NextObject);
        previousButton.onClick.AddListener(PreviousObject);

        // Initialize objects: make sure only the first one is active
        UpdateActiveObject();

        // Update button visibility based on the initial state
        UpdateButtonVisibility();
    }

    // Function to activate the next object in the list and deactivate the previous one
    public void NextObject()
    {
        if (objects.Count == 0) return;

        // Deactivate the current object
        objects[currentIndex].SetActive(false);

        // Increment the current index
        currentIndex++;

        // Activate the new current object
        objects[currentIndex].SetActive(true);

        // Update button visibility after the change
        UpdateButtonVisibility();
    }

    // Function to activate the previous object in the list and deactivate the current one
    public void PreviousObject()
    {
        if (objects.Count == 0) return;

        // Deactivate the current object
        objects[currentIndex].SetActive(false);

        // Decrement the current index
        currentIndex--;

        // Activate the new current object
        objects[currentIndex].SetActive(true);

        // Update button visibility after the change
        UpdateButtonVisibility();
    }

    // Update the currently active object (only used at the start)
    private void UpdateActiveObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
            {
                objects[i].SetActive(i == currentIndex); // Activate only the current index
            }
        }
    }

    // Update the visibility of the Next and Previous buttons
    private void UpdateButtonVisibility()
    {
        // Hide the Previous button if we're at the first object, otherwise show it
        if (currentIndex <= 0)
        {
            previousButton.gameObject.SetActive(false);
        }
        else
        {
            previousButton.gameObject.SetActive(true);
        }

        // Hide the Next button if we're at the last object, otherwise show it
        if (currentIndex >= objects.Count - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
        }
    }
}
