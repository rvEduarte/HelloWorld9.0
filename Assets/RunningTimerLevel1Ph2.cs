using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningTimerLevel1Ph2 : MonoBehaviour
{
    //RunningTimer ph1;

    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI textComplete;
    [SerializeField] TextMeshProUGUI textCompletion1;
    [SerializeField] TextMeshProUGUI textCompletion2;
    [SerializeField] TextMeshProUGUI textCompletion3;

    [SerializeField] private Pause pauseMenu;

    float elapsedTime;
    public bool isPicked = false;

    [Header("PH1 ELAPSED TIME")]
    [SerializeField] string timePh1;

    [Header("PH2 ELAPSED TIME")]
    [SerializeField] string timebegginnerLevel1Ph2;

    [Header("PH1 SCORE")]
    [SerializeField] int scorePh1;

    private void Start()
    {
        timePh1 = PlayerPrefs.GetString("timebegginnerLevel1Ph1");
        timebegginnerLevel1Ph2 = PlayerPrefs.GetString("timebegginnerLevel1Ph2");

        scorePh1 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph1");
    }
    void Update()
    {
        if (!pauseMenu.pause)
        {
            if (isPicked == false)
            {
                elapsedTime += Time.deltaTime;

                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                string trialText = string.Format("{0:00}:{1:00}", minutes, seconds);

                timerTxt.text = trialText;

                if (elapsedTime <= 31)
                {
                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph2", 100);
                }
                else if (elapsedTime <= 46)
                {
                    string text1 = "100 score 30 sec.";
                    string weight1 = "#44";
                    textCompletion1.text = "<alpha=" + weight1 + ">" + text1;


                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph2", 80);
                }
                else if (elapsedTime <= 61)
                {
                    string text2 = "80 score 45 sec.";
                    string weight1 = "#44";
                    textCompletion2.text = "<alpha=" + weight1 + ">" + text2;


                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph2", 50);

                }
                else
                {
                    string text3 = "50 score 60 sec.";
                    string weight1 = "#44";
                    textCompletion3.text = "<alpha=" + weight1 + ">" + text3;

                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph2", 0);
                }

                PlayerPrefs.Save();
            }
            else
            {
                textComplete.text = timerTxt.text;
                PlayerPrefs.SetString("timebegginnerLevel1Ph2", textComplete.text);
                PlayerPrefs.Save();
            }
        }
    }
}
