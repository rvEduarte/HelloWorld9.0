using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class LoginScript : MonoBehaviour
{
    public LootlockerSceneProgress progressData;

    [Header("Buttons")]
    public Button registerButtonDisableInteractable;
    public Button resetButtonDisableInteractable;

    [Header("Scenes")]
    private string mainMenuScene = "MainMenu";
    private string offlineMainMenuScene = "Offline_MainMenu";

    public LevelUnlockScriptable levelUnlockScriptable;

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
    public bool autoLogin;

    [Header("New Player Name")]
    public TMP_InputField newPlayerNameInputField;

    [Header("Player name")]
    public TextMeshProUGUI playerNameText;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

    [Header("NEW FEATURE")]
    public GameObject onlineButtonPanel;
    public GameObject LoginPanel;
    public GameObject SetNickNamePanel;
    public GameObject NewUserPanel;

    public TextMeshProUGUI registerButton;
    public TextMeshProUGUI resetPassButton;

    public GameObject disableRegisterButton;

    public Button registerToLogin;

    [Header("AUTO LOGIN")]
    public GameObject onlineButtons;
    public GameObject startButtons;

    [Header("DEBUGGER TEXXT")]

    public TMP_Text debuggerText;
    // URL of a reliable server (can be adjusted if needed)
    private string testUrl = "https://www.google.com";

    // Start is called before the first frame update

    public void Start()
    {


        registerButtonDisableInteractable.enabled = true;
        progressData.LoadFromLocalFile();

        // See if we should log in the player automatically
        if (PlayerPrefs.GetInt("AutoLogin") == 1)
        {
            autoLogin = true;
            Debug.Log("TRUE");
        }
        else
        {
            autoLogin = false;
            Debug.Log("FALSE");
        }

        //Register State
        if (PlayerPrefs.GetInt("RegisterState") == 1)
        {
            disableRegisterButton.gameObject.SetActive(false);
        }
        else
        {
            disableRegisterButton.gameObject.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(mainMenuScene);     
    }
    public void PlayGameOffline()
    {
        SceneManager.LoadScene(offlineMainMenuScene);
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

        CheckInternetConnection(4);
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

            PlayerPrefs.SetString("PlayerName", newPlayerName);
            PlayerPrefs.Save();

            SetNickNamePanel.SetActive(false);
            //load the game
            PlayGame();
        });
    }

    // Show an error message on the screen
    public void ShowErrorMessage(string message, float showTime = 1.5f)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();

        //wait for 3 seconds and hide the error panel
        Invoke("HideErrorMessage", showTime);
    }

    private void HideErrorMessage()
    {
        errorPanel.SetActive(false);
    }

    // Called when pressing "CREATE" on new user screen
    public void NewUser()
    {
        string email = newUserEmailInputField.text;
        string password = newUserPasswordInputField.text;


        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please fill in all fields");
            return;
        }

        if (!email.Contains("@") || !email.EndsWith(".com"))
        {
            ShowErrorMessage("Please enter a valid email address (example@domain.com)");
            return;
        }

        //if password is shorter than 8 characters display an error
        if (password.Length < 8)
        {
            ShowErrorMessage("Password must be at least 8 characters long");
            return;
        }

        CheckInternetConnection(2);

    }
    public void RegisterToLogin()
    {
        string email = newUserEmailInputField.text;
        string password = newUserPasswordInputField.text;

        if (email.Length < 1 || password.Length < 1)
        {
            ShowErrorMessage("Please fill in all fields");
            return;
        }

        CheckInternetConnection(3);

    }

    public void AutoLogin()
    {
        // Does the user want to automatically log in?
        if (autoLogin == true)
        {
            Debug.Log("Auto login");

            //-------------------------------------CHECK INTERNET CONNECION--------------------------------------------//
            CheckInternetConnection(1);

        }
        else if (autoLogin == false)
        {
            Debug.Log("Auto login is off");
            // Continue as usual
            startButtons.SetActive(false);
            onlineButtons.SetActive(true);
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

                return;
            }

            Debug.Log("requested password reset successfully");

            resetPassButton.text = "SENT";
            resetButtonDisableInteractable.enabled = false;
        });
    }

    public void ResendVerificationEmail()
    {
        int code = PlayerPrefs.GetInt("VerificationCode");
        LootLockerSDKManager.WhiteLabelRequestVerification(code, (response) =>
        {
            if (!response.success)
            {
                Debug.LogError("Error sending verification email: " + response.errorData.ToString());
            }
            else
            {
                Debug.Log("Verification email sent successfully.");
            }
        });
        return;
    }

    private string ExtractMessageFromLootLockerError(string rawError)
    {
        //tite
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

    public void OnApplicationQuit()
    {
        Debug.Log("Succesfully Quit");
        Application.Quit();
    }

    public void ClearInput()
    {
        //tite
        registerButton.text = "Register";
        registerButtonDisableInteractable.enabled = true;
        registerToLogin.gameObject.SetActive(false);
        //empty
        newUserEmailInputField.text = string.Empty;
        newUserPasswordInputField.text = string.Empty;
        //empty
        existingUserEmailInputField.text = string.Empty;
        existingUserPasswordInputField.text = string.Empty;

        resetPassButton.text = "RESET";
        resetButtonDisableInteractable.enabled = true;
        //empty
        resetPasswordInputField.text = string.Empty;
    }
    public void CheckInternetConnection(int number)
    {
        // First, check if there's any network connection at all
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No network connection");
            debuggerText.text = "No network connection (Wi-Fi, LAN, or mobile).";
            ShowErrorMessage(debuggerText.text);
        }
        else
        {
            // If connected, check if the network has internet access by making a request
            StartCoroutine(CheckInternetAccess(number));
        }
    }

    private IEnumerator CheckInternetAccess(int number)
    {
        string email = newUserEmailInputField.text;
        string password = newUserPasswordInputField.text;

        string existingEmail = existingUserEmailInputField.text;
        string existingPassword = existingUserPasswordInputField.text;

        using (UnityWebRequest request = UnityWebRequest.Get(testUrl))
        {
            // Set a timeout (e.g., 5 seconds) for the request
            request.timeout = 2; // Time in seconds

            // Send the request
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                // Network is available but there is no internet access
                Debug.Log("Connected to a network, but no internet access");
                debuggerText.text = "Connected to a network, but no internet access";
                ShowErrorMessage(debuggerText.text);
            }
            else
            {
                // Network has internet access
                Debug.Log("Network has internet access.");
                if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                {
                    Debug.Log("Connected via mobile data and has internet access.");
                    debuggerText.text = "Connected via mobile data and has internet access.";

                    if(number == 1)
                    {
                        CheckInternetAutoLogin();
                    }
                    else if (number == 2)
                    {
                        CheckInterNewUser(email, password);
                    }
                    else if (number == 3)
                    {
                        Debug.Log("FUCK PUMASOK");
                        debuggerText.text = "FUCK PUMASOK";
                        CheckInternetRegisterToLogin(email, password);
                    }
                    else if(number == 4)
                    {
                        CheckInternetLogin(existingEmail, existingPassword);
                    }

                }
                else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    Debug.Log("Connected via Wi-Fi/LAN and has internet access.");
                    debuggerText.text = "Connected via Wi-Fi/LAN and has internet access.";

                    if (number == 1)
                    {
                        CheckInternetAutoLogin();
                    }
                    else if (number == 2)
                    {
                        CheckInterNewUser(email, password);
                    }
                    else if (number == 3)
                    {
                        Debug.Log("FUCK PUMASOK");
                        debuggerText.text = "FUCK PUMASOK";
                        CheckInternetRegisterToLogin(email, password);
                    }
                    else if (number == 4)
                    {
                        CheckInternetLogin(existingEmail, existingPassword);
                    }
                }
            }
        }
    }

    private void CheckInternetLogin(string email, string password)
    {
        void isError(string error)
        {
            if (error.Contains("message"))
            {
                ShowErrorMessage("Incorrect Password/Email");
                return;
            }

            if (!error.Contains("message"))
            {
                ShowErrorMessage("Error logging in");
                return;
            }

            return;
        }

        LootLockerSDKManager.WhiteLabelLogin(email, password, response =>
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

            }

            // Is the account verified?
            if (response.VerifiedAt == null)
            {
                ShowErrorMessage("Your account is not verified. Please check your email for verification.");
            }
            else
            {
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
                        LoginPanel.SetActive(false);
                        Debug.Log("session started successfully");
                        CheckIfPlayerHasName(response.public_uid);

                        //DisableRegisterButton
                        disableRegisterButton.gameObject.SetActive(false);

                        Debug.Log("Player was logged in succesfully");
                        PlayerPrefs.SetInt("RegisterState", 1);
                        PlayerPrefs.SetInt("AutoLogin", 1);
                        PlayerPrefs.Save();
                    }
                });
            }
        });
    }

    private void CheckInterNewUser(string email, string password)
    {
        void isError(string error)
        {
            if (error.Contains("message"))
            {
                string message = ExtractMessageFromLootLockerError(error);
                if (message.Contains("UNIQUE"))
                {
                    ShowErrorMessage("Display name already taken");
                }
                else
                {
                    ShowErrorMessage(message);
                }
            }

            if (!error.Contains("message"))
            {
                ShowErrorMessage("Error creating account");
            }

            return;
        }

        //if passes all above checks, create the account
        Debug.Log("Creating account");

        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            if (!response.success)
            {
                isError(response.errorData.ToString());
                return;
            }
            else
            {
                // Store the ID from the sign-up response
                PlayerPrefs.SetInt("VerificationCode", response.ID);
                PlayerPrefs.Save();

                Debug.Log("Account Created");
                registerButtonDisableInteractable.enabled = false;
                registerToLogin.gameObject.SetActive(true);

                // Send verification email
                ResendVerificationEmail();
            }
        });
    }
    private void CheckInternetRegisterToLogin(string email, string password)
    {
        void isError(string error)
        {
            if (error.Contains("message"))
            {
                ShowErrorMessage("Incorrect Password/Email");
                return;
            }

            if (!error.Contains("message"))
            {
                ShowErrorMessage("Error logging in");
                return;
            }

            return;
        }

        LootLockerSDKManager.WhiteLabelLogin(email, password, response =>
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

            }

            // Is the account verified?
            if (response.VerifiedAt == null)
            {
                ShowErrorMessage("Please check your email for the verification.");
            }
            else
            {
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
                        LoginPanel.SetActive(false);
                        Debug.Log("session started successfully");
                        CheckIfPlayerHasName(response.public_uid);

                        //DisableRegisterButton
                        disableRegisterButton.gameObject.SetActive(false);

                        //HideRegisterPanel
                        NewUserPanel.SetActive(false);

                        Debug.Log("Player was logged in succesfully");
                        PlayerPrefs.SetInt("RegisterState", 1);
                        PlayerPrefs.SetInt("AutoLogin", 1);
                        PlayerPrefs.Save();
                    }
                });
            }
        });
    }

    private void CheckInternetAutoLogin()
    {
        // Hide the buttons on the login screen
        startButtons.SetActive(false);
        onlineButtons.SetActive(false);

        // Hide the buttons on the login screen
        //onlineButtonPanel.SetActive(false);
        //LoginPanel.SetActive(false);


        LootLockerSDKManager.CheckWhiteLabelSession(response =>
        {
            if (response == false)
            {
                // Session was not valid, show error
                // set the remember me bool to false here, so that the next time the player press login
                // they will get to the login screen
                ShowErrorMessage("error while logging in. Please check your internet connection");
                //LoginPanel.SetActive(true);
                //onlineButtonPanel.SetActive(true);

                //PlayerPrefs.SetInt("AutoLogin", 0);
                //PlayerPrefs.Save();
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
                        // Write the current players name to the screen
                        CheckIfPlayerHasName(response.public_uid);
                    }
                    else
                    {
                        // Error
                        Debug.Log("error starting LootLocker session");
                        // set the remember me bool to false here, so that the next time the player press login
                        // they will get to the login screen
                        PlayerPrefs.SetInt("AutoLogin", 0);
                        PlayerPrefs.Save();

                        return;
                    }
                });
            }
        });
    }
}

