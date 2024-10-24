using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_OpeningPortalQuiz : MonoBehaviour
{
    public TMP_InputField inputField;    // TextMesh Pro Input Field for the player to type the code
    public TextMeshProUGUI messageText;  // Text Mesh Pro for displaying messages
    public GameObject consolePanel;      // The panel that contains the input field
    public List<GameObject> portals;     // List of portal objects to activate

    public float portalActivationDelay = 2f; // Delay before activating portals
    public float panelCloseDelay = 1f;       // Delay before closing the panel

    public GameObject dialoguePortalOpen;
    public GameObject outputPanel;  // Reference to the output panel
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
        if (outputPanel != null) outputPanel.SetActive(false); // Ensure output panel is initially inactive
        if (hintPanel != null) hintPanel.SetActive(true); // Ensure hint panel is active
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
                messageText.text = "<color=#0CCB2A>Correct code!</color> Closing console...";
                outputPanel.SetActive(true); // Activate output panel for correct response
                Debug.Log("Correct code entered."); // Log for debugging

                if (successImage != null)
                {
                    successImage.SetActive(true); // Show success image
                }

                // Remove or comment this line to keep the hint panel visible
                // if (hintPanel != null)
                // {
                //     hintPanel.SetActive(false); // Deactivate hint panel
                // }

                StartCoroutine(ClosePanelAndActivatePortals());
                return; // Exit the loop early if a correct code is found
            }
        }

        // If the code is incorrect
        if (errorImage != null)
        {
            errorImage.SetActive(true); // Show error image 
        }

        messageText.text = "<color=#FF0000>Wrong code. Try again!</color>";  // Red text for wrong code message
        outputPanel.SetActive(true); // Activate output panel for incorrect response
        StartCoroutine(BlinkErrorImage()); // Start blinking error image
        StartCoroutine(HideOutputPanelAfterDelay()); // Hide output panel after blinking

        if (hintPanel != null)
        {
            hintPanel.SetActive(true); // Show the hint panel again
        }
    }

    // Coroutine to hide the output panel after blinking error image
    private IEnumerator HideOutputPanelAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // Wait for the duration of the blinking
        outputPanel.SetActive(false); // Hide the output panel
    }

    // Coroutine to close the panel and activate portals after a delay
    IEnumerator ClosePanelAndActivatePortals()
    {
        // Wait for a moment to show the "Correct code!" message
        yield return new WaitForSeconds(panelCloseDelay);

        yield return new WaitForSeconds(1f);
        dialoguePortalOpen.SetActive(true);

        // Close the console panel
        consolePanel.SetActive(false);
        successImage.SetActive(false);

        // Reset the cursor to the default when the panel is closed
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        // Wait for the specified delay before activating the portals
        yield return new WaitForSeconds(portalActivationDelay);

        // Activate all portals and their controllers
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
