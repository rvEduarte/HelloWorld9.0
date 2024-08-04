using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CopyToClipboard : MonoBehaviour
{
    [SerializeField] private GameObject output;
    public TextMeshProUGUI codeText; // Use TextMeshProUGUI if using TextMeshPro
    public Button copyButton;

    public float timeToDestroy = 5f; // Ensure you have a default value for timeToDestroy

    void Start()
    {
        copyButton.onClick.AddListener(CopyCodeToClipboard);
        output.SetActive(false); // Initially hide the output
    }

    void CopyCodeToClipboard()
    {
        if (codeText != null)
        {
            // Copy text to clipboard
            GUIUtility.systemCopyBuffer = codeText.text;
            Debug.Log("Code copied to clipboard: " + codeText.text);
            output.SetActive(true);

            // Start the coroutine to destroy the game object after a delay
            StartCoroutine(DestroyAfterDelay());
        }
        else
        {
            Debug.LogError("Code text is not assigned.");
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(timeToDestroy);

        // Destroy the game object
        Destroy(gameObject);
    }
}
