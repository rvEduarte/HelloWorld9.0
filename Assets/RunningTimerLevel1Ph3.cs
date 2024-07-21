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

    [Header("EXERCISE ACCURACY PERCENTAGE PHASE 2")]
    [SerializeField] TextMeshProUGUI valueExerciseAccuracyTxtPh2;

    [Header("EXERCISE ACCURACY PERCENTAGE PHASE 3")]
    [SerializeField] TextMeshProUGUI valueExerciseAccuracyTxtPh3;

    [Header("QUIZ ACCURACY PERCENTAGE PHASE 3")]
    [SerializeField] TextMeshProUGUI valueQuizAccuracyTxtPh3;

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

    [Header("PH2 Exercise ACCURACY")]
    [SerializeField] private float exerciseAccuracyPh2;

    private float accuracyPercentage;

    int quizWrong;
    float quizPercentage;

    int exerciseWrong;
    float exercisePercentage;

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
        exerciseAccuracyPh2 = PlayerPrefs.GetFloat("accuracyPercentage_beginnerLevel1Ph2");


    }

    private void DisplayInitialTimesAndAccuracy()
    {
        valueTimeCompleteTxt1.text = $"{timePh1} PH1";
        valueTimeCompleteTxt2.text = $"{timePh2} PH2";
        valueExerciseAccuracyTxtPh2.text = $"{exerciseAccuracyPh2}% PH2";
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
        PlayerPrefs.SetInt("scoreTime_beginnerLevel1Ph3", score);
    }

    private void DisplayCompletionText(TextMeshProUGUI textComponent, string text)
    {
        string weight = "#44";
        textComponent.text = $"<alpha={weight}>{text}";
    }

    private void SaveAndDisplayCompletionTimesAndScores()
    {
        
        //Get the quizScore VALUE
        quizScore = PlayerPrefs.GetInt("quizScore_beginnerLevel1");

        timePh3 = timerTxt.text;
        PlayerPrefs.SetString("time_beginnerLevel1Ph3", timePh3);

        valueTimeCompleteTxt3.text = $"{timePh3} PH3";

        //Get the SCORE TIME  OF PHASE 3
        scorePh3 = PlayerPrefs.GetInt("scoreTime_beginnerLevel1Ph3");

        CalculateQuizAccuracy();
        CalculateExerciseAccuracy();
    }

    private void CalculateQuizAccuracy()
    {
        quizWrong = PlayerPrefs.GetInt("quizAccuracy_beginnerLevel1");
        quizPercentage = ((5 - quizWrong) / 5f) * 100;
        valueQuizAccuracyTxtPh3.text = quizPercentage.ToString("F0") + "% PH3";

        //Save QUIZ ACCURACY OF PHASE 3
        PlayerPrefs.SetFloat("quizAccuracyPercentage_beginnerLevel1Ph2", quizPercentage);
        PlayerPrefs.Save();
    }

    private void CalculateExerciseAccuracy()
    {
        exerciseWrong = PlayerPrefs.GetInt("excerciseAccuracy_beginnerLevel1");
        exercisePercentage = Mathf.Max(100f - (exerciseWrong - 1) * 10f, 0f);
        valueExerciseAccuracyTxtPh3.text = $"{exercisePercentage}% PH3";

        //Save EXERCISE ACCURACY OF PHASE 3
        PlayerPrefs.SetFloat("exerciseAccuracyPercentage_beginnerLevel1Ph2", exercisePercentage);
        PlayerPrefs.Save();

        int totalScore = scorePh1 + scorePh2 + scorePh3 + quizScore + Mathf.RoundToInt(exercisePercentage);
        textTotalScoreLevel1.text = totalScore.ToString();

        //Set the TOTAL SCORE of PHASE 1 TO PHASE 3
        PlayerPrefs.SetInt("Totalscore_beginnerLevel1", totalScore);
        PlayerPrefs.Save();
    }
}
