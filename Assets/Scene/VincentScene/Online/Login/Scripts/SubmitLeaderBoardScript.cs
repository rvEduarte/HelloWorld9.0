using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitLeaderBoardScript : MonoBehaviour
{
    public static string leaderboardKey ="BegginerLevel1";
    public TMP_InputField inputscore;

    public void GameManagerLevel1()
    {
        leaderboardKey = "BegginerLevel1";
    }

    public void GameManagerLevel2() 
    {
        leaderboardKey = "BegginerLevel2";
    }
    public void titeSubmit(int scoreToSubmit, float timeTaken, float accuracy)
    {

        PlayerPrefs.SetFloat("tite1", timeTaken);
        PlayerPrefs.SetFloat("tite", accuracy);
        PlayerPrefs.Save();
        Submit(scoreToSubmit);

    }
    public static void Submit(int scoreToSubmit)
    {
        //the member id is set when the user logs in or uses guest login, if they have not done either of those then this will be empty and the request will fail.
        // we set it throughout WhiteLabelManager.cs, you can cmd/ctrl + f and look for PlayerPrefs.SetString("LLplayerId"
        string playerId = PlayerPrefs.GetString("LLplayerId");
        //string metadata = "timeTaken: " + PlayerPrefs.GetFloat("tite1") +" Accuracy: "+ PlayerPrefs.GetFloat("tite");

        // Create metadata object
        PlayerMetaData1 metadataObject = new PlayerMetaData1
        {
            timeTaken = PlayerPrefs.GetFloat("tite1"),
            accuracy = PlayerPrefs.GetFloat("tite")
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
