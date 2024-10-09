using Cinemachine;
using System.Collections;
using UnityEngine;

public class K_NPCLvl2Ph2 : MonoBehaviour
{
    public K_DialogueTriggerLvl2Ph2 trigger;

    public GameObject computer;  // Reference to the computer object
    private bool hasTriggeredFirstConversation = false;  // Flag to ensure one-time first conversation
    private bool hasTriggeredSecondConversation = false; // Flag to ensure one-time second conversation
    private bool hasZoomed = false;  // Flag to ensure the camera zooms only once

    public GameObject triggerPortalConvo;

    [Header("Camera Target")]
    public CinemachineVirtualCamera vCam; // Reference to the virtual camera

    public GameObject textMeshPro3D; // Reference to the 3D TextMeshPro object

    public void Start()
    {
        computer.SetActive(false);
        triggerPortalConvo.SetActive(false);
        textMeshPro3D.SetActive(false); // Ensure the TextMeshPro object is initially inactive
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters and if the first conversation hasn't been triggered
        if (collision.gameObject.CompareTag("Player") && !hasTriggeredFirstConversation)
        {
            // Disable player movement for the first conversation
            TriggerTutorial.disableMove = true;

            trigger.StartDialogue(this);  // Pass the NPC reference
        }
    }

    // This method is called when the first conversation ends
    public void OnConversationEnd()
    {
        hasTriggeredFirstConversation = true;  // Mark first conversation as triggered

        // Start the sequence: delay -> set computer active -> set TextMeshPro active -> start second conversation
        StartCoroutine(ShowComputerAndStartSecondDialogue());
    }

    private IEnumerator ShowComputerAndStartSecondDialogue()
    {
        yield return new WaitForSeconds(2f);  // Delay before showing computer (adjust as needed)

        // Set the computer active
        computer.SetActive(true);

        // Activate the 3D TextMeshPro object
        textMeshPro3D.SetActive(true);

        // Perform camera zoom only once
        if (!hasZoomed)
        {
            // Activate the Cinemachine camera and focus on the computer
            vCam.Priority = 11; // Increase priority to make this camera active
            yield return new WaitForSeconds(3f);  // Adjust time for camera focus
            vCam.Priority = 0;  // Reset camera priority after focusing on the computer

            // Mark the zoom as complete
            hasZoomed = true;
        }

        // Start the second conversation only if it hasn't been triggered before
        yield return new WaitForSeconds(1f);  // Short delay before starting the second conversation
        if (!hasTriggeredSecondConversation)
        {
            trigger.StartSecondDialogue(this);
            hasTriggeredSecondConversation = true;  // Mark second conversation as triggered
        }
    }

    // This method is called when the second conversation ends
    public void OnSecondConversationEnd()
    {
        // Re-enable player movement after the second conversation ends
        TriggerTutorial.disableMove = false;
    }
}