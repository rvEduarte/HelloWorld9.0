using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class K_NPCLvl2Ph2 : MonoBehaviour
{
    public K_DialogueTriggerLvl2Ph2 trigger;

    public GameObject computer;  // Reference to the computer object (can be optional)
    private bool hasTriggeredFirstConversation = false;  // Flag to ensure one-time first conversation
    private bool hasTriggeredSecondConversation = false; // Flag to ensure one-time second conversation
    private bool hasZoomed = false;  // Flag to ensure the camera zooms only once

    [Header("Optional Trigger Portal Conversation")]
    public GameObject triggerPortalConvo; // This can be optional now

    [Header("Camera Target")]
    public CinemachineVirtualCamera vCam; // Reference to the virtual camera

    public GameObject textMeshPro3D; // Reference to the 3D TextMeshPro object

    [Header("Secret Passage")]
    public Tilemap secretPassageTilemap;  // Tilemap for the secret passage
    private TilemapRenderer passageRenderer;  // Renderer for the secret passage
    private TilemapCollider2D passageCollider;  // Collider for the secret passage

    public void Start()
    {
        // Check if computer is assigned before attempting to deactivate it
        if (computer != null)
        {
            computer.SetActive(false);
        }

        // Only deactivate triggerPortalConvo if it is assigned
        if (triggerPortalConvo != null)
        {
            triggerPortalConvo.SetActive(false);
        }

        // Ensure the TextMeshPro object is initially inactive
        if (textMeshPro3D != null)
        {
            textMeshPro3D.SetActive(false);
        }

        // Make sure the secret passage is initially hidden if the Tilemap is assigned
        if (secretPassageTilemap != null)
        {
            passageRenderer = secretPassageTilemap.GetComponent<TilemapRenderer>();
            passageCollider = secretPassageTilemap.GetComponent<TilemapCollider2D>();

            // Initially hide the secret passage and disable its collider
            if (passageRenderer != null)
            {
                passageRenderer.enabled = true;
            }
            if (passageCollider != null)
            {
                passageCollider.enabled = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters and if the first conversation hasn't been triggered
        if (collision.gameObject.CompareTag("Player") && !hasTriggeredFirstConversation)
        {
            // Disable player movement for the first conversation
            TriggerTutorial.disableMove = true;

            if (trigger != null)
            {
                trigger.StartDialogue(this);  // Pass the NPC reference
            }
            else
            {
                Debug.LogError("Trigger is not assigned in K_NPCLvl2Ph2 script.");
            }
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

        // Check if the computer reference is assigned before activating it
        if (computer != null)
        {
            // Set the computer active
            computer.SetActive(true);
            Debug.Log("Computer activated.");
        }
        else
        {
            Debug.LogWarning("Computer reference is not assigned in K_NPCLvl2Ph2 script.");
        }

        // Activate the 3D TextMeshPro object if it's assigned
        if (textMeshPro3D != null)
        {
            textMeshPro3D.SetActive(true);
            Debug.Log("TextMeshPro object activated.");
        }
        else
        {
            Debug.LogWarning("TextMeshPro object reference is not assigned in K_NPCLvl2Ph2 script.");
        }

        // Perform camera zoom and reveal the secret passage in sync
        if (!hasZoomed)
        {
            // Activate the Cinemachine camera and focus on the computer
            if (vCam != null)
            {
                vCam.Priority = 11; // Increase priority to make this camera active
                Debug.Log("Camera zoom started.");
            }
            else
            {
                Debug.LogWarning("CinemachineVirtualCamera reference is not assigned in K_NPCLvl2Ph2 script.");
            }

            // Wait for the camera zoom duration and reveal the passage simultaneously
            StartCoroutine(RevealSecretPassageDuringZoom(3f));  // Adjust zoom time as needed

            yield return new WaitForSeconds(3f);  // Wait for the camera zoom to complete

            if (vCam != null)
            {
                vCam.Priority = 0;  // Reset camera priority after focusing on the computer
                Debug.Log("Camera zoom ended.");
            }

            // Mark the zoom as complete
            hasZoomed = true;
        }

        // Start the second conversation only if it hasn't been triggered before
        yield return new WaitForSeconds(1f);  // Short delay before starting the second conversation
        if (!hasTriggeredSecondConversation)
        {
            if (trigger != null)
            {
                trigger.StartSecondDialogue(this);
                hasTriggeredSecondConversation = true;  // Mark second conversation as triggered
                Debug.Log("Second conversation started.");
            }
            else
            {
                Debug.LogWarning("Trigger reference is not assigned in K_NPCLvl2Ph2 script.");
            }
        }
    }

    // Coroutine to reveal the secret passage during the camera zoom
    private IEnumerator RevealSecretPassageDuringZoom(float zoomDuration)
    {
        yield return new WaitForSeconds(zoomDuration * 0.5f);  // Reveal halfway through the zoom duration

        // Make the secret passage visible and enable its collider if the Tilemap is assigned
        if (secretPassageTilemap != null)
        {
            if (passageRenderer == null || passageCollider == null)  // Get components if not assigned
            {
                passageRenderer = secretPassageTilemap.GetComponent<TilemapRenderer>();
                passageCollider = secretPassageTilemap.GetComponent<TilemapCollider2D>();
            }
            if (passageRenderer != null)
            {
                passageRenderer.enabled = false;  // Show the passage
                Debug.Log("Secret passage revealed during camera zoom.");
            }
            if (passageCollider != null)
            {
                passageCollider.enabled = false;  // Enable passage's collider
            }
        }
    }

    // This method is called when the second conversation ends
    public void OnSecondConversationEnd()
    {
        // Re-enable player movement after the second conversation ends
        TriggerTutorial.disableMove = false;
        Debug.Log("Player movement re-enabled.");
    }
}
