using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph3 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI textCompletion1;
    [SerializeField] TextMeshProUGUI textCompletion2;
    [SerializeField] TextMeshProUGUI textCompletion3;
    [SerializeField] TextMeshProUGUI textCompleteph1;
    [SerializeField] TextMeshProUGUI textCompleteph2;
    [SerializeField] TextMeshProUGUI textCompleteph3;
    [SerializeField] TextMeshProUGUI textTotalScoreLevel1;
    [SerializeField] TextMeshProUGUI textAccuracyLevel1Ph2;
    [SerializeField] Pause pauseMenu;

    private float elapsedTime;
    public bool isPicked = false;

    [Header("PH1 ELAPSED TIME")]
    [SerializeField] private string timePh1;

    [Header("PH2 ELAPSED TIME")]
    [SerializeField] private string timePh2;

    [Header("PH3 ELAPSED TIME")]
    [SerializeField] private string timePh3;

    [Header("PH1 SCORE")]
    [SerializeField] private int scorePh1;

    [Header("PH2 SCORE")]
    [SerializeField] private int scorePh2;

    [Header("PH3 SCORE")]
    [SerializeField] private int scorePh3;

    [Header("PH3 QUIZ SCORE")]
    [SerializeField] private int quizScore;

    [Header("PH2 ACCURACY")]
    [SerializeField] private int accuracyPh2;

    private float accuracyPercentage;

    private void Start()
    {
        LoadPlayerPrefs();
        DisplayInitialTimesAndScores();
    }

    private void Update()
    {
        if (!pauseMenu.pause)
        {
            if (!isPicked)
            {
                UpdateElapsedTime();
            }
            else
            {
                SaveAndDisplayCompletionTimesAndScores();
            }
        }
    }

    private void LoadPlayerPrefs()
    {
        timePh1 = PlayerPrefs.GetString("timebegginnerLevel1Ph1");
        timePh2 = PlayerPrefs.GetString("timebegginnerLevel1Ph2");

        scorePh1 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph1");
        scorePh2 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph2");

        accuracyPh2 = PlayerPrefs.GetInt("accuracyBeginnerLevel1Ph2");

        quizScore = PlayerPrefs.GetInt("quizScore_BeginnerLevel1");
    }

    private void DisplayInitialTimesAndScores()
    {
        textCompleteph1.text = $"{timePh1} PH1";
        textCompleteph2.text = $"{timePh2} PH2";
    }

    private void UpdateElapsedTime()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        string formattedTime = $"{minutes:00}:{seconds:00}";

        timerTxt.text = formattedTime;

        UpdateScoreBasedOnTime(elapsedTime);
    }

    private void UpdateScoreBasedOnTime(float time)
    {
        if (time <= 31)
        {
            SetScore(100);
        }
        else if (time <= 46)
        {
            SetScore(80);
            DisplayCompletionText(textCompletion1, "100 score 30 sec.");
        }
        else if (time <= 61)
        {
            SetScore(50);
            DisplayCompletionText(textCompletion2, "80 score 45 sec.");
        }
        else
        {
            SetScore(0);
            DisplayCompletionText(textCompletion3, "50 score 60 sec.");
        }

        PlayerPrefs.Save();
    }

    private void SetScore(int score)
    {
        PlayerPrefs.SetInt("scorebegginnerLevel1Ph3", score);
    }

    private void DisplayCompletionText(TextMeshProUGUI textComponent, string text)
    {
        string weight = "#44";
        textComponent.text = $"<alpha={weight}>{text}";
    }

    private void SaveAndDisplayCompletionTimesAndScores()
    {
        timePh3 = timerTxt.text;
        PlayerPrefs.SetString("timebegginnerLevel1Ph3", timePh3);

        textCompleteph3.text = $"{timePh3} PH3";

        scorePh3 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph3");

        int totalScore = scorePh1 + scorePh2 + scorePh3 + quizScore;
        textTotalScoreLevel1.text = totalScore.ToString();

        PlayerPrefs.SetInt("TotalscoreBegginnerLevel1", totalScore);
        PlayerPrefs.Save();

        CalculateAndDisplayAccuracy();
    }

    private void CalculateAndDisplayAccuracy()
    {
        accuracyPercentage = Mathf.Max(100f - (accuracyPh2 - 1) * 10f, 0f);
        textAccuracyLevel1Ph2.text = $"{accuracyPercentage}% PH2";
    }
}
