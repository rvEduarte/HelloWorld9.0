using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    public GameObject bridge;                   // Reference to the bridge GameObject
    public TextMeshProUGUI consoleOutput;       // TextMeshProUGUI component for console output
    public TMP_InputField consoleInput;         // TMP_InputField for player input
    private bool isBridgeRaised = false;        // To check if the bridge is already raised

    private void Start()
    {
        consoleOutput.text = "Console: Type commands to raise the bridge.\n";
        consoleOutput.text += "Commands: setBridgeHeight <int>, toggleBridge <bool>, setBridgeStatus <string>.\n";
        consoleInput.onEndEdit.AddListener(HandleConsoleInput); // Add listener for input submission
    }

    // Handles the player's input from the console
    private void HandleConsoleInput(string input)
    {
        consoleInput.text = ""; // Clear the input field after submission
        consoleInput.ActivateInputField(); // Re-focus the input field after clearing

        if (input.StartsWith("setBridgeHeight"))
        {
            SetBridgeHeightCommand(input);
        }
        else if (input.StartsWith("toggleBridge"))
        {
            ToggleBridgeCommand(input);
        }
        else if (input.StartsWith("setBridgeStatus"))
        {
            SetBridgeStatusCommand(input);
        }
        else
        {
            DisplayMessage("Invalid command. Try again.");
        }
    }

    // Command to set the height of the bridge
    private void SetBridgeHeightCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length == 2 && int.TryParse(parts[1], out int height))
        {
            if (height > 0 && !isBridgeRaised)
            {
                bridge.transform.position += new Vector3(0, height, 0); // Move the bridge up
                isBridgeRaised = true;
                DisplayMessage($"Bridge raised by {height} units.");
            }
            else if (isBridgeRaised)
            {
                DisplayMessage("Bridge is already raised.");
            }
            else
            {
                DisplayMessage("Height must be greater than 0.");
            }
        }
        else
        {
            DisplayMessage("Usage: setBridgeHeight <int>");
        }
    }

    // Command to toggle the bridge up or down using a boolean
    private void ToggleBridgeCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length == 2 && bool.TryParse(parts[1], out bool toggle))
        {
            if (toggle && !isBridgeRaised)
            {
                bridge.transform.position += new Vector3(0, 5, 0); // Move the bridge up
                isBridgeRaised = true;
                DisplayMessage("Bridge raised.");
            }
            else if (!toggle && isBridgeRaised)
            {
                bridge.transform.position -= new Vector3(0, 5, 0); // Move the bridge down
                isBridgeRaised = false;
                DisplayMessage("Bridge lowered.");
            }
            else
            {
                DisplayMessage("Bridge is already in the desired state.");
            }
        }
        else
        {
            DisplayMessage("Usage: toggleBridge <bool>");
        }
    }

    // Command to set the status of the bridge with a string
    private void SetBridgeStatusCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length == 2)
        {
            string status = parts[1].ToLower();
            if (status == "up" && !isBridgeRaised)
            {
                bridge.transform.position += new Vector3(0, 5, 0); // Move the bridge up
                isBridgeRaised = true;
                DisplayMessage("Bridge status set to UP.");
            }
            else if (status == "down" && isBridgeRaised)
            {
                bridge.transform.position -= new Vector3(0, 5, 0); // Move the bridge down
                isBridgeRaised = false;
                DisplayMessage("Bridge status set to DOWN.");
            }
            else
            {
                DisplayMessage("Bridge is already in the desired status.");
            }
        }
        else
        {
            DisplayMessage("Usage: setBridgeStatus <string> (up/down)");
        }
    }

    // Display a message on the console output
    private void DisplayMessage(string message)
    {
        consoleOutput.text += message + "\n";
    }
}
