using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Kurt_MultiTabInput : MonoBehaviour
{
    public List<TMP_InputField> inputFields; // List of TMP_InputFields to tab through

    private int currentIndex = 0;

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
}
