using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingValue : MonoBehaviour
{
    public LeaderboardPersistentDataScriptable leaderboardData;
    public TemporaryScriptable temporaryData;
    public List<string> leaderboardKeys = new List<string> { "BeginnerLevel1", "BeginnerLevel2", "IntermediateLevel1", "IntermediateLevel2" }; // List of leaderboard keys

    private void Start()
    {
        leaderboardData.LoadData();
        //offlineScriptableObject.LoadData();
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
            Debug.Log("ONE");
            temporaryData.levelData[0] = level1Data; // Update first level data
            CheckValue(leaderboardKey);
        }
        else if (leaderboardKey == "BeginnerLevel2")
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
            Debug.Log("ONE");
            temporaryData.levelData[1] = level1Data; // Update first level data
            CheckValue(leaderboardKey);
        }
        else if (leaderboardKey == "IntermediateLevel1")
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
            Debug.Log("ONE");
            temporaryData.levelData[2] = level1Data; // Update first level data
            CheckValue(leaderboardKey);
        }
        else if (leaderboardKey == "IntermediateLevel2")
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
            Debug.Log("ONE");
            temporaryData.levelData[3] = level1Data; // Update first level data
            CheckValue(leaderboardKey);
        }
    }
    private void CheckValue(string leaderBoardKey)
    {
        if (leaderBoardKey == "BeginnerLevel1")
        {
            if(leaderboardData.levelData[0].totalScore > temporaryData.levelData[0].totalScore)
            {
                Debug.Log("NEW VALUE !!");
                leaderboardData.SaveData();
            }
            else
            {
                Debug.Log(" < ! > NEW VALUE !!");
                leaderboardData.levelData[0] = temporaryData.levelData[0];
                leaderboardData.SaveData();
            }
        }
        else if (leaderBoardKey == "BeginnerLevel2")
        {
            if (leaderboardData.levelData[1].totalScore > temporaryData.levelData[1].totalScore)
            {
                Debug.Log("NEW VALUE !!");
                leaderboardData.SaveData();
            }
            else
            {
                Debug.Log(" < ! > NEW VALUE !!");
                leaderboardData.levelData[1] = temporaryData.levelData[1];
                leaderboardData.SaveData();
            }
        }
        else if (leaderBoardKey == "IntermediateLevel1")
        {
            if (leaderboardData.levelData[2].totalScore > temporaryData.levelData[2].totalScore)
            {
                Debug.Log("NEW VALUE !!");
                leaderboardData.SaveData();
            }
            else
            {
                Debug.Log(" < ! > NEW VALUE !!");
                leaderboardData.levelData[2] = temporaryData.levelData[2];
                leaderboardData.SaveData();
            }
        }
        else if (leaderBoardKey == "IntermediateLevel2")
        {
            if (leaderboardData.levelData[3].totalScore > temporaryData.levelData[3].totalScore)
            {
                Debug.Log("NEW VALUE !!");
                leaderboardData.SaveData();
            }
            else
            {
                Debug.Log(" < ! > NEW VALUE !!");
                leaderboardData.levelData[3] = temporaryData.levelData[3];
                leaderboardData.SaveData();
            }
        }
    }
}
