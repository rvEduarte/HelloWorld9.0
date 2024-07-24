using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitLeaderBoardScript : MonoBehaviour
{
    public LeaderBoardScriptableObject leaderboardData;
    public static string leaderboardKey;
    //public TMP_InputField inputscore;

    private void Start()
    {
        PlayerPrefs.DeleteKey("tite1");
        PlayerPrefs.DeleteKey("tite2");
        PlayerPrefs.DeleteKey("tite3");
        PlayerPrefs.DeleteKey("accuracyExercisePh2");
        PlayerPrefs.DeleteKey("accuracyExercisePh2");
        PlayerPrefs.DeleteKey("accuracyQuizPh3");
    }
    public void GameManagerLevel1()
    {
        leaderboardKey = "BegginerLevel1";
    }

    public void GameManagerLevel2() 
    {
        leaderboardKey = "BegginerLevel2";
    }

    public void titeSubmit(int scoreToSubmit, string timeTaken1, string timeTaken2, string timeTaken3, float accuracyExercisePh2, float accuracyExercisePh3, float accuracyQuizPh3)
    {
        /*PlayerPrefs.SetString("tite1", timeTaken1);
        PlayerPrefs.SetString("tite2", timeTaken2);
        PlayerPrefs.SetString("tite3", timeTaken3);
        PlayerPrefs.SetFloat("accuracyExercisePh2", accuracyExercisePh2);
        PlayerPrefs.SetFloat("accuracyExercisePh3", accuracyExercisePh3);
        PlayerPrefs.SetFloat("accuracyQuizPh3", accuracyQuizPh3);
        PlayerPrefs.Save();*/
        leaderboardData.timePh1 = timeTaken1;
        leaderboardData.timePh2 = timeTaken2;
        leaderboardData.timePh3 = timeTaken3;

        leaderboardData.exerciseAccuracyPh2 = accuracyExercisePh2;
        leaderboardData.exerciseAccuracyPh3 = accuracyExercisePh3;
        leaderboardData.quizAccuracyPh3 = accuracyQuizPh3;

        Submit(scoreToSubmit, leaderboardData);

    }
    public static void Submit(int scoreToSubmit, LeaderBoardScriptableObject leaderboardData)
    {
        //the member id is set when the user logs in or uses guest login, if they have not done either of those then this will be empty and the request will fail.
        // we set it throughout WhiteLabelManager.cs, you can cmd/ctrl + f and look for PlayerPrefs.SetString("LLplayerId"
        string playerId = PlayerPrefs.GetString("LLplayerId");

        // Create metadata object
        PlayerMetaData1 metadataObject = new PlayerMetaData1
        {
            /*timeTaken1 = PlayerPrefs.GetString("tite1"),
            timeTaken2 = PlayerPrefs.GetString("tite2"),
            timeTaken3 = PlayerPrefs.GetString("tite3"),
            accuracyExercisePh2 = PlayerPrefs.GetFloat("accuracyExercisePh2"),
            accuracyExercisePh3 = PlayerPrefs.GetFloat("accuracyExercisePh3"),
            accuracyQuizPh3 = PlayerPrefs.GetFloat("accuracyQuizPh3")*/
            timeTaken1 = leaderboardData.timePh1,
            timeTaken2 = leaderboardData.timePh2,
            timeTaken3 = leaderboardData.timePh3,

            accuracyExercisePh2 = leaderboardData.exerciseAccuracyPh2,
            accuracyExercisePh3 = leaderboardData.exerciseAccuracyPh3,
            accuracyQuizPh3 = leaderboardData.quizAccuracyPh3,

        };
        // Serialize metadata to JSON
        string metadata = JsonUtility.ToJson(metadataObject);

        LootLockerSDKManager.SubmitScore(playerId, scoreToSubmit, leaderboardKey, metadata, (response) =>
        {
            if (response.success)
            {
                Debug.Log("SubmitLeaderboardScore successful" + metadata);
            }
            else
            {
                Debug.LogError("SubmitLeaderboardScore failed");
                Debug.LogError("Error: " + response.Error);
            }
        });
    }

    public static void GetPlayerHighScore()
    {
        string playerIdString = PlayerPrefs.GetString("LLplayerId");

        // Convert the playerId string to an integer
        int playerId;
        if (!int.TryParse(playerIdString, out playerId))
        {
            Debug.LogError("Failed to parse playerId from PlayerPrefs.");
            return;
        }
        LootLockerSDKManager.GetMemberRank(leaderboardKey, playerId, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                Debug.Log("Player Score: " + response.score);
                PlayerPrefs.SetInt("highScore", response.score);
            }
            else
            {
                Debug.Log("failed to get highscore: " + response.Error);
            }
        });
    }

}
