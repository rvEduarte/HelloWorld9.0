using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class OfflineLeaderboardScript : MonoBehaviour
{
    public OfflineScriptableObject leaderboardstat;

    [Header("Leaderboard Text")]
    public TextMeshProUGUI leaderboardLevelText;
    public TextMeshProUGUI leaderboardGamerText;
    public TextMeshProUGUI leaderboardScoreText;

    [Header("Leaderboard TimeTaken Text")]
    public TextMeshProUGUI timeTaken1;
    public TextMeshProUGUI timeTaken2;
    public TextMeshProUGUI timeTaken3;

    [Header("Leaderboard Exercise Accuracy Text")]
    public TextMeshProUGUI exerciseAccuracyPh2;
    public TextMeshProUGUI exerciseAccuracyPh3;

    [Header("Leaderboard Quiz Accuracy Text")]
    public TextMeshProUGUI quizAccuracyPh3;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void BackButtonPressed(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void UpdateLeaderboardUI(string levelTitle, int totalScore, string timePh1, string timePh2, string timePh3, float accExPh2, float accExPh3, float accQuizPh3)
    {
        leaderboardLevelText.text = levelTitle;

        leaderboardGamerText.text = "PLAYER NAME";
        leaderboardScoreText.text = "SCORE";

        timeTaken1.text = "elapsed time ph1";
        timeTaken2.text = "elapsed time ph2";
        timeTaken3.text = "elapsed time ph3";

        exerciseAccuracyPh2.text = "Challenge Exercise Accuracy PH2";
        exerciseAccuracyPh3.text = "Challenge Exercise Accuracy PH3";

        quizAccuracyPh3.text = "Quiz Accuracy PH3";

        // Add the score to the text
        leaderboardGamerText.text += "\n" + PlayerPrefs.GetString("PlayerName");
        leaderboardScoreText.text += "\n" + totalScore;

        timeTaken1.text += "\n" + timePh1;
        timeTaken2.text += "\n" + timePh2;
        timeTaken3.text += "\n" + timePh3;

        exerciseAccuracyPh2.text += "\n" + accExPh2 + "%";
        exerciseAccuracyPh3.text += "\n" + accExPh3 + "%";
        quizAccuracyPh3.text += "\n" + accQuizPh3 + "%";
    }

    public void BegginnerGetDataLevel1()
    {
        UpdateLeaderboardUI(
            "Level1 Ranking",
            leaderboardstat.TotalScore,
            leaderboardstat.timePhase1,
            leaderboardstat.timePhase2,
            leaderboardstat.timePhase3,
            leaderboardstat.exerciseAccuracyPhase2,
            leaderboardstat.exerciseAccuracyPhase3,
            leaderboardstat.quizAccuracyPhase3
        );
    }

    public void BegginnerGetDataLevel2()
    {
        UpdateLeaderboardUI(
            "Level2 Ranking",
            leaderboardstat.lvl2_TotalScore,
            leaderboardstat.lvl2_timePhase1,
            leaderboardstat.lvl2_timePhase2,
            leaderboardstat.lvl2_timePhase3,
            leaderboardstat.lvl2_exerciseAccuracyPhase2,
            leaderboardstat.lvl2_exerciseAccuracyPhase3,
            leaderboardstat.lvl2_quizAccuracyPhase3
        );
    }
}
