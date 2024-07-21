using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph3 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTxt;

    [Header("Timebased completion fade")]
    [SerializeField] TextMeshProUGUI timeFadeTxt1;
    [SerializeField] TextMeshProUGUI timeFadeTxt2;
    [SerializeField] TextMeshProUGUI timeFadeTxt3;

    [Header("time value completion")]
    [SerializeField] TextMeshProUGUI valueTimeCompleteTxt1;
    [SerializeField] TextMeshProUGUI valueTimeCompleteTxt2;
    [SerializeField] TextMeshProUGUI valueTimeCompleteTxt3;

    [SerializeField] TextMeshProUGUI textTotalScoreLevel1;

    [Header("ACCURACY PERCENTAGE PHASE 2")]
    [SerializeField] TextMeshProUGUI valueAccuracyTxtPh2;
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
    [SerializeField] private float accuracyPh2;

    private float accuracyPercentage;

    private void Start()
    {
        LoadPlayerPrefs();
        DisplayInitialTimesAndAccuracy();
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
        //Get the TIME VALUE of PHASE 1 & PHASE 2
        timePh1 = PlayerPrefs.GetString("time_beginnerLevel1Ph1");
        timePh2 = PlayerPrefs.GetString("time_beginnerLevel1Ph2");

        //Get the TOTAL SCORE of PHASE 1 & PHASE 2
        scorePh1 = PlayerPrefs.GetInt("scoreTime_beginnerLevel1Ph1");
        scorePh2 = PlayerPrefs.GetInt("totalScore_beginnerLevel1Ph2");

        //Get the ACCURACY PERCENTAGE of PHASE 2
        accuracyPh2 = PlayerPrefs.GetFloat("accuracyPercentage_beginnerLevel1Ph2");


        //quizScore = PlayerPrefs.GetInt("quizScore_BeginnerLevel1");
    }

    private void DisplayInitialTimesAndAccuracy()
    {
        valueTimeCompleteTxt1.text = $"{timePh1} PH1";
        valueTimeCompleteTxt2.text = $"{timePh2} PH2";
        valueAccuracyTxtPh2.text = $"{accuracyPh2}% PH2";
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
            DisplayCompletionText(timeFadeTxt1, "100 score 30 sec.");
        }
        else if (time <= 61)
        {
            SetScore(50);
            DisplayCompletionText(timeFadeTxt2, "80 score 45 sec.");
        }
        else
        {
            SetScore(0);
            DisplayCompletionText(timeFadeTxt3, "50 score 60 sec.");
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
        PlayerPrefs.SetString("time_beginnerLevel1Ph3", timePh3);

        valueTimeCompleteTxt3.text = $"{timePh3} PH3";

        scorePh3 = PlayerPrefs.GetInt("scoreTime_beginnerLevel1Ph3");

        int totalScore = scorePh1 + scorePh2 + scorePh3 + quizScore;
        textTotalScoreLevel1.text = totalScore.ToString();

        PlayerPrefs.SetInt("Totalscore_beginnerLevel1", totalScore);
        PlayerPrefs.Save();
    }
}
