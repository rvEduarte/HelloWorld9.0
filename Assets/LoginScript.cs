using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    private string gameSceneName = "NewAndLoad";

    // Input fields
    [Header("New User")]
    public TMP_InputField newUserEmailInputField;
    public TMP_InputField newUserPasswordInputField;

    [Header("Existing User")]
    public TMP_InputField existingUserEmailInputField;
    public TMP_InputField existingUserPasswordInputField;

    [Header("Reset password")]
    public TMP_InputField resetPasswordInputField;

    [Header("RememberMe")]
    // Components for enabling auto login
    public Toggle rememberMeToggle;
    private int rememberMe;

    [Header("New Player Name")]
    public TMP_InputField newPlayerNameInputField;

    [Header("Player name")]
    public TextMeshProUGUI playerNameText;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

    [Header("NEW FUCKINSHIT")]
    public GameObject LoginPanel;
    public GameObject SetNickNamePanel;
    public GameObject NewUserPanel;

    public TextMeshProUGUI registerButton;
    public TextMeshProUGUI resetPassButton;

    public void PlayGame()
    {
        //load scene
        SceneManager.LoadScene(gameSceneName);
    }

    // Called when pressing "LOGIN" on the login-page
    public void Login()
    {
        string email = existingUserEmailInputField.text;
        string password = existingUserPasswordInputField.text;

        if (email.Length < 1 || password.Length < 1)
        {
            ShowErrorMessage("Please fill in all fields");
            return;
        }


        void isError(string error)
        {
            if (error.Contains("message"))
            {
                ShowErrorMessage(ExtractMessageFromLootLockerError(error));
                //return;
            }

            if (!error.Contains("message"))
            {
                ShowErrorMessage("Error logging in");
               //s return;
            }

           // loginButtonAnimator.SetTrigger("Error");
            //loginRememberMeAnimator.SetTrigger("Show");
            //loginEmailInputFieldAnimator.SetTrigger("Show");
            //loginPasswordInputFieldAnimator.SetTrigger("Show");
           // loginBackButtonAnimator.SetTrigger("Show");
            return;
        }

        LootLockerSDKManager.WhiteLabelLogin(email, password, Convert.ToBoolean(rememberMe), response =>
        {
            if (!response.success)
            {
                // Error
                isError(response.errorData.ToString());
                Debug.Log("error while logging in");
                return;
            }
            else
            {
                Debug.Log("Player was logged in succesfully");
            }

            // Is the account verified?
            /*if (response.VerifiedAt == null)
            {
                // Stop here if you want to require your players to verify their email before continuing
            }*/

            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                    // Error
                    isError(response.errorData.ToString());
                    return;
                }
                else
                {
                    PlayerPrefs.SetString("LLplayerId", response.player_id.ToString());
                    // Session was succesfully started;
                    // animate the buttons
                    //loginButtonAnimator.SetTrigger("LoggedIn");
                    //loginButtonAnimator.SetTrigger("Hide");
                    LoginPanel.SetActive(false);
                    Debug.Log("session started successfully");
                    CheckIfPlayerHasName(response.public_uid);
                }
            });
        });
    }

    //checks if user has set a display name, if not forces them to set one
    public void CheckIfPlayerHasName(string publicUID)
    {
        string playerName;
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                playerName = response.name;
                //if the players name is the same as their publicUID, they have not set a display name
                if (playerName == "" || playerName.ToLower() == publicUID.ToLower())
                {
                    // Player does not have a name, force them to set one
                    Debug.Log("Player has not set a display name");
                    //show the set display name screen
                    //setDisplayNameCanvasAnimator.CallAppearOnAllAnimators();
                    SetNickNamePanel.SetActive(true);
                }
                else
                {
                    // Player has a name, continue
                    Debug.Log("Player has a name: " + response.name);
                    playerName = response.name;
                    //load the game
                    PlayGame();
                }
            }
        });

    }

    public void UpdatePlayerName()
    {
        //newNickNameCreateButtonAnimator.SetTrigger("UpdateName");
       // newNickNameLogOutButtonAnimator.SetTrigger("Hide");
        //newNickNameInputFieldAnimator.SetTrigger("Hide");

        string newPlayerName = newPlayerNameInputField.text;
        if (newPlayerName == "")
        {
            ShowErrorMessage("Please enter a display name");
            return;
        }

        void isError(string error)
        {
            if (error.Contains("message"))
            {
                string message = ExtractMessageFromLootLockerError(error);
                if (message.Contains("UNIQUE")) ShowErrorMessage("Display name already taken");
                else
                {
                    ShowErrorMessage(message);
                }
            }

            if (!error.Contains("message"))
            {
                ShowErrorMessage("Error setting display name");
            }
            //newNickNameCreateButtonAnimator.ResetTrigger("UpdateName");
            //newNickNameLogOutButtonAnimator.SetTrigger("Show");
           // newNickNameInputFieldAnimator.SetTrigger("Show");
           // newNickNameCreateButtonAnimator.SetTrigger("Error");

            return;
        }
        // Set the players name
        LootLockerSDKManager.SetPlayerName(newPlayerName, (response) =>
        {
            if (!response.success)
            {
                isError(response.errorData.ToString());
                return;
            }

            //setDisplayNameCanvasAnimator.CallDisappearOnAllAnimators();
            //newNickNameCreateButtonAnimator.SetTrigger("Hide");
            SetNickNamePanel.SetActive(false);
            // Write the players name to the screen
            //load the game
            PlayGame();
        });
    }

    // Show an error message on the screen
    public void ShowErrorMessage(string message, int showTime = 3)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();
        //errorScreenAnimator.SetTrigger("Show");
        //wait for 3 seconds and hide the error panel
        Invoke("HideErrorMessage", showTime);
    }

    private void HideErrorMessage()
    {
        //errorScreenAnimator.SetTrigger("Hide");
        errorPanel.SetActive(false);
    }

    public void Logout()
    {
        //remove the auto remember
        PlayerPrefs.SetInt("rememberMe", 0);
        rememberMeToggle.isOn = false;
        rememberMe = 0;

        //createButtonAnimator.SetTrigger("Hide");
        //createButtonAnimator.ResetTrigger("CreateAccount");
        //createButtonAnimator.ResetTrigger("Login");
        //createButtonAnimator.ResetTrigger("ResetPassword");

        existingUserEmailInputField.text = "";
        existingUserPasswordInputField.text = "";


        //end the session
        LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();
        LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
        {
            if (!response.success)
            {
                ShowErrorMessage("Error logging out");
                return;
            }
            PlayerPrefs.DeleteKey("LLplayerId");
            Debug.Log("Logged Out");
        });

    }

    // Called when pressing "CREATE" on new user screen
    public void NewUser()
    {
        string email = newUserEmailInputField.text;
        string password = newUserPasswordInputField.text;
        // string newNickName = nickNameInputField.text;


        if (email.Length < 1 || password.Length < 1)
        {
            ShowErrorMessage("Please fill in all fields");
            return;
        }

        //if password is shorter than 8 characters display an error
        if (password.Length < 8)
        {
            ShowErrorMessage("Password must be at least 8 characters long");
            return;
        }

        void isError(string error)
        {
            if (error.Contains("message"))
            {
                ShowErrorMessage(ExtractMessageFromLootLockerError(error));
            }

            if (!error.Contains("message"))
            {
                ShowErrorMessage("Error creating account");
            }
            //createButtonAnimator.SetTrigger("Error");

            //createBackButtonAnimator.SetTrigger("Show");
            //createPasswordInputFieldAnimator.SetTrigger("Show");
            //createEmailInputFieldAnimator.SetTrigger("Show");
            return;
        }


        //if passes all above checks, create the account
        Debug.Log("Creating account");
        //createButtonAnimator.SetTrigger("CreateAccount");
        //createBackButtonAnimator.SetTrigger("Hide");
        //createPasswordInputFieldAnimator.SetTrigger("Hide");
       // createEmailInputFieldAnimator.SetTrigger("Hide");


        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            if (!response.success)
            {
                isError(response.errorData.ToString());
                return;
            }
            else
            {
                // Succesful response
                // Log in player to set name
                // Login the player
                LootLockerSDKManager.WhiteLabelLogin(email, password, false, response =>
                {
                    if (!response.success)
                    {
                        isError(response.errorData.ToString());
                        return;
                    }
                    // Start session
                    LootLockerSDKManager.StartWhiteLabelSession((response) =>
                    {
                        if (!response.success)
                        {
                            isError(response.errorData.ToString());
                            return;
                        }
                        string publicUID = response.public_uid;
                        // Set nickname to be public UID 
                        string newNickName = response.public_uid;
                        // Set new nickname for player
                        LootLockerSDKManager.SetPlayerName(newNickName, (response) =>
                        {
                            if (!response.success)
                            {
                                ShowErrorMessage("Your account was created but your display name was already taken, you'll be asked to set it when you log in.", 5);
                                // Set public UID as name if setting nickname failed
                                LootLockerSDKManager.SetPlayerName(publicUID, (response) =>
                                {
                                    if (!response.success)
                                    {
                                        ShowErrorMessage("Your account was created but your display name was already taken, you'll be asked to set it when you log in.", 5);
                                    }
                                });
                            }

                            // End this session
                            LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();
                            LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
                            {
                                if (!response.success)
                                {
                                    ShowErrorMessage("Account created but error ending session");
                                    return;
                                }
                                Debug.Log("Account Created");
                                //createButtonAnimator.SetTrigger("AccountCreated");
                                //createBackButtonAnimator.SetTrigger("Show");
                                registerButton.text = "AccountCreated";
                                // New user, turn off remember me
                                rememberMeToggle.isOn = false;
                            });
                        });
                    });
                });
            }
        });
    }

    // Start is called before the first frame update
    public void Start()
    {
        // See if we should log in the player automatically
        rememberMe = PlayerPrefs.GetInt("rememberMe", 0);
        if (rememberMe == 0)
        {
            rememberMeToggle.isOn = false;
        }
        else
        {
            rememberMeToggle.isOn = true;
        }
    }

    // Called when changing the value on the toggle
    public void ToggleRememberMe()
    {
        bool rememberMeBool = rememberMeToggle.isOn;
        rememberMe = Convert.ToInt32(rememberMeBool);

        // Animate button
        if (rememberMeBool == true)
        {
            //rememberMeAnimator.SetTrigger("On");
            Debug.Log("CHECKED");
        }
        else
        {
            //rememberMeAnimator.SetTrigger("Off");
            Debug.Log(" NOT CHECKED");
        }
        PlayerPrefs.SetInt("rememberMe", rememberMe);
    }

    public void AutoLogin()
    {
        // Does the user want to automatically log in?
        if (Convert.ToBoolean(rememberMe) == true)
        {
            Debug.Log("Auto login");
            // Hide the buttons on the login screen
            /*existingUserEmailInputField.GetComponent<Animator>().ResetTrigger("Show");
            existingUserEmailInputField.GetComponent<Animator>().SetTrigger("Hide");
            existingUserEmailInputField.GetComponent<Animator>().ResetTrigger("Show");
            existingUserPasswordInputField.GetComponent<Animator>().SetTrigger("Hide");*/
            //loginBackButtonAnimator.ResetTrigger("Show");
            //loginBackButtonAnimator.SetTrigger("Hide");
            LoginPanel.SetActive(false);
            // Start to spin the login button
            //loginButtonAnimator.ResetTrigger("Hide");
            //loginButtonAnimator.SetTrigger("Hide");

            LootLockerSDKManager.CheckWhiteLabelSession(response =>
            {
                if (response == false)
                {
                    // Session was not valid, show error animation
                    // and show back button
                    //loginButtonAnimator.SetTrigger("Error");
                    //loginBackButtonAnimator.SetTrigger("Show");

                    // set the remember me bool to false here, so that the next time the player press login
                    // they will get to the login screen
                    rememberMeToggle.isOn = false;
                }
                else
                {
                    // Session is valid, start game session
                    LootLockerSDKManager.StartWhiteLabelSession((response) =>
                    {
                        if (response.success)
                        {
                            PlayerPrefs.SetString("LLplayerId", response.player_id.ToString());
                            // It was succeful, log in
                            //loginButtonAnimator.SetTrigger("Hide");
                            //loginBackButtonAnimator.SetTrigger("Hide");
                            // Write the current players name to the screen
                            CheckIfPlayerHasName(response.public_uid);
                        }
                        else
                        {
                            // Error
                            // Animate the buttons
                            //loginButtonAnimator.SetTrigger("Error");
                            //loginBackButtonAnimator.SetTrigger("Show");

                            Debug.Log("error starting LootLocker session");
                            // set the remember me bool to false here, so that the next time the player press login
                            // they will get to the login screen
                            rememberMeToggle.isOn = false;

                            return;
                        }

                    });

                }

            });
        }
        else if (Convert.ToBoolean(rememberMe) == false)
        {
            Debug.Log("Auto login is off");
            // Continue as usual
            //loginCanvasAnimator.CallAppearOnAllAnimators();
            //   loginButtonAnimator.ResetTrigger("Show");
            //loginButtonAnimator.SetTrigger("Show");
        }
    }

    public void PasswordReset()
    {
        string email = resetPasswordInputField.text;
        LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error requesting password reset");
                //get the message from the error and dsiplay it 

                if (response.errorData.ToString().Contains("message"))
                {
                    ShowErrorMessage(ExtractMessageFromLootLockerError(response.errorData.ToString()));
                }

                if (!response.errorData.ToString().Contains("message"))
                {
                    ShowErrorMessage("Error requesting password reset");
                }

                //resetPasswordButtonAnimator.SetTrigger("Error");

                // make the buttons show again 
                //resetBackButtonAnimator.SetTrigger("Show");
                //resetEmailInputFieldAnimator.SetTrigger("Show");

                return;
            }

            Debug.Log("requested password reset successfully");
            // resetEmailInputFieldAnimator.SetTrigger("Hide");
            //resetPasswordButtonAnimator.SetTrigger("Done");
            // resetBackButtonAnimator.SetTrigger("Show");
            resetPassButton.text = "SENT";
        });
    }

    public void ResendVerificationEmail()
    {
        int playerID = 0;
        LootLockerSDKManager.WhiteLabelRequestVerification(playerID, (response) =>
        {
            if (response.success)
            {
                // Email was sent!
            }
        });
    }

    private string ExtractMessageFromLootLockerError(string rawError)
    {
        // Find the start index of the message
        int startIndex = rawError.IndexOf("\"") + 1; // Skip the first quote
        if (startIndex == 0)
        {
            return "Message not found"; // Handle case where the first quote is not found
        }

        // Find the end index of the message
        int endIndex = rawError.IndexOf("\"", startIndex); // Find the closing quote
        if (endIndex == -1)
        {
            return "Message not properly terminated"; // Handle case where the message is not properly terminated
        }

        // Extract the message
        string message = rawError.Substring(startIndex, endIndex - startIndex);

        return message;
    }
}

