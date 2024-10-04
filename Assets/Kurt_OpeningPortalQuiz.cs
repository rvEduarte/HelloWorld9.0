using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kurt_OpeningPortalQuiz : MonoBehaviour
{
    public TMP_InputField inputField;    // TextMesh Pro Input Field for the player to type the code
    public TextMeshProUGUI messageText;  // TextMesh Pro for displaying messages
    public GameObject consolePanel;      // The panel that contains the input field
    public List<GameObject> portals;     // List of portal objects to activate
    public List<KurtPortalController> portalControllers; // List of portal controller scripts
    public KurtComputer computerScript;  // Reference to the KurtComputer script to enable player movement

    public float portalActivationDelay = 2f; // Delay before activating portals
    public float panelCloseDelay = 1f;       // Delay before closing the panel

    private string correctCode = "1234";     // The correct code for the console

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
    }

    public void CheckCode()
    {
        // Get the player's input from the TMP InputField
        string playerInput = inputField.text;

        // Check if the input matches the correct code
        if (playerInput == correctCode)
        {
            messageText.text = "Correct code! Closing console...";
            StartCoroutine(ClosePanelAndActivatePortals());
        }
        else
        {
            messageText.text = "Wrong code. Try again!";
        }

        // Clear the input field after checking
        inputField.text = "";
    }

    // Coroutine to close the panel and activate portals after a delay
    IEnumerator ClosePanelAndActivatePortals()
    {
        // Wait for a moment to show the "Correct code!" message
        yield return new WaitForSeconds(panelCloseDelay);

        // Close the console panel
        consolePanel.SetActive(false);

        // Re-enable player movement using the KurtComputer script
        if (computerScript != null)
        {
            computerScript.CloseJigsawPanel();
        }

        // Wait for the specified delay before activating the portals
        yield return new WaitForSeconds(portalActivationDelay);

        // Activate all portals and their controllers
        for (int i = 0; i < portals.Count; i++)
        {
            portals[i].SetActive(true); // Activate each portal
            if (portalControllers[i] != null)
            {
                portalControllers[i].enabled = true; // Enable each portal controller
            }
        }
    }
}
