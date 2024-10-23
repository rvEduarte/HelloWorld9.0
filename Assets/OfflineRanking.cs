using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class OfflineRanking : MonoBehaviour
{
    public LeaderboardOfflineScriptable leaderboardOfflineScriptable;

    [Header("Leaderboard Text")]
    public TextMeshProUGUI leaderboardLevelText;

    public addingimage addingimage;

    string playerName;
    // Start is called before the first frame update
    void Start()
    {

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
        {
            playerName = "Guest";
        }
        else
        {
            playerName = PlayerPrefs.GetString("PlayerName");
        }

        leaderboardLevelText.text = PlayerPrefs.GetString("level");
        GetLeaderboardData();
    }
    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void GetLeaderboardData()
    {
        addingimage.BackGroundImage();
        addingimage.LevelFirstMedal();

        addingimage.PlayerName(playerName);

        addingimage.ScrollRectTransform();

        addingimage.ViewPortObject();
        addingimage.ContentObject();

        addingimage.TextScoreImage(leaderboardOfflineScriptable.TotalScore.ToString());

        addingimage.TextTimerImage(leaderboardOfflineScriptable.timePhase1);
        addingimage.TextTimerImage(leaderboardOfflineScriptable.timePhase2);
        addingimage.TextTimerImage(leaderboardOfflineScriptable.timePhase3);

        addingimage.TextExerciseImage(leaderboardOfflineScriptable.exerciseAccuracyPhase3.ToString() + "%");

        addingimage.TextQuizImage(leaderboardOfflineScriptable.quizAccuracyPhase3.ToString() + "%");
    }
}
