using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;  // Needed for TilemapRenderer and Tilemap

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

    [Header("Secret Passage")]
    public Tilemap secretPassageTilemap;  // Tilemap for the secret passage
    private TilemapRenderer passageRenderer;  // Renderer for the secret passage
    private TilemapCollider2D passageCollider;  // Collider for the secret passage

    public void Start()
    {
        computer.SetActive(false);
        triggerPortalConvo.SetActive(false);

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
            passageRenderer.enabled = true;
            passageCollider.enabled = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters and if the first conversation hasn't been triggered
        if (collision.gameObject.CompareTag("Player") && !hasTriggeredFirstConversation)
        {
            // Disable player movement for the first conversation
            TriggerTutorial.disableMove = true;

            // Make the secret passage visible and enable its collider if the Tilemap is assigned
            if (secretPassageTilemap != null)
            {
                if (passageRenderer == null || passageCollider == null)  // Get components if not assigned
                {
                    passageRenderer = secretPassageTilemap.GetComponent<TilemapRenderer>();
                    passageCollider = secretPassageTilemap.GetComponent<TilemapCollider2D>();
                }
                passageRenderer.enabled = false;  // Show the passage
                passageCollider.enabled = false;  // Enable passage's collider
                Debug.Log("Secret passage revealed.");
            }

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

        // Check if the computer reference is assigned before activating it
        if (computer != null)
        {
            // Set the computer active
            computer.SetActive(true);
            Debug.Log("Computer activated.");
        }
        else
        {
            Debug.LogWarning("Computer GameObject is not assigned.");
        }

        // Activate the 3D TextMeshPro object if it's assigned
        if (textMeshPro3D != null)
        {
            textMeshPro3D.SetActive(true);
            Debug.Log("TextMeshPro object activated.");
        }
        else
        {
            Debug.LogWarning("textMeshPro3D is not assigned.");
        }

        // Perform camera zoom only once
        if (!hasZoomed)
        {
            // Activate the Cinemachine camera and focus on the computer
            vCam.Priority = 11; // Increase priority to make this camera active
            Debug.Log("Camera zoom started.");
            yield return new WaitForSeconds(3f);  // Adjust time for camera focus
            vCam.Priority = 0;  // Reset camera priority after focusing on the computer
            Debug.Log("Camera zoom ended.");

            // Mark the zoom as complete
            hasZoomed = true;
        }

        // Start the second conversation only if it hasn't been triggered before
        yield return new WaitForSeconds(1f);  // Short delay before starting the second conversation
        if (!hasTriggeredSecondConversation)
        {
            trigger.StartSecondDialogue(this);
            hasTriggeredSecondConversation = true;  // Mark second conversation as triggered
            Debug.Log("Second conversation started.");
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
