using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Reference to TextMeshProUGUI component
    public float typingSpeed = 0.05f;      // Speed of the typewriting effect
    private string fullText;               // The full text to display
    private string currentText = "";       // The text currently displayed

    private void Start()
    {
        fullText = textComponent.text;     // Store the full text from TextMeshPro
        textComponent.text = "";           // Clear the text initially
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Optional: If you want to trigger the effect again on a button press
    public void RestartTypewriter()
    {
        StopAllCoroutines();
        StartCoroutine(ShowText());
    }
}
