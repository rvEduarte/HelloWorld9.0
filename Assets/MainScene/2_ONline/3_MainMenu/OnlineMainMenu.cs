using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineMainMenu : MonoBehaviour
{
    public LeaderboardPersistentDataScriptable lDataScriptableObject;
    public LevelProgressionLootlockerV2 progress;
    public LootlockerSceneProgress progressData;

    [SerializeField] public bool clicked; //FALSE
    public LevelUnlockScriptable levelUnlockScriptable;

    private string loginSceneName = "Login";

    [Header("Player Name")]
    public TextMeshProUGUI playerNameText;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

    public TMP_Text debuggerText;
    private string testUrl = "https://www.google.com";

    private float checkInterval = 10f; // Check every 10 seconds
    private float timeSinceLastCheck = 0f;

    public GameObject offlineButton;
    public VerticalLayoutGroup vlayoutGroup;

    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= checkInterval)
        {
            CheckInternetConnection();
            timeSinceLastCheck = 0f; // Reset timer
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        // Load the saved state
        if (PlayerPrefs.GetInt("uploadPlayer") == 1)
        {
            clicked = true;
        }
        else
        {
            clicked = false;
        }

        // check if they have player id
        string playerId = PlayerPrefs.GetString("LLplayerId", "nada");
        if (playerId == "nada")
        {
            onNoPLayerId();
            return;
        }

        //get player name
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                playerNameText.text = response.name;

                PlayerPrefs.SetString("PlayerName", playerNameText.text);
                PlayerPrefs.Save();

                
            }
        });
        //DISPLAY PLAYER NAME
        playerNameText.text = PlayerPrefs.GetString("PlayerName");
        //SubmitLeaderBoardScript.GetPlayerHighScore();
    }
    public void PlayButtonOnline(string name)
    {
        if (!clicked)
        {
            progressData.UploadFileFromPath(levelUnlockScriptable);
            Debug.Log(levelUnlockScriptable);

            // Mark the action as performed and save the state
            PlayerPrefs.SetInt("uploadPlayer", 1); // 1 means true
            PlayerPrefs.Save(); // Make sure changes are saved to disk

            clicked = true;

            StartCoroutine(DelayedSceneLoad(name, 0.5f));
            Debug.Log("ISA LANG");
        }

        StartCoroutine(DelayedSceneLoad(name, 0.5f));
        Debug.Log("YES");
    }
    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene loaded after delay");
    }
    public void GotoScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void onNoPLayerId()
    {
        showErrorMessage("You're not logged in");
        //wait 3 seconds and then logout 
        Invoke("goBackToLogin", 3);
    }
    // Show an error message on the screen
    public void showErrorMessage(string message, int showTime = 3)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();
        //wait for 3 seconds and hide the error panel
        Invoke("hideErrorMessage", showTime);
    }
    // hide error message on the screen
    private void hideErrorMessage()
    {
        errorPanel.SetActive(false);
    }
    // End player online session
    public void logout()
    {
        //end the session
        LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();

        LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
        {
            if (!response.success)
            {
                showErrorMessage("Error logging out");
                return;
            }
            PlayerPrefs.DeleteKey("LLplayerId");
            PlayerPrefs.Save();
            LootLockerSDKManager.ClearLocalSession();
            SceneManager.LoadScene(loginSceneName);
            Debug.Log("Logged Out");

            //remove the values of scriptableObjects
            levelUnlockScriptable.ResetValues();

            //remove the ONCE newAndLoad
            PlayerPrefs.SetInt("NewAndLoad", 0);

            //remove the ONCE UploadPlayerFile
            PlayerPrefs.SetInt("uploadPlayer", 0);

            //remove the ONCE copyState
            PlayerPrefs.SetInt("copyState", 0);

            //remove the autoLogin
            PlayerPrefs.SetInt("AutoLogin", 0);

            //remove the playerName
            PlayerPrefs.SetString("PlayerName", "");

            //Enable the registerButton
            PlayerPrefs.SetInt("RegisterState", 0);

            //disable SkipStory
            PlayerPrefs.SetInt("SkipStory", 0);

            //Reset XP VALUE
            PlayerPrefs.SetInt("XpValue", 0);

            //Reset Levels
            PlayerPrefs.DeleteKey("CompletedLevels");

            //RESET GET XP VALUE
            PlayerPrefs.SetInt("CheckXp", 0);

            PlayerPrefs.Save();

            lDataScriptableObject.ResetData();

            //Clear hashset;
            progress.completedLevels.Clear();

            progressData.ResetProgress();
        });
    }
    public void QuitApp()
    {
        progressData.SaveToLocalFile();
        Application.Quit();
        Debug.Log("Application Successfully Quit");
    }

    public void CheckInternetConnection()
    {
        // First, check if there's any network connection at all
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No network connection (Wi-Fi, LAN, or mobile).");
            debuggerText.text = "No network connection";
            showErrorMessage(debuggerText.text);
            vlayoutGroup.spacing = -50f;
            offlineButton.SetActive(true);
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
            request.timeout = 3; // Time in seconds

            // Send the request
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                // Network is available but there is no internet access
                Debug.Log("Connected to a network, but no internet access (or the test URL is unreachable).");
                debuggerText.text = "Connected to a network, but no internet access";
                showErrorMessage(debuggerText.text);
                vlayoutGroup.spacing = -50f;
                offlineButton.SetActive(true);
            }
            else
            {
                // Network has internet access
                Debug.Log("Network has internet access.");
                if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                {
                    vlayoutGroup.spacing = -90.15f;
                    offlineButton.SetActive(false);
                    Debug.Log("Connected via mobile data and has internet access.");
                    debuggerText.text = "Connected via mobile data and has internet access.";
                }
                else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    vlayoutGroup.spacing = -90.15f;
                    offlineButton.SetActive(false);
                    Debug.Log("Connected via Wi-Fi/LAN and has internet access.");
                    debuggerText.text = "Connected via Wi-Fi/LAN and has internet access.";
                }
            }
        }
    }
}
