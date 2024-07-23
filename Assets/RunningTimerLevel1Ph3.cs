using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph3 : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;

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

    private void Start()
    {
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

    private void DisplayInitialTimesAndAccuracy()
    {
        valueTimeCompleteTxt1.text = $"{playerData.timePhase1} PH1";
        valueTimeCompleteTxt2.text = $"{playerData.timePhase2} PH2";
        valueExerciseAccuracyTxtPh2.text = $"{playerData.exerciseAccuracyPhase2}% PH2";
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
    }

    private void SetScore(int score)
    {
        //Save TIME SCORE
        playerData.scorePhase3 = score;
    }

    private void DisplayCompletionText(TextMeshProUGUI textComponent, string text)
    {
        string weight = "#44";
        textComponent.text = $"<alpha={weight}>{text}";
    }

    private void SaveAndDisplayCompletionTimesAndScores()
    {
        timePh3 = timerTxt.text;

        //Save TIME VALUE
        playerData.timePhase3 = timePh3;

        valueTimeCompleteTxt3.text = timePh3;

        //Get the quizScore VALUE
        //quizScore = PlayerPrefs.GetInt("quizScore_beginnerLevel1");

        ////Get the SCORE TIME  OF PHASE 3
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
        // Calculate the percentage and assign it back to the ScriptableObject
        float quizPercentage = ((5 - playerData.wrongQuizPhase3) / 5f) * 100;


        // Calculate the percentage and assign it back to the ScriptableObject
        float exercisePercentage = Mathf.Max(100f - (playerData.rawExercisePhase3 - 1) * 10f, 0f);
        playerData.exerciseAccuracyPhase3 += exercisePercentage;

        // Update the text to display the new total score
        valueExerciseAccuracyTxtPh3.text = $"{exercisePercentage}% PH3";

        // Calculate the total score of PHASE 3 and assign it back to the ScriptableObject
        int totalScorePhase3 = playerData.scoreQuizPhase3 + Mathf.RoundToInt(exercisePercentage);
        playerData.scorePhase3 += totalScorePhase3;

        // Calculate the total Level score and assign it back to the ScriptableObject
        int totalLevelScore = playerData.scorePhase1 + playerData.scorePhase2 + playerData.scorePhase3;
        playerData.TotalScore += totalLevelScore;

        //int totalScore = scorePh1 + scorePh2 + scorePh3 + quizScore + Mathf.RoundToInt(exercisePercentage);
        textTotalScoreLevel1.text = totalLevelScore.ToString();

    }
}
