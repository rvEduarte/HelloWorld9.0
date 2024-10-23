using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoardOfflineScript : MonoBehaviour
{
    public LeaderboardPersistentDataScriptable leaderboardData;
    public LeaderboardOfflineScriptable leaderboardOfflineScriptable;

    private Dictionary<int, LevelData> levelDataDictionary;

    public void Start()
    {
        leaderboardData.LoadData();

        levelDataDictionary = new Dictionary<int, LevelData>
        {
            { 1, new LevelData(
                leaderboardData.levelData[0].totalScore, // Access totalScore correctly
                leaderboardData.levelData[0].timePhase1,
                leaderboardData.levelData[0].timePhase2,
                leaderboardData.levelData[0].timePhase3,
                leaderboardData.levelData[0].exerciseAccuracyPhase3,
                leaderboardData.levelData[0].quizAccuracyPhase3) },
            { 2, new LevelData(
                leaderboardData.levelData[1].totalScore, // Access totalScore correctly
                leaderboardData.levelData[1].timePhase1,
                leaderboardData.levelData[1].timePhase2,
                leaderboardData.levelData[1].timePhase3,
                leaderboardData.levelData[1].exerciseAccuracyPhase3,
                leaderboardData.levelData[1].quizAccuracyPhase3) },
            { 3, new LevelData(
                leaderboardData.levelData[2].totalScore,
                leaderboardData.levelData[2].timePhase1,
                leaderboardData.levelData[2].timePhase2,
                leaderboardData.levelData[2].timePhase3,
                leaderboardData.levelData[2].exerciseAccuracyPhase3,
                leaderboardData.levelData[2].quizAccuracyPhase3) },
            { 4, new LevelData(
                leaderboardData.levelData[3].totalScore,
                leaderboardData.levelData[3].timePhase1,
                leaderboardData.levelData[3].timePhase2,
                leaderboardData.levelData[3].timePhase3,
                leaderboardData.levelData[3].exerciseAccuracyPhase3,
                leaderboardData.levelData[3].quizAccuracyPhase3) }
            };
        }
    public void PassValue(int level)
    {
        if (levelDataDictionary.TryGetValue(level, out LevelData levelData))
        {
            leaderboardOfflineScriptable.TotalScore = levelData.Score;
            leaderboardOfflineScriptable.timePhase1 = levelData.TimePhase1;
            leaderboardOfflineScriptable.timePhase2 = levelData.TimePhase2;
            leaderboardOfflineScriptable.timePhase3 = levelData.TimePhase3;
            leaderboardOfflineScriptable.exerciseAccuracyPhase3 = levelData.AccuracyExercisePhase3;
            leaderboardOfflineScriptable.quizAccuracyPhase3 = levelData.AccuracyQuizPhase3;

            Debug.Log($"Passed Level Data for Level {level} to Leaderboard.");
        }
        else
        {
            Debug.LogWarning($"No data found for Level {level}.");
        }
    }

    public void GetData(int level)
    {
        if (level == 1)
        {
            PassValue(level);
            PlayerPrefs.SetString("leaderboardKey", "BeginnerLevel1");
            PlayerPrefs.SetString("level", "Beginner Level 1");
            PlayerPrefs.Save();

            SceneManager.LoadScene("Offline_C#RankingBeginner");
        }
        else if (level == 2)
        {
            PassValue(level);
            PlayerPrefs.SetString("leaderboardKey", "BeginnerLevel2");
            PlayerPrefs.SetString("level", "Beginner Level 2");
            PlayerPrefs.Save();

            SceneManager.LoadScene("Offline_C#RankingBeginner");
        }
        else if (level == 3)
        {
            PassValue(level);
            PlayerPrefs.SetString("leaderboardKey", "IntermediateLevel1");
            PlayerPrefs.SetString("level", "Intermediate Level 1");
            PlayerPrefs.Save();

            SceneManager.LoadScene("Offline_C#RankingBeginner");
        }
        else if (level == 4)
        {
            PassValue(level);
            PlayerPrefs.SetString("leaderboardKey", "IntermediateLevel2");
            PlayerPrefs.SetString("level", "Intermediate Level 2");
            PlayerPrefs.Save();

            SceneManager.LoadScene("Offline_C#RankingBeginner");
        }
        //ADD MORE LEVELS
    }
}
