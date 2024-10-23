using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUserUID : MonoBehaviour
{
    public OfflineScriptableObject offlineScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        string leaderboardKey = "BeginnerLevel1";
        string memberID = PlayerPrefs.GetString("LLplayerId");

        LootLockerSDKManager.GetMemberRank(leaderboardKey, memberID, (response) =>
        {
            if (!response.success)
            {
                Debug.LogError("Could not get the entry!");
                Debug.Log(response.errorData.ToString());
                return;
            }
            else
            {
                Debug.Log("Successfully got entry!");

                // Parse the metadata JSON
                string metadataJson = response.metadata;

                if (!string.IsNullOrEmpty(metadataJson))
                {
                    try
                    {
                        // Deserialize the metadata JSON
                        Metadata2 metadata = JsonUtility.FromJson<Metadata2>(metadataJson);

                        // Update your ScriptableObject with the loaded data
                        offlineScriptableObject.timePhase1 = metadata.timeTaken1;
                        offlineScriptableObject.timePhase2 = metadata.timeTaken2;
                        offlineScriptableObject.timePhase3 = metadata.timeTaken3;
                        offlineScriptableObject.exerciseAccuracyPhase3 = metadata.accuracyExercisePh3;
                        offlineScriptableObject.quizAccuracyPhase3 = metadata.accuracyQuizPh3;
                        offlineScriptableObject.TotalScore = response.score;

                        // Save the updated data to the local file
                        offlineScriptableObject.SaveData();

                        Debug.Log("Data successfully loaded into ScriptableObject and saved.");
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
            }
        });
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
}
