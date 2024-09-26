using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KurtPlatfrom3Quiz : MonoBehaviour
{
    public GameObject platform;           // The platform to be moved
    public GameObject blockedDoor;        // The door that blocks the player's way
    public InputField consoleInput;       // Player's input field (console)
    public Text outputText;               // Output screen to display messages and hints
    public float platformRiseSpeed = 2f;  // Speed at which the platform rises
    public Transform targetPosition;      // The target position for the platform to reach
    public Button submitButton;           // Button to submit the player's code
    private bool doorIsOpen = false;      // Variable to check if the door is open
    private bool isPlatformRising = false;

    private string currentHint = "";      // Holds the current hint for the player

    void Start()
    {
        submitButton.onClick.AddListener(CheckPlayerCode);  // Attach button event to check the code
        outputText.text = "Hint: Complete the code to open the door and move the platform.";
        ShowGuidedHint(); // Display the first guided hint
    }

    void Update()
    {
        // Move platform if the correct code is entered and the door is open
        if (isPlatformRising && platform.transform.position.y < targetPosition.position.y)
        {
            platform.transform.position = Vector3.MoveTowards(
                platform.transform.position,
                targetPosition.position,
                platformRiseSpeed * Time.deltaTime
            );
        }
    }

    // Show hints with partial code to guide the player
    void ShowGuidedHint()
    {
        if (!doorIsOpen)
        {
            // Provide the player with a hint for setting the boolean variable
            currentHint = "System.out.print(\"The door is \" + )";
            outputText.text = currentHint + " (Hint: Declare a variable like 'bool doorIsOpen = true;')";
        }
        else if (!isPlatformRising)
        {
            // Once the door is open, guide the player to move the platform
            currentHint = "To move the platform, type 'platform.Rise();'";
            outputText.text = currentHint + " (Hint: The door is already open!)";
        }
    }

    // Function to check player's input and validate it
    void CheckPlayerCode()
    {
        string playerInput = consoleInput.text.Trim();  // Get the player's input and remove extra spaces

        // Condition to declare and set the variable to open the door
        if (playerInput == "bool doorIsOpen = true;")
        {
            doorIsOpen = true;
            outputText.text = "Correct! The door is now open. Enter 'platform.Rise();' to move the platform.";
            blockedDoor.SetActive(false);  // Hide the blocked door
        }
        // Condition to move the platform only if the door is open
        else if (playerInput == "platform.Rise();" && doorIsOpen)
        {
            outputText.text = "Correct! The platform is rising...";
            isPlatformRising = true;
        }
        // Feedback for incorrect input
        else if (playerInput == "platform.Rise();" && !doorIsOpen)
        {
            outputText.text = "The door is still locked. Solve the code to open the door first!";
        }
        else
        {
            // If the player enters incorrect input, prompt them with the current hint
            outputText.text = "Incorrect syntax. " + currentHint;
        }

        // Display the next guided hint after checking input
        ShowGuidedHint();
    }
}
