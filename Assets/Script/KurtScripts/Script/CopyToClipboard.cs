using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CopyToClipboard : MonoBehaviour
{
    public Text codeText; // Use TextMeshProUGUI if using TextMeshPro
    public Button copyButton;

    void Start()
    {
        copyButton.onClick.AddListener(CopyCodeToClipboard);
    }

    
    void CopyCodeToClipboard()
    {
        if (codeText != null)
        {
            // Copy text to clipboard
            GUIUtility.systemCopyBuffer = codeText.text;
            Debug.Log("Code copied to clipboard: " + codeText.text);
        }
        else
        {
            Debug.LogError("Code text is not assigned.");
        }

    }
}
