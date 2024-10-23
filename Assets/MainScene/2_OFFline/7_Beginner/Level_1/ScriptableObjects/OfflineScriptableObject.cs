using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "OfflineScoreScriptableObject", menuName = "ScriptableObjects/OfflineScore")]

public class OfflineScriptableObject : ScriptableObject
{
    [Header("OFFLINE DATA")]
    [Header("LEVEL 1")]
    public string timePhase1 = "00:00";
    public string timePhase2 = "00:00";
    public string timePhase3 = "00:00";
    public float exerciseAccuracyPhase2;
    public float exerciseAccuracyPhase3;
    public float quizAccuracyPhase3;
    public int TotalScore;

    [Header("")]
    [Header("LEVEL 2")]
    public string lvl2_timePhase1 = "00:00";
    public string lvl2_timePhase2 = "00:00";
    public string lvl2_timePhase3 = "00:00";
    public float lvl2_exerciseAccuracyPhase2;
    public float lvl2_exerciseAccuracyPhase3;
    public float lvl2_quizAccuracyPhase3;
    public int lvl2_TotalScore;

    public void ResetData()
    {
        // LEVEL 1
        timePhase1 = "00:00";
        timePhase2 = "00:00";
        timePhase3 = "00:00";
        exerciseAccuracyPhase2 = 0f;
        exerciseAccuracyPhase3 = 0f;
        quizAccuracyPhase3 = 0f;
        TotalScore = 0;

        // LEVEL 2
        lvl2_timePhase1 = "00:00";
        lvl2_timePhase2 = "00:00";
        lvl2_timePhase3 = "00:00";
        lvl2_exerciseAccuracyPhase2 = 0f;
        lvl2_exerciseAccuracyPhase3 = 0f;
        lvl2_quizAccuracyPhase3 = 0f;
        lvl2_TotalScore = 0;

        // Save the cleared data
        SaveData();

        Debug.Log("Offline data reset to defaults and saved.");
    }

    private string filePath;

    private void OnEnable()
    {
        // Initialize file path here
        filePath = Path.Combine(Application.persistentDataPath, "offlineScoreData.json");
    }

    // Save method
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Data saved to " + filePath);
    }

    // Load method
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
}
