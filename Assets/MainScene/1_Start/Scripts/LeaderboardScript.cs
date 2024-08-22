using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardScript : MonoBehaviour
{
    #region Variables

    public OfflineScriptableObject offlineScriptableObject;
    public SubmitLeaderBoardScript submitLead;

    private Dictionary<int, LevelData> levelDataDictionary;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

    #endregion

    public void Start()
    {
        // Initialize the dictionary with level-specific data
        levelDataDictionary = new Dictionary<int, LevelData>
        {
            { 1, new LevelData(
                offlineScriptableObject.TotalScore,
                offlineScriptableObject.timePhase1,
                offlineScriptableObject.timePhase2,
                offlineScriptableObject.timePhase3,
                offlineScriptableObject.exerciseAccuracyPhase2,
                offlineScriptableObject.exerciseAccuracyPhase3,
                offlineScriptableObject.quizAccuracyPhase3) },
            { 2, new LevelData(
                offlineScriptableObject.lvl2_TotalScore,
                offlineScriptableObject.lvl2_timePhase1,
                offlineScriptableObject.lvl2_timePhase2,
                offlineScriptableObject.lvl2_timePhase3,
                offlineScriptableObject.lvl2_exerciseAccuracyPhase2,
                offlineScriptableObject.lvl2_exerciseAccuracyPhase3,
                offlineScriptableObject.lvl2_quizAccuracyPhase3) }
            // Add more levels soon
        };
    }
    public void BackButtonPressed(string name)
    {
        SceneManager.LoadScene(name);
    }

    #region SubmitLeaderBoardData

    public void OffLineSubmitScore(int level)
    {
        if (levelDataDictionary.TryGetValue(level, out LevelData levelData))
        {
            // Validate data before submitting
            if (IsValidData(levelData))
            {
                Debug.Log(level);
                submitLead.GameManagerLevel(level);
                submitLead.SubmitData(
                    levelData.Score,
                    levelData.TimePhase1,
                    levelData.TimePhase2,
                    levelData.TimePhase3,
                    levelData.AccuracyExercisePhase2,
                    levelData.AccuracyExercisePhase3,
                    levelData.AccuracyQuizPhase3);

                Debug.Log($"Submitted score for level {level}");
            }
            else
            {
                Debug.LogWarning("One or more required values are null or invalid. Submission aborted.");
            }
        }
        else
        {
            Debug.LogError("Invalid level specified");
        }
    }
    private bool IsValidData(LevelData data)
    {
        return data.Score != 0 &&
               !string.IsNullOrEmpty(data.TimePhase1) &&
               !string.IsNullOrEmpty(data.TimePhase2) &&
               !string.IsNullOrEmpty(data.TimePhase3) &&
               data.AccuracyExercisePhase2 != 0 &&
               data.AccuracyExercisePhase3 != 0 &&
               data.AccuracyQuizPhase3 != 0;
    }

    #endregion


    #region GetLeaderBoardData

    public void GetData(int level)
    {
        if (level == 1)
        {
            OffLineSubmitScore(level);
            PlayerPrefs.SetString("leaderboardKey", "BeginnerLevel1");
            PlayerPrefs.SetString("level", "Beginner Level 1");
            PlayerPrefs.Save();

            SceneManager.LoadScene("C#RankingBeginner");
        }
        else if (level == 2)
        {
            OffLineSubmitScore(level);
            PlayerPrefs.SetString("leaderboardKey", "BeginnerLevel2");
            PlayerPrefs.SetString("level", "Beginner Level 2");
            PlayerPrefs.Save();

            SceneManager.LoadScene("C#RankingBeginner");
        }
        //ADD MORE LEVELS
    }

    #endregion


    /*#region ErroMessageHandler

    // Show an error message on the screen
    public void showErrorMessage(string message, int showTime = 3)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();
        //wait for 3 seconds and hide the error panel
        Invoke("hideErrorMessage", showTime);
    }

    private void hideErrorMessage()
    {
        errorPanel.SetActive(false);
    }

    private string extractMessageFromLootLockerError(string rawError)
    {
        // Find the start index of the message
        int startIndex = rawError.IndexOf("\"") + 1; // Skip the first quote
        if (startIndex == 0)
        {
            return "Message not found"; // Handle case where the first quote is not found
        }

        // Find the end index of the message
        int endIndex = rawError.IndexOf("\"", startIndex); // Find the closing quote
        if (endIndex == -1)
        {
            return "Message not properly terminated"; // Handle case where the message is not properly terminated
        }

        // Extract the message
        string message = rawError.Substring(startIndex, endIndex - startIndex);

        return message;
    }

    #endregion*/

    
}

public class LevelData
{
    public int Score { get; }
    public string TimePhase1 { get; }
    public string TimePhase2 { get; }
    public string TimePhase3 { get; }
    public float AccuracyExercisePhase2 { get; }
    public float AccuracyExercisePhase3 { get; }
    public float AccuracyQuizPhase3 { get; }

    public LevelData(int scoreToSubmit, string timePhase1, string timePhase2, string timePhase3, float accuracyExercisePhase2, float accuracyExercisePhase3, float accuracyQuizPhase3)
    {
        Score = scoreToSubmit;
        TimePhase1 = timePhase1;
        TimePhase2 = timePhase2;
        TimePhase3 = timePhase3;
        AccuracyExercisePhase2 = accuracyExercisePhase2;
        AccuracyExercisePhase3 = accuracyExercisePhase3;
        AccuracyQuizPhase3 = accuracyQuizPhase3;
    }
}
