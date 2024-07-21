using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph2 : MonoBehaviour
{
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

    int timeScore;
    int accuracy;
    float accuracyPercentage;

    [Header("PH1 ELAPSED TIME")]
    [SerializeField] private string timePh1;

    [Header("PH2 ELAPSED TIME")]
    [SerializeField] private string timePh2;

    [Header("PH1 SCORE")]
    [SerializeField] private int scorePh1;

    private void Start()
    {
        LoadPlayerPrefs();
        DisplayInitialTimes();
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
                SaveAndDisplayCompletionTimes();
            }
        }
    }

    private void LoadPlayerPrefs()
    {
        //get the TIME VALUE and TIME SCORE of PHASE 1
        timePh1 = PlayerPrefs.GetString("time_beginnerLevel1Ph1");
        scorePh1 = PlayerPrefs.GetInt("scoreTime_beginnerLevel1Ph1");
    }

    private void DisplayInitialTimes()
    {
        // Display initial times if needed
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
        //Save TIME SCORE
        PlayerPrefs.SetInt("scoreTime_beginnerLevel1Ph2", score);
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
        PlayerPrefs.SetString("time_beginnerLevel1Ph2", timePh2);
        textCompleteValue.text = $"{timePh2}";
        PlayerPrefs.Save();

        timeScore = PlayerPrefs.GetInt("scoreTime_beginnerLevel1Ph2");

        //Get EXECERCISE ACCURACY VALUE -- TrialComputer.cs -- LINE 265
        accuracy = PlayerPrefs.GetInt("accuracy_beginnerLevel1Ph2");
        CalculateAndDisplayAccuracy();
    }

    private void CalculateAndDisplayAccuracy()
    {
        accuracyPercentage = Mathf.Max(100f - (accuracy - 1) * 10f, 0f);
        exerciseAccuracy.text = $"{accuracyPercentage}%";

        int totalScore = timeScore + Mathf.RoundToInt(accuracyPercentage);
        textTimeScorePh2.text = totalScore.ToString();

        //SAVE ACCURACY PERCENTAGE OF PHASE 2
        PlayerPrefs.SetFloat("accuracyPercentage_beginnerLevel1Ph2", accuracyPercentage);

        // SAVE TOTAL SCORE OF PHASE 2
        PlayerPrefs.SetInt("totalScore_beginnerLevel1Ph2", totalScore);
        PlayerPrefs.Save();
    }
}
