using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inputCraneConsole : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public TMP_InputField inputField1; // First input field

    private string baseText = "= ;"; // Base template text
    void Start()
    {
        // Add listeners for input fields to update text when they change
        inputField1.onValueChanged.AddListener(UpdateTextMeshFromInput1);
    }

    // Update TextMeshPro when first input field changes
    private void UpdateTextMeshFromInput1(string input)
    {
        // Rebuild the text by inserting the first input after "("
        string newText = "= " + input + ";";

        // Update TextMeshPro with the new text and retain the colored comment
        textMeshPro.text = newText;

    }
}
