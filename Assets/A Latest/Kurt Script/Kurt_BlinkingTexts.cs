using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_BlinkingTexts : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI component
    public GameObject panel;            // Reference to the panel to close
    public float blinkDuration = 5f;    // Total duration to blink
    public float blinkInterval = 0.5f;  // Time between each blink (on/off)

    private void Start()
    {
      
    }

    public void UpdatePanel()
    {
        if (messageText != null && panel != null)
        {
            StartCoroutine(BlinkMessageAndClosePanel());
        }
    }

    IEnumerator BlinkMessageAndClosePanel()
    {
        float endTime = Time.time + blinkDuration;
        while (Time.time < endTime)
        {
            messageText.enabled = !messageText.enabled; // Toggle the message on/off
            yield return new WaitForSeconds(blinkInterval); // Wait for the interval time
        }

        messageText.enabled = true; // Ensure the message is visible after blinking
        ClosePanel(); // Close the panel
    }

    private void ClosePanel()
    {
        panel.SetActive(false); // Disable the panel GameObject
    }
}
