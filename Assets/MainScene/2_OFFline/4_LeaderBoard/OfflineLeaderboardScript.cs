using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineLeaderboardScript : MonoBehaviour
{
    [Header("Leaderboard Text")]
    public TextMeshProUGUI leaderboardLevelText;
    public TextMeshProUGUI leaderboardGamerText;
    public TextMeshProUGUI leaderboardScoreText;
    public TextMeshProUGUI leaderboardAccuracyText;

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
        //leaderboardKey = "BegginerLevel1";
        leaderboardLevelText.text = "Level1 Ranking";
        //LevelGetData();
    }
}
