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
    private bool accuracyCalculated = false;

    [Header("PH3 ELAPSED TIME")]
    [SerializeField] private string timePh3;

    private void Start()
    {
        playerData.ResetPh3Values();
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
            else if (!accuracyCalculated)
            {
                SaveAndDisplayCompletionTimesAndScores();
                accuracyCalculated = true;
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

        valueTimeCompleteTxt3.text = timePh3 + " PH3";

        CalculateExerciseAndQuizAccuracy();
    }

    private void CalculateExerciseAndQuizAccuracy()
    {
        // Calculate the percentage and assign it back to the ScriptableObject
        float quizPercentage = ((5 - playerData.wrongQuizPhase3) / 5f) * 100;
        playerData.quizAccuracyPhase3 += quizPercentage;

        // Update the text to display the new Quiz accuracy
        valueQuizAccuracyTxtPh3.text = quizPercentage.ToString("F0") + "% PH3";

        //===========================================================================//

        // Calculate the percentage and assign it back to the ScriptableObject
        float exercisePercentage = Mathf.Max(100f - (playerData.rawExercisePhase3 - 1) * 10f, 0f);
        playerData.exerciseAccuracyPhase3 += exercisePercentage;

        // Update the text to display the Exercise accuracy
        valueExerciseAccuracyTxtPh3.text = $"{exercisePercentage}% PH3";

        // Calculate the total score of PHASE 3 and assign it back to the ScriptableObject
        int totalScorePhase3 = playerData.scoreQuizPhase3 + Mathf.RoundToInt(exercisePercentage);
        playerData.scorePhase3 += totalScorePhase3;

        // Calculate the total Level score and assign it back to the ScriptableObject
        int totalLevelScore = playerData.scorePhase1 + playerData.scorePhase2 + playerData.scorePhase3;
        playerData.TotalScore += totalLevelScore;

        // Update the text to display the new Total Score
        textTotalScoreLevel1.text = totalLevelScore.ToString();

    }
}
