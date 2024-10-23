using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardScript : MonoBehaviour
{
    #region Variables

    public LeaderboardPersistentDataScriptable leaderboardData;
    public SubmitLeaderBoardScript submitLead;

    private Dictionary<int, LevelData> levelDataDictionary;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

    #endregion

    public void Start()
    {
        leaderboardData.LoadData();
        // Initialize the dictionary with level-specific data
        levelDataDictionary = new Dictionary<int, LevelData>
        {
            { 1, new LevelData(
                leaderboardData.levelData[0].totalScore,
                leaderboardData.levelData[0].timePhase1,
                leaderboardData.levelData[0].timePhase2,
                leaderboardData.levelData[0].timePhase3,
                leaderboardData.levelData[0].exerciseAccuracyPhase3,
                leaderboardData.levelData[0].quizAccuracyPhase3) },
            { 2, new LevelData(
                leaderboardData.levelData[1].totalScore,
                leaderboardData.levelData[1].timePhase1,
                leaderboardData.levelData[1].timePhase2,
                leaderboardData.levelData[1].timePhase3,
                leaderboardData.levelData[1].exerciseAccuracyPhase3,
                leaderboardData.levelData[1].quizAccuracyPhase3) }
            // Add more levels as needed
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
        // Add more levels as needed
    }

    #endregion
}

public class LevelData
{
    public int Score { get; }
    public string TimePhase1 { get; }
    public string TimePhase2 { get; }
    public string TimePhase3 { get; }
    public float AccuracyExercisePhase3 { get; }
    public float AccuracyQuizPhase3 { get; }

    public LevelData(int scoreToSubmit, string timePhase1, string timePhase2, string timePhase3, float accuracyExercisePhase3, float accuracyQuizPhase3)
    {
        Score = scoreToSubmit;
        TimePhase1 = timePhase1;
        TimePhase2 = timePhase2;
        TimePhase3 = timePhase3;
        AccuracyExercisePhase3 = accuracyExercisePhase3;
        AccuracyQuizPhase3 = accuracyQuizPhase3;
    }
}
