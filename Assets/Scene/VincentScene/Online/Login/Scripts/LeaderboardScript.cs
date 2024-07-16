using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardScript : MonoBehaviour
{
    int leaderboardKey = 23450;
    private string mainMenuSceneName = "Scene 1";

    [Header("Leaderboard Text")]
    public TextMeshProUGUI leaderboardGamerText;
    public TextMeshProUGUI leaderboardScoreText;
    public TextMeshProUGUI leaderboardTimeText;
    public TextMeshProUGUI leaderboardAccuracyText;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;
    public Animator errorScreenAnimator;

    public void Start()
    {
        //GetLeaderboardData();
    }
    public void LevelGetData()
    {
        if (leaderboardGamerText == null || leaderboardScoreText == null || leaderboardTimeText == null || leaderboardAccuracyText)
        {
            Debug.Log("Not assigned");
        }
        else
        {
            GetLeaderboardData();
        }
    }

    public void BackButtonPressed()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void GetLeaderboardData()
    {
        //how many scores to retrieve
        int count = 10;

        LootLockerSDKManager.GetScoreListMain(leaderboardKey, count, 0, (response) =>
        {
            if (response.success)
            {
                // Leaderboard was retrieved
                Debug.Log("Leaderboard was retrieved");
                //show the leaderboard screen and populate it with the data

                leaderboardGamerText.text = "PLAYER NAME";
                leaderboardScoreText.text = "SCORE";
                leaderboardTimeText.text = "ELAPSED TIME";
                leaderboardAccuracyText.text = "ACCURACY";

                //for each item 
                foreach (LootLockerLeaderboardMember score in response.items)
                {
                    //add the score to the text
                    leaderboardGamerText.text += "\n" + score.rank + ". " + score.player.name;
                    leaderboardScoreText.text += "\n" + score.score.ToString();

                    // Parse metadata
                    PlayerMetaData1 metadata = JsonUtility.FromJson<PlayerMetaData1>(score.metadata);
                    leaderboardTimeText.text += "\n" + metadata.timeTaken.ToString();
                    leaderboardAccuracyText.text += "\n" + (metadata.accuracy * 100).ToString("F2") + "%";
                }
            }
            else
            {
                // Error
                Debug.Log(response.Error);
                if (response.Error.Contains("message"))
                {
                    showErrorMessage(extractMessageFromLootLockerError(response.Error));
                }
                else
                {
                    showErrorMessage("Error retrieving leaderboard");
                }
            }
        });
    }
    // Show an error message on the screen
    public void showErrorMessage(string message, int showTime = 3)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();
        errorScreenAnimator.SetTrigger("Show");
        //wait for 3 seconds and hide the error panel
        Invoke("hideErrorMessage", showTime);
    }

    private void hideErrorMessage()
    {
        //errorScreenAnimator.SetTrigger("Hide");
        //BackButtonAnimator.SetTrigger("Show");
        errorPanel.SetActive(false);
    }

    private string extractMessageFromLootLockerError(string rawError)
    {
        //find in the string "message":" and split the string there
        int first = rawError.IndexOf("\"message\":\"") + "\"message\":\"".Length;
        int last = rawError.LastIndexOf("\"message\":\"");
        // removes "message":" and everything before it from the string
        string str2 = rawError.Substring(first, rawError.Length - first);

        int end = str2.IndexOf("\"");
        // finds the closing " and removes everything after it from the string 
        string res = str2.Substring(0, end);
        res = res.ToUpper();
        return res;
    }
}
