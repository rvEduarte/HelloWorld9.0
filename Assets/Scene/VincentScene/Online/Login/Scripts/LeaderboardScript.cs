using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardScript : MonoBehaviour
{
    int leaderboardKey;
    private string mainMenuSceneName = "Scene 1";

    [Header("Leaderboard Text")]
    public TextMeshProUGUI leaderboardLevelText;
    public TextMeshProUGUI leaderboardGamerText;
    public TextMeshProUGUI leaderboardScoreText;
    //public TextMeshProUGUI leaderboardTimeText;
    public TextMeshProUGUI leaderboardAccuracyText;

    [Header("Leaderboard TimeTaken Text")]
    public TextMeshProUGUI timeTaken1;
    public TextMeshProUGUI timeTaken2;
    public TextMeshProUGUI timeTaken3;

    [Header("Leaderboard Exercise Accuracy Text")]
    public TextMeshProUGUI exerciseAccuracyPh2;
    public TextMeshProUGUI exerciseAccuracyPh3;

    [Header("Leaderboard Quiz Accuracy Text")]
    public TextMeshProUGUI quizAccuracyPh3;


    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;
    public Animator errorScreenAnimator;

    public void Start()
    {
       //LevelGetData();
    }
    public void LevelGetData()
    {
        if (leaderboardGamerText == null || leaderboardScoreText == null || leaderboardAccuracyText == null || timeTaken1 == null || timeTaken2 == null || timeTaken3 == null)
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
                leaderboardAccuracyText.text = "ACCURACY";

                timeTaken1.text = "elapsed time ph1";
                timeTaken2.text = "elapsed time ph2";
                timeTaken3.text = "elapsed time ph3";

                exerciseAccuracyPh2.text = "Challenge Exercise Accuracy PH2";
                exerciseAccuracyPh3.text = "Challenge Exercise Accuracy PH3";

                quizAccuracyPh3.text = "Quiz Accuracy PH3";

                //for each item 
                foreach (LootLockerLeaderboardMember score in response.items)
                {
                    //add the score to the text
                    leaderboardGamerText.text += "\n" + score.rank + ". " + score.player.name;
                    leaderboardScoreText.text += "\n" + score.score.ToString();

                    // Parse metadata
                    PlayerMetaData1 metadata = JsonUtility.FromJson<PlayerMetaData1>(score.metadata);
                    timeTaken1.text += "\n" + metadata.timeTaken1;
                    timeTaken2.text += "\n" + metadata.timeTaken2;
                    timeTaken3.text += "\n" + metadata.timeTaken3;

                    exerciseAccuracyPh2.text += "\n" + metadata.accuracyExercisePh2 + "%";
                    exerciseAccuracyPh3.text += "\n" + metadata.accuracyExercisePh3 + "%";

                    quizAccuracyPh3.text += "\n" + metadata.accuracyQuizPh3 + "%";

                    //leaderboardTimeText.text += "\n" + metadata.timeTaken.ToString();
                    //leaderboardAccuracyText.text += "\n" + (metadata.accuracy * 100).ToString("F2") + "%";
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
        //errorScreenAnimator.SetTrigger("Show");
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

    public void BegginnerGetDataLevel1()
    {
        leaderboardKey = 23450;
        leaderboardLevelText.text = "Level1 Ranking";
        LevelGetData();
    }

    public void BegginnerGetDataLevel2()
    {
        leaderboardKey = 23455;
        leaderboardLevelText.text = "Level2 Ranking";
        LevelGetData();
    }
}
