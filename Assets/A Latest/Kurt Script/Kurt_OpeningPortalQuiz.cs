using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_OpeningPortalQuiz : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public TMP_InputField inputField;    // TextMesh Pro Input Field for the player to type the code
    public TextMeshProUGUI messageText;  // Text Mesh Pro for displaying messages
    public GameObject consolePanel;      // The panel that contains the input field
    public List<GameObject> portals;     // List of portal objects to activate
    public RvComputer rvComputerScript;  // Reference to the KurtComputer script to enable player movement

    public float portalActivationDelay = 2f; // Delay before activating portals
    public float panelCloseDelay = 1f;       // Delay before closing the panel

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

    void Start()
    {
        // Clear the input field and set the initial message    
        inputField.text = "";
        messageText.text = "Enter the code:";

        // Ensure all portals are initially deactivated
        foreach (GameObject portal in portals)
        {
            portal.SetActive(false);
        }

        // Ensure images are initially inactive
        if (successImage != null) successImage.SetActive(false);
        if (errorImage != null) errorImage.SetActive(false);
    }

    public void CheckCode()
    {
        // Get the player's input from the TMP InputField, trimmed of extra spaces and newlines
        string playerInput = inputField.text.Trim();

        // Check if the input matches any of the correct codes
        foreach (var code in correctCodes)
        {
            if (playerInput == code)
            {
                messageText.text = "Correct code! Closing console...";

                if (successImage != null)
                {
                    successImage.SetActive(true); // Show success image
                }

                StartCoroutine(ClosePanelAndActivatePortals());
                return; // Exit the loop early if a correct code is found
            }
        }

        messageText.text = "<color=#FF0000>Wrong code. Try again!</color>";  // Red text for wrong code message

        if (errorImage != null)
        {
            errorImage.SetActive(true); // Show error image
        }

        StartCoroutine(BlinkErrorImage());

        // Clear the input field after checking (if desired, comment this line if not)
        // inputField.text = "";
    }

    // Coroutine to close the panel and activate portals after a delay
    IEnumerator ClosePanelAndActivatePortals()
    {
        // Wait for a moment to show the "Correct code!" message
        yield return new WaitForSeconds(panelCloseDelay);

        // Close the console panel
        consolePanel.SetActive(false);

        vCam.Priority = 11;

        // Wait for the specified delay before activating the portals
        yield return new WaitForSeconds(portalActivationDelay);

        // Activate all portals and their controllers
        foreach (GameObject portal in portals)
        {
            portal.SetActive(true); // Activate each portal
        }

        yield return new WaitForSeconds(3f);
        vCam.Priority = 0;
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