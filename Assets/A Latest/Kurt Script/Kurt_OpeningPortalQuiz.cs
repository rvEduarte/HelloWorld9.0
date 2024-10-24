using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Include UI namespace for Image

public class Kurt_OpeningPortalQuiz : MonoBehaviour
{
    public TMP_InputField inputField;    // TextMesh Pro Input Field for the player to type the code
    public GameObject consolePanel;      // The panel that contains the input field
    public List<GameObject> portals;     // List of portal objects to activate

    public float panelCloseDelay = 1f;       // Delay before closing the panel

    public GameObject dialoguePortalOpen;
    public GameObject hintPanel;    // Reference to the hint panel

    // Correct codes for the console
    private string[] correctCodes = {
        "bool isPortalOpen = true;",
        "bool isPortalOpen=true;",
        "bool isPortalOpen= true;",
        "bool isPortalOpen =true;"
    };

    // Reference to error and success images
    public GameObject errorImage;
    public GameObject successImage;

    private bool panelClosed = false; // Flag to check if the panel is closed

    // Reference to the panel's image for fading effect
    public Image panelImage; // Add this for fading effect

    void Start()
    {
        // Clear the input field and set the initial message    
        inputField.text = "";

        // Ensure all portals are initially deactivated
        foreach (GameObject portal in portals)
        {
            portal.SetActive(false);
        }

        // Ensure images and panels are initially inactive
        if (successImage != null) successImage.SetActive(false);
        if (errorImage != null) errorImage.SetActive(false);
        if (hintPanel != null) hintPanel.SetActive(true); // Ensure hint panel is active

        // Initialize the panel's image (set the alpha to 1 for fully visible)
        if (panelImage != null)
        {
            Color tempColor = panelImage.color;
            tempColor.a = 1f; // Fully visible
            panelImage.color = tempColor;
        }
    }

    void Update()
    {
        // Check if the Enter key is pressed and the panel is not closed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!panelClosed) // Only check code if the panel is open
            {
                CheckCode(); // Trigger code checking when Enter key is pressed
            }
        }
    }

    public void CheckCode()
    {
        // Get the player's input from the TMP InputField, trimmed of extra spaces and newlines
        string playerInput = inputField.text.Trim();
        Debug.Log($"Player Input: {playerInput}"); // Log player input for debugging

        // Check if the input matches any of the correct codes
        foreach (var code in correctCodes)
        {
            if (playerInput == code)
            {
                // Correct code entered
                Debug.Log("Correct code entered."); // Log for debugging

                if (successImage != null)
                {
                    successImage.SetActive(true); // Show success image
                }

                StartCoroutine(ClosePanelAndActivatePortals());
                return; // Exit the loop early if a correct code is found
            }
        }

        // If the code is incorrect
        if (errorImage != null)
        {
            errorImage.SetActive(true); // Show error image 
        }

        StartCoroutine(BlinkErrorImage()); // Start blinking error image

        if (hintPanel != null)
        {
            hintPanel.SetActive(true); // Show the hint panel again
        }
    }

    // Coroutine to close the panel and activate portals after a delay
    IEnumerator ClosePanelAndActivatePortals()
    {
        // Wait for a moment to show the "Correct code!" message
        yield return new WaitForSeconds(panelCloseDelay);

        // Fade out the panel before closing
        yield return StartCoroutine(FadeOutPanel());

        // Wait for 1 second before opening the dialogue
        yield return new WaitForSeconds(1f);
        dialoguePortalOpen.SetActive(true);

        // Close the console panel
        consolePanel.SetActive(false);
        successImage.SetActive(false);

        // Reset the cursor to the default when the panel is closed
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        // Activate all portals and their controllers immediately
        foreach (GameObject portal in portals)
        {
            portal.SetActive(true); // Activate each portal
        }

        // Set the flag to indicate that the panel has been closed
        panelClosed = true;

        // Wait until the conversation is finished before enabling movement
        while (BlockDialogue.isActive)
        {
            yield return null;  // Wait until the conversation is over
        }

        Debug.Log("Player movement enabled after conversation");
        TriggerTutorial.disableMove = true; // Enable movement
        TriggerTutorial.disableJump = false; // Enable jumping
    }

    private IEnumerator FadeOutPanel()
    {
        if (panelImage != null)
        {
            // Gradually decrease the alpha value to create a fade-out effect
            for (float i = 1; i >= 0; i -= Time.deltaTime / 1f) // Change 1f to adjust the fade duration
            {
                Color tempColor = panelImage.color;
                tempColor.a = i;
                panelImage.color = tempColor;
                yield return null; // Wait until the next frame
            }
            // Ensure the panel is fully transparent after fading out
            Color finalColor = panelImage.color;
            finalColor.a = 0;
            panelImage.color = finalColor;
        }
    }

    private IEnumerator BlinkErrorImage()
    {
        for (int i = 0; i < 3; i++)
        {
            if (errorImage != null)
            {
                errorImage.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                errorImage.SetActive(false);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
