using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitLeaderBoardScript : MonoBehaviour
{
    public LeaderBoardScriptableObject leaderboardData;
    public static string leaderboardKey;

    public void GameManagerLevel(int level)
    {
        leaderboardKey = level switch
        {
            1 => "BeginnerLevel1",
            2 => "BeginnerLevel2",
            // Add more levels soon
            _ => throw new System.ArgumentException("Invalid level number")
        };
    }

    public void SubmitData(int scoreToSubmit, string timeTaken1, string timeTaken2, string timeTaken3, float accuracyExercisePh3, float accuracyQuizPh3)
    {
        leaderboardData.timePh1 = timeTaken1;
        leaderboardData.timePh2 = timeTaken2;
        leaderboardData.timePh3 = timeTaken3;

        leaderboardData.exerciseAccuracyPh3 = accuracyExercisePh3;
        leaderboardData.quizAccuracyPh3 = accuracyQuizPh3;

        Submit(scoreToSubmit, leaderboardData);

    }
    public static void Submit(int scoreToSubmit, LeaderBoardScriptableObject leaderboardData)
    {
        string playerId = PlayerPrefs.GetString("LLplayerId");

        // Create metadata object
        PlayerMetaData1 metadataObject = new PlayerMetaData1
        {
            timeTaken1 = leaderboardData.timePh1,
            timeTaken2 = leaderboardData.timePh2,
            timeTaken3 = leaderboardData.timePh3,

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

                //HIGHSCORE
                GetPlayerHighScore();
            }
            else
            {
                Debug.LogError("SubmitLeaderboardScore failed");
                Debug.LogError("Error: " + response.errorData.ToString());
            }
        });
    }

    public static void GetPlayerHighScore()
    {
        string playerId = PlayerPrefs.GetString("LLplayerId");

        LootLockerSDKManager.GetMemberRank(leaderboardKey, playerId, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                Debug.Log("Player Score: " + response.score);
                PlayerPrefs.SetInt("highScore", response.score);
                Debug.Log(PlayerPrefs.GetInt("highScore"));
            }
            else
            {
                Debug.Log("failed to get highscore: " + response.errorData.ToString());
            }
        });
    }

}
