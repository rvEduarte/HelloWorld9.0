using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "OfflineScoreScriptableObject", menuName = "ScriptableObjects/OfflineScore")]

public class OfflineScriptableObject : ScriptableObject
{
    public List<LevelData2> levelData = new List<LevelData2>();

    private string filePath;

    private void OnEnable()
    {
        filePath = Path.Combine(Application.persistentDataPath, "offlineScoreData.json");

        // Initialize with default data if the list is empty
        if (levelData.Count == 0)
        {
            // Add some default level data (you can change the number of levels)
            for (int i = 0; i < 2; i++)
            {
                levelData.Add(new LevelData2());
            }
        }
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Data saved to " + filePath);
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
            Debug.Log("Data loaded from " + filePath);
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }

    public void ResetData()
    {
        foreach (var level in levelData)
        {
            level.timePhase1 = "00:00";
            level.timePhase2 = "00:00";
            level.timePhase3 = "00:00";
            level.exerciseAccuracyPhase2 = 0f;
            level.exerciseAccuracyPhase3 = 0f;
            level.quizAccuracyPhase3 = 0f;
            level.totalScore = 0;
        }

        SaveData();
        Debug.Log("Offline data reset to defaults and saved.");
    }
}

[Serializable]
public class LevelData2
{
    public string timePhase1 = "00:00";
    public string timePhase2 = "00:00";
    public string timePhase3 = "00:00";
    public float exerciseAccuracyPhase2;
    public float exerciseAccuracyPhase3;
    public float quizAccuracyPhase3;
    public int totalScore;
}
