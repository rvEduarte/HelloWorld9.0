using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUserUID : MonoBehaviour
{
    public LeaderboardPersistentDataScriptable offlineScriptableObject;
    public List<string> leaderboardKeys = new List<string> { "BeginnerLevel1", "BeginnerLevel2" }; // List of leaderboard keys

    void Start()
    {
        offlineScriptableObject.LoadData();
        string memberID = PlayerPrefs.GetString("LLplayerId");

        foreach (string leaderboardKey in leaderboardKeys)
        {
            // For each leaderboard key, make a request
            LootLockerSDKManager.GetMemberRank(leaderboardKey, memberID, (response) =>
            {
                if (!response.success)
                {
                    Debug.LogError($"Could not get the entry for {leaderboardKey}!");
                    return;
                }
                Debug.Log($"Successfully got entry for {leaderboardKey}!");

                // Parse the metadata JSON
                string metadataJson = response.metadata;

                if (!string.IsNullOrEmpty(metadataJson))
                {
                    try
                    {
                        Metadata2 metadata = JsonUtility.FromJson<Metadata2>(metadataJson);

                        metadata.score = response.score;

                        UpdateLevelData(leaderboardKey, metadata);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Failed to parse metadata: " + e.Message);
                    }
                }
                else
                {
                    Debug.LogWarning("No metadata available.");
                }
            });
        }
    }

    // Method to update level data based on the leaderboard key
    private void UpdateLevelData(string leaderboardKey, Metadata2 metadata)
    {
        if (leaderboardKey == "BeginnerLevel1")
        {
            // Assuming you want to update level data for level 1
            LevelData2 level1Data = new LevelData2
            {
                timePhase1 = metadata.timeTaken1,
                timePhase2 = metadata.timeTaken2,
                timePhase3 = metadata.timeTaken3,
                exerciseAccuracyPhase3 = metadata.accuracyExercisePh3,
                quizAccuracyPhase3 = metadata.accuracyQuizPh3,
                totalScore = metadata.score
            };
            offlineScriptableObject.levelData[0] = level1Data; // Update first level data
        }
        else if (leaderboardKey == "BeginnerLevel2")
        {
            // Assuming you want to update level data for advanced level
            LevelData2 level2Data = new LevelData2
            {
                timePhase1 = metadata.timeTaken1,
                timePhase2 = metadata.timeTaken2,
                timePhase3 = metadata.timeTaken3,
                exerciseAccuracyPhase3 = metadata.accuracyExercisePh3,
                quizAccuracyPhase3 = metadata.accuracyQuizPh3,
                totalScore = metadata.score
            };
            offlineScriptableObject.levelData[1] = level2Data; // Update second level data
        }

        // Save the updated data to the local file
        offlineScriptableObject.SaveData();
        Debug.Log("Data successfully loaded into ScriptableObject and saved.");
    }
}

[Serializable]
public class Metadata2
{
    public string timeTaken1;
    public string timeTaken2;
    public string timeTaken3;
    public float accuracyExercisePh3;
    public float accuracyQuizPh3;
    public int score;
}
