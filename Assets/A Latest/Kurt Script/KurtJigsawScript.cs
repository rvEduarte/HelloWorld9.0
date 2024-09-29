using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class KurtJigsawScript : MonoBehaviour
{
    public PlayerController playerController; // Reference to the player controller
    [SerializeField]
    private GameObject pickUpText; // Text to show when near the jigsaw

    public GameObject jigsawPanel; // The jigsaw panel UI

    public GameObject jigsaw; // The jigsaw GameObject

    private bool pickUpAllowed; // Flag to allow pickup
    public bool isClicked = false; // Track if the jigsaw has been clicked

    void Start()
    {
        jigsawPanel.SetActive(false); // Hide the jigsaw panel at the start
        pickUpText.SetActive(false); // Hide the pickup text
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            // Show the jigsaw panel when picked up
            jigsawPanel.SetActive(true);
            playerController.enabled = false; // Disable player movement

            // Optional: Disable specific movement types
            // TriggerTutorial.disableMove = true; // Commented out to enable movement
            // TriggerTutorial.disableJump = true; // If you want to disable jumping, keep this

            Debug.Log("Jigsaw picked up");
            Destroy(jigsaw); // Destroy the jigsaw GameObject
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpText.SetActive(true); // Show the pickup text
            pickUpAllowed = true; // Allow pickup
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpText.SetActive(false); // Hide the pickup text
            pickUpAllowed = false; // Disallow pickup
        }
    }

    // Call this method to close the jigsaw panel and enable player movement
    public void CloseJigsawPanel()
    {
        jigsawPanel.SetActive(false); // Hide the jigsaw panel
        playerController.enabled = true; // Enable player movement
    }
}
