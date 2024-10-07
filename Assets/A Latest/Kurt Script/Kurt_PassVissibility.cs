using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Kurt_PassVissibility : MonoBehaviour
{
    public TMP_InputField passwordTMPInputField;  // TextMeshPro InputField
    public Button toggleButton;  // Button to toggle password visibility
    public GameObject eyeClosedImage;  // Image for closed eye (password hidden)
    public GameObject eyeOpenImage;  // Image for open eye (password visible)

    private bool isPasswordVisible = false;  // Track the visibility state

    private void Start()
    {
        if (passwordTMPInputField != null)
        {
            // Initially hide the password
            passwordTMPInputField.contentType = TMP_InputField.ContentType.Password;
            passwordTMPInputField.ForceLabelUpdate();  // Update the input field to apply content type
        }

        // Set the initial state to show the closed eye (password hidden)
        SetEyeImages(isPasswordVisible);

        // Add a listener to the button to toggle visibility and switch images
        toggleButton.onClick.AddListener(TogglePassword);
    }

    public void TogglePassword()
    {
        isPasswordVisible = !isPasswordVisible;  // Toggle the visibility state

        if (passwordTMPInputField != null)
        {
            // Toggle password visibility for TMP_InputField
            passwordTMPInputField.contentType = isPasswordVisible
                ? TMP_InputField.ContentType.Standard  // Show password
                : TMP_InputField.ContentType.Password;  // Hide password
            passwordTMPInputField.ForceLabelUpdate();  // Update TMP input field to reflect changes
        }

        // Toggle between the eye open and closed images
        SetEyeImages(isPasswordVisible);
    }

    private void SetEyeImages(bool isVisible)
    {
        // Show the appropriate image based on the password visibility
        if (eyeClosedImage != null && eyeOpenImage != null)
        {
            eyeClosedImage.SetActive(!isVisible);  // Show the closed eye when password is hidden
            eyeOpenImage.SetActive(isVisible);  // Show the open eye when password is visible
        }
    }
}
