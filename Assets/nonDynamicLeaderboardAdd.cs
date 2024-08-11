using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class nonDynamicLeaderboardAdd : MonoBehaviour
{
    GameObject panel1;
    GameObject panel2;
    GameObject panel3;
    GameObject panel4;
    GameObject panel5;
    GameObject panel6;
    GameObject panel7;
    GameObject panel8;
    GameObject panel9;
    GameObject panel10;

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

    public void EnableLeaderboard(int count)
    {
        if (count == 1)
        {

        }
        else if (count == 2)
        {

        }
        else if(count == 3)
        {

        }
        else if(count == 4)
        {

        }
    }
}
