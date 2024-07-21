using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph1 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI textComplete;

    [Header("Timebased completion fade")]
    [SerializeField] TextMeshProUGUI textCompletion1;
    [SerializeField] TextMeshProUGUI textCompletion2;
    [SerializeField] TextMeshProUGUI textCompletion3;

    [Header("timebased score")]
    [SerializeField] TextMeshProUGUI textTimeScorePh1;

    [SerializeField] private Pause pauseMenu;

    private float elapsedTime;
    public bool isPicked = false;

    [Header("PH1 ELAPSED TIME")]
    [SerializeField] private string timeBeginnerLevel1Ph1;

    [Header("PH1 SCORE")]
    [SerializeField] private int scoreBeginnerLevel1Ph1;

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
        PlayerPrefs.SetInt("scoreTime_beginnerLevel1Ph1", score);
    }

    private void DisplayCompletionText(TextMeshProUGUI textComponent, string text)
    {
        string weight = "#44";
        textComponent.text = $"<alpha={weight}>{text}";
    }

    private void SaveAndDisplayCompletionTimes()
    {
        
        timeBeginnerLevel1Ph1 = timerTxt.text;

        //Save TIME VALUE
        PlayerPrefs.SetString("time_beginnerLevel1Ph1", timeBeginnerLevel1Ph1);
        textComplete.text = timeBeginnerLevel1Ph1;
        PlayerPrefs.Save();

        textTimeScorePh1.text = PlayerPrefs.GetInt("scoreTime_beginnerLevel1Ph1").ToString();
    }
}
