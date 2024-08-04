using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph2 : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;

    [SerializeField] TextMeshProUGUI timerTxt;

    [Header("time value completion")]
    [SerializeField] TextMeshProUGUI textCompleteValue;

    [Header("Timebased completion fade")]
    [SerializeField] TextMeshProUGUI textCompletion1;
    [SerializeField] TextMeshProUGUI textCompletion2;
    [SerializeField] TextMeshProUGUI textCompletion3;

    [Header("timebased score")]
    [SerializeField] TextMeshProUGUI textTimeScorePh2;

    [Header("ACCURACY Percentage")]
    public TextMeshProUGUI exerciseAccuracy;

    [SerializeField] private Pause pauseMenu;

    private float elapsedTime;
    public bool isPicked = false;
    private bool accuracyCalculated = false;

    [Header("PH2 ELAPSED TIME")]
    [SerializeField] private string timePh2;

    private void Start()
    {
        playerData.ResetPh2Values();
    }
    private void Update()
    {
        if (!pauseMenu.pause)
        {
            if (!isPicked)
            {
                UpdateElapsedTime();
            }
            else if(!accuracyCalculated)
            {
                SaveAndDisplayCompletionTimes();
                accuracyCalculated = true; 
            }
        }
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
    }

    private void SetScore(int score)
    {
        //Save TIME SCORE
        playerData.scorePhase2 = score;
    }

    private void DisplayCompletionText(TextMeshProUGUI textComponent, string text)
    {
        string weight = "#44";
        textComponent.text = $"<alpha={weight}>{text}";
    }

    private void SaveAndDisplayCompletionTimes()
    {
        timePh2 = timerTxt.text;

        //Save TIME VALUE
        playerData.timePhase2 = timePh2;

        textCompleteValue.text = timePh2;

        CalculateAndDisplayAccuracy();
    }

    private void CalculateAndDisplayAccuracy()
    {
        // Calculate the percentage and assign it back to the ScriptableObject
        float accuracyPercentage = Mathf.Max(100f - (playerData.rawExercisePhase2 - 1) * 10f, 0f);
        playerData.exerciseAccuracyPhase2 += accuracyPercentage;

        // Update the text to display the new total score
        exerciseAccuracy.text = $"{accuracyPercentage}%";

        // Calculate the total score and assign it back to the ScriptableObject
        int roundedAccuracy = Mathf.RoundToInt(accuracyPercentage);
        playerData.scorePhase2 += roundedAccuracy;

        // Update the text to display the new total score
        textTimeScorePh2.text = playerData.scorePhase2.ToString();
    }
}
