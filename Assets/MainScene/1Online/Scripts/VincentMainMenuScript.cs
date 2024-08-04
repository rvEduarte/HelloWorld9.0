using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VincentMainMenuScript : MonoBehaviour
{
    public LevelUnlockScriptable levelUnlockScriptable;

    private string loginSceneName = "Login";
    private string leaderboardSceneName = "Leaderboard";
    private string playGameSceneName = "TRIALPLAY";

    [Header("Player Name")]
    public TextMeshProUGUI playerNameText;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;
    //public Animator errorScreenAnimator;

    public void Start()
    {

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
            }
        });

        SubmitLeaderBoardScript.GetPlayerHighScore();

    }

    public void onNoPLayerId()
    {
        showErrorMessage("You're not logged in");
        //wait 3 seconds and then logout 
        Invoke("goBackToLogin", 3);
    }

    /*public void goBackToLogin()
    {
        SceneManager.LoadScene(loginSceneName);
    }*/

    public void goToLeaderboard()
    {
        SceneManager.LoadScene(leaderboardSceneName);
    }

    public void startGame()
    {
        SceneManager.LoadScene(playGameSceneName);
    }

    // Show an error message on the screen
    public void showErrorMessage(string message, int showTime = 3)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();
        //errorScreenAnimator.SetTrigger("Show");
        errorPanel.SetActive(true);
        //wait for 3 seconds and hide the error panel
        Invoke("hideErrorMessage", showTime);
    }

    private void hideErrorMessage()
    {
        errorPanel.SetActive(false);
        //errorScreenAnimator.SetTrigger("Hide");
    }

    public void logout()
    {
        //remove the values of scriptableObjects
        levelUnlockScriptable.ResetValues();

        //remove the auto remember
        PlayerPrefs.SetInt("rememberMe", 0);

        //remove the ONCE newAndLoad
        PlayerPrefs.SetInt("NewAndLoad", 0);

        //remove the ONCE UploadPlayerFile
        PlayerPrefs.SetInt("Clicked", 0);

        //remove the ONCE copyState
        PlayerPrefs.SetInt("copyState", 0);

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
        });

    }
}
