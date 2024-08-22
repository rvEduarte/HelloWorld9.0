using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineMainMenu : MonoBehaviour
{
    public LootlockerSceneProgress progressData;

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

            progressData.ResetProgress();
        });
    }
    public void QuitApp()
    {
        progressData.SaveToLocalFile();
        Application.Quit();
        Debug.Log("Application Successfully Quit");
    }
}
