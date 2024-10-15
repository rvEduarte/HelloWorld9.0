using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Kurt_MultiTabInput : MonoBehaviour
{
    public List<TMP_InputField> inputFields; // List of TMP_InputFields to tab through

    private int currentIndex = 0;

    void Start()
    {
        // Add listeners for input fields to track when they are clicked or selected
        for (int i = 0; i < inputFields.Count; i++)
        {
            int index = i; // Capture index for the closure
            inputFields[i].onSelect.AddListener((string text) => UpdateCurrentIndex(index));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputFields.Count == 0)
                return;

            // Prevent adding multiple listeners if there's already one active
            if (inputFields[currentIndex].isFocused)
            {
                currentIndex = (currentIndex + 1) % inputFields.Count;

                // Set the next input field to be focused
                EventSystem.current.SetSelectedGameObject(inputFields[currentIndex].gameObject);
                inputFields[currentIndex].ActivateInputField();
            }
        }
    }

    // Update the current index when an input field is clicked or selected
    private void UpdateCurrentIndex(int index)
    {
        currentIndex = index;
    }
}
