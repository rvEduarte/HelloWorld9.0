using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI textComplete;
    [SerializeField] TextMeshProUGUI textCompletion1;
    [SerializeField] TextMeshProUGUI textCompletion2;
    [SerializeField] TextMeshProUGUI textCompletion3;
    [SerializeField] private Pause pauseMenu;

    private float elapsedTime;
    public bool isPicked = false;

    [Header("PH1 ELAPSED TIME")]
    [SerializeField] private string timePh1;

    [Header("PH2 ELAPSED TIME")]
    [SerializeField] private string timePh2;

    [Header("PH1 SCORE")]
    [SerializeField] private int scorePh1;

    [Header("PH2 SCORE")]
    [SerializeField] private int scorePh2;

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
        timePh1 = PlayerPrefs.GetString("timebegginnerLevel1Ph1");
        timePh2 = PlayerPrefs.GetString("timebegginnerLevel1Ph2");
        scorePh1 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph1");
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
        PlayerPrefs.SetInt("scorebegginnerLevel1Ph2", score);
    }

    private void DisplayCompletionText(TextMeshProUGUI textComponent, string text)
    {
        string weight = "#44";
        textComponent.text = $"<alpha={weight}>{text}";
    }

    private void SaveAndDisplayCompletionTimes()
    {
        timePh2 = timerTxt.text;
        PlayerPrefs.SetString("timebegginnerLevel1Ph2", timePh2);
        textComplete.text = $"{timePh2}";
        PlayerPrefs.Save();
    }
}
