using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoardOfflineScript : MonoBehaviour
{
    public OfflineScriptableObject offlineScriptableObject;
    public SubmitLeaderBoardScript submitLead;

    private Dictionary<int, LevelData> levelDataDictionary;

    public void Start()
    {
        offlineScriptableObject.LoadData();
        // Initialize the dictionary with level-specific data
        levelDataDictionary = new Dictionary<int, LevelData>
        {
            { 1, new LevelData(
                offlineScriptableObject.TotalScore,
                offlineScriptableObject.timePhase1,
                offlineScriptableObject.timePhase2,
                offlineScriptableObject.timePhase3,
                offlineScriptableObject.exerciseAccuracyPhase3,
                offlineScriptableObject.quizAccuracyPhase3) },
            { 2, new LevelData(
                offlineScriptableObject.lvl2_TotalScore,
                offlineScriptableObject.lvl2_timePhase1,
                offlineScriptableObject.lvl2_timePhase2,
                offlineScriptableObject.lvl2_timePhase3,
                offlineScriptableObject.lvl2_exerciseAccuracyPhase3,
                offlineScriptableObject.lvl2_quizAccuracyPhase3) }
            // Add more levels soon
        };
    }

    public void GetData(int level)
    {
        if (level == 1)
        {
            //OffLineSubmitScore(level);
            PlayerPrefs.SetString("leaderboardKey", "BeginnerLevel1");
            PlayerPrefs.SetString("level", "Beginner Level 1");
            PlayerPrefs.Save();

            SceneManager.LoadScene("C#RankingBeginner");
        }
        else if (level == 2)
        {
            //OffLineSubmitScore(level);
            PlayerPrefs.SetString("leaderboardKey", "BeginnerLevel2");
            PlayerPrefs.SetString("level", "Beginner Level 2");
            PlayerPrefs.Save();

            SceneManager.LoadScene("C#RankingBeginner");
        }
        //ADD MORE LEVELS
    }
}
