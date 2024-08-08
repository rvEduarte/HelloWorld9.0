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

    public void BegginnerGetDataLevel1()
    {
        leaderboardLevelText.text = "Level1 Ranking";

        leaderboardGamerText.text = "PLAYER NAME";
        leaderboardScoreText.text = "SCORE";

        timeTaken1.text = "elapsed time ph1";
        timeTaken2.text = "elapsed time ph2";
        timeTaken3.text = "elapsed time ph3";

        exerciseAccuracyPh2.text = "Challenge Exercise Accuracy PH2";
        exerciseAccuracyPh3.text = "Challenge Exercise Accuracy PH3";

        quizAccuracyPh3.text = "Quiz Accuracy PH3";

        //add the score to the text
        leaderboardGamerText.text += "\n" + PlayerPrefs.GetString("PlayerName");
        leaderboardScoreText.text += "\n" + leaderboardstat.TotalScore;

        timeTaken1.text += "\n" + leaderboardstat.timePhase1;
        timeTaken2.text += "\n" + leaderboardstat.timePhase2;
        timeTaken3.text += "\n" + leaderboardstat.timePhase3;

        exerciseAccuracyPh2.text += "\n" + leaderboardstat.exerciseAccuracyPhase2 + "%";
        exerciseAccuracyPh3.text += "\n" + leaderboardstat.exerciseAccuracyPhase3 + "%";

        quizAccuracyPh3.text += "\n" + leaderboardstat.lvl2_quizAccuracyPhase3 + "%";
    }
}
