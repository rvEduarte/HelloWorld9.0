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
