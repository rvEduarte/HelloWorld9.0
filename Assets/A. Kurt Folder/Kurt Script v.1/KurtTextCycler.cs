using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KurtTextCycler : MonoBehaviour
{
    // List of GameObjects to cycle through
    public List<GameObject> objects;

    // Index to track the currently active object
    private int currentIndex = 0;

    // References to the buttons
    public Button nextButton;
    public Button previousButton;

    private void Start()
    {
        // Set the initial object visibility (only the first one is active)
        UpdateObjectVisibility();

        // Add listeners to buttons
        nextButton.onClick.AddListener(ShowNextObject);
        previousButton.onClick.AddListener(ShowPreviousObject);
    }

    // Function to show the next object in the list
    public void ShowNextObject()
    {
        // Hide the current object
        objects[currentIndex].SetActive(false);

        // Increment the index, loop back to the start if necessary
        currentIndex = (currentIndex + 1) % objects.Count;

        // Show the next object
        objects[currentIndex].SetActive(true);
    }

    // Function to show the previous object in the list
    public void ShowPreviousObject()
    {
        // Hide the current object
        objects[currentIndex].SetActive(false);

        // Decrement the index, loop back to the end if necessary
        currentIndex = (currentIndex - 1 + objects.Count) % objects.Count;

        // Show the previous object
        objects[currentIndex].SetActive(true);
    }

    // Ensures that only the current object is visible at the start
    private void UpdateObjectVisibility()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            // Only the object at currentIndex should be active
            objects[i].SetActive(i == currentIndex);
        }
    }
}
