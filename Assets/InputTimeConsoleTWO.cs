using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputTimeConsoleTWO : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public TMP_InputField inputField1; // First input field
    public TMP_InputField inputField2; // Second input field

    private string baseText = "( <= )"; // Base template text
    private string coloredComment = "  <color=#d48326> //else if statement only run when the first condition is false</color>";

    private void Start()
    {
        // Set the default text for TextMeshPro
        textMeshPro.text = baseText + coloredComment;

        // Add listeners for input fields to update text when they change
        inputField1.onValueChanged.AddListener(UpdateTextMeshFromInput1);
        inputField2.onValueChanged.AddListener(UpdateTextMeshFromInput2);
    }

    // Update TextMeshPro when first input field changes
    private void UpdateTextMeshFromInput1(string input)
    {
        // Rebuild the text by inserting the first input after "("
        string newText = "(" + input + " <= )";

        // Update TextMeshPro with the new text and retain the colored comment
        textMeshPro.text = newText + coloredComment;

        // Call UpdateTextMeshFromInput2 to ensure the second input stays consistent
        UpdateTextMeshFromInput2(inputField2.text);
    }

    // Update TextMeshPro when second input field changes
    private void UpdateTextMeshFromInput2(string input)
    {
        // First, keep the text updated from inputField1
        string currentText = textMeshPro.text;

        // Find the index of ">" and update text after ">"
        int indexOfGreaterThan = currentText.IndexOf("<=") + 2;

        // Rebuild the text by adding the second input after ">"
        string newText = currentText.Substring(0, indexOfGreaterThan) +" "+ input + ")";

        // Update TextMeshPro with the new text and retain the colored comment
        textMeshPro.text = newText + coloredComment;
        Debug.Log(newText);
    }
}
