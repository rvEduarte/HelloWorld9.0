using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CopyToClipboard : MonoBehaviour
{
    [SerializeField] private GameObject output; //text
    public TextMeshProUGUI codeText; // Use TextMeshProUGUI if using TextMeshPro
    public Button copyButton;

    public Image bg;
    public Image unCopyImage;
    public Image border;
    public Image copyImage;

    public bool copyState; //false

    void Start()
    {
        // Load the saved state
        if (PlayerPrefs.GetInt("copyState") == 1)
        {
            copyState = true;
            Debug.Log("TRUE");

            //disable objects
            bg.gameObject.SetActive(false);
            output.SetActive(false);
            codeText.gameObject.SetActive(false);
            copyButton.gameObject.SetActive(false);
            unCopyImage.gameObject.SetActive(false);
            border.gameObject.SetActive(false);
            copyImage.gameObject.SetActive(false);
        }
        else
        {
            codeText.text = PlayerPrefs.GetInt("PlayerSaveDataFileID").ToString();


            copyState = false;
            Debug.Log("FALSE");  
            copyButton.onClick.AddListener(CopyCodeToClipboard);

            // Initially hide objects
            output.SetActive(false);
            border.gameObject.SetActive(false);
            copyImage.gameObject.SetActive(false);
        }
    }

    void CopyCodeToClipboard()
    {
        if (codeText != null)
        {
            if (!copyState)
            {
                // Mark the action as performed and save the state
                PlayerPrefs.SetInt("copyState", 1); // 1 means true
                PlayerPrefs.Save(); // Make sure changes are saved to disk

                copyState = true;
            }
            // Copy text to clipboard
            GUIUtility.systemCopyBuffer = codeText.text;
            Debug.Log("Code copied to clipboard: " + codeText.text);
            output.SetActive(true);

            unCopyImage.gameObject.SetActive(false);
            border.gameObject.SetActive(true);
            copyImage.gameObject.SetActive(true);
            

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
        yield return new WaitForSeconds(5f);

        // Destroy the game object
        Destroy(gameObject);
    }
}
