using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class OnlineButtonManager : MonoBehaviour
{
    [Header("Set Nickname")]
    public TMP_InputField newPlayerNameInputField;
    public GameObject bg;
    public GameObject SetNickNamePanel;

    [Header("RememberMe")]
    public bool autoLogin;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;


    public GameObject startPanel;
    public TMP_Text debuggerText;
    // URL of a reliable server
    private string testUrl = "https://www.google.com";

    private void Start()
    {
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
    }
    public void AutoLogin()
    {
        // Does the user want to automatically log in?
        if (autoLogin == true)
        {
            Debug.Log("Auto login");

            //-------------------------------------CHECK INTERNET CONNECION--------------------------------------------//
            CheckInternetConnection();

        }
        else if (autoLogin == false)
        {
            Debug.Log("Auto login is off");
            LoadSecondScene();
        }
    }
    public void CheckInternetConnection()
    {
        // First, check if there's any network connection at all
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No network connection (Wi-Fi, LAN, or mobile).");
            debuggerText.text = "No network connection (Wi-Fi, LAN, or mobile).";
            ShowErrorMessage(debuggerText.text);
        }
        else
        {
            // If connected, check if the network has internet access by making a request
            StartCoroutine(CheckInternetAccess());
        }
    }
    private IEnumerator CheckInternetAccess()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(testUrl))
        {
            // Set a timeout (e.g., 5 seconds) for the request
            request.timeout = 2; // Time in seconds

            // Send the request
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                // Network is available but there is no internet access
                Debug.Log("Connected to a network, but no internet access (or the test URL is unreachable).");
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
                    CheckInternetAutoLogin();
                }
                else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    Debug.Log("Connected via Wi-Fi/LAN and has internet access.");
                    debuggerText.text = "Connected via Wi-Fi/LAN and has internet access.";
                    CheckInternetAutoLogin();
                }
            }
        }
    }
    private void CheckInternetAutoLogin()
    {
        startPanel.SetActive(false);
        LootLockerSDKManager.CheckWhiteLabelSession(response =>
        {
            if (response == false)
            {
                // Session was not valid, show error
                // set the remember me bool to false here, so that the next time the player press login
                // they will get to the login screen
                ShowErrorMessage("error while logging in. Please check your internet connection");
                SceneManager.LoadScene("Login");
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
                    bg.SetActive(true);
                    LeanTween.scale(SetNickNamePanel, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
                }
                else
                {
                    // Player has a name, continue
                    Debug.Log("Player has a name: " + response.name);
                    playerName = response.name;
                    //load the game
                    SceneManager.LoadScene("MainMenu");
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

            //hide the set display name screen
            bg.SetActive(false);
            LeanTween.scale(SetNickNamePanel, Vector3.zero, 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);

            //load the game
            SceneManager.LoadScene("MainMenu");
        });
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

    ///FOR AUTO LOGIN FROM OFFLINE TO ONLINE
    ///
    public void LoadSecondScene()
    {
        // Register callback to be called when the new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load the second scene
        SceneManager.LoadScene("Login");
    }

    // This method is called when the second scene is fully loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Login")
        {
            // Call the method in the second scene to deactivate GameObjects
            AutoLoginManager.Instance.DeactivateGameObjects();

            // Unsubscribe after calling to avoid multiple calls
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
