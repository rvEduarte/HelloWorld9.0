using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_TypeWriter : MonoBehaviour
{
    public TextMeshPro textMeshPro;  // Reference to the TextMeshPro component in 3D
    public float typingSpeed = 0.05f; // Delay between each character

    private string fullText;          // The full text to display
    private Coroutine typingCoroutine;

    private void Start()
    {
        // Store the full text and clear the initial text
        fullText = textMeshPro.text;
        textMeshPro.text = "";

        // Start the typewriter effect
        StartTypewriterEffect();
    }

    // Public method to start the typewriter effect
    public void StartTypewriterEffect()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText());
    }

    // Coroutine to display the text letter by letter
    private IEnumerator TypeText()
    {
        textMeshPro.text = ""; // Clear the text
        foreach (char letter in fullText.ToCharArray())
        {
            textMeshPro.text += letter;  // Add one letter at a time
            yield return new WaitForSeconds(typingSpeed);  // Wait for the specified delay
        }
    }
}
