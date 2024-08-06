using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineMainMenu : MonoBehaviour
{
    [SerializeField] public bool clicked; //FALSE
    public LevelUnlockScriptable levelUnlockScriptable;

    private string loginSceneName = "Login";

    [Header("Player Name")]
    public TextMeshProUGUI playerNameText;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

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

        SubmitLeaderBoardScript.GetPlayerHighScore();
    }
    public void PlayButtonOnline(string name)
    {
        if (!clicked)
        {
            if (LootlockerSceneProgress.Instance != null)
            {
                LootlockerSceneProgress.Instance.UploadFileFromPath(levelUnlockScriptable);
                Debug.Log(levelUnlockScriptable);
            }
            else
            {
                Debug.LogError("errorUploadFile");
            }

            // Mark the action as performed and save the state
            PlayerPrefs.SetInt("uploadPlayer", 1); // 1 means true
            PlayerPrefs.Save(); // Make sure changes are saved to disk

            clicked = true;

            SceneManager.LoadScene(name);
            Debug.Log("ISA LANG");
        }

        SceneManager.LoadScene(name);
        Debug.Log("YES");
    }
    public void GotoScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
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
        });
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application Successfully Quit");
    }
}
