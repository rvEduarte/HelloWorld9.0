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

    [SerializeField] private Pause pauseMenu;

    float elapsedTime;
    public bool isPicked = false;

    [Header("PH1 ELAPSED TIME")]
    [SerializeField] string timePh1;

    [Header("PH2 ELAPSED TIME")]
    [SerializeField] string timePh2;

    [Header("PH3 ELAPSED TIME")]
    [SerializeField] string timePh3;

    [Header("PH1 SCORE")]
    [SerializeField] int scorePh1;

    [Header("PH2 SCORE")]
    [SerializeField] int scorePh2;

    [Header("PH3 SCORE")]
    [SerializeField] int scorePh3;

    [Header("LevelCompletion TIME")]
    [SerializeField] TextMeshProUGUI textCompleteph1;
    [SerializeField] TextMeshProUGUI textCompleteph2;
    [SerializeField] TextMeshProUGUI textCompleteph3;

    [Header("LevelCompletion SCORE")]
    [SerializeField] TextMeshProUGUI textTotalScoreLevel1;

    private void Start()
    {
        timePh1 = PlayerPrefs.GetString("timebegginnerLevel1Ph1");
        timePh2 = PlayerPrefs.GetString("timebegginnerLevel1Ph2");

        textCompleteph1.text = timePh1 + " PH1";
        textCompleteph2.text = timePh2 + " PH2";

        scorePh1 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph1");
        scorePh2 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph2");
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
                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph3", 100);
                }
                else if (elapsedTime <= 46)
                {
                    string text1 = "100 score 30 sec.";
                    string weight1 = "#44";
                    textCompletion1.text = "<alpha=" + weight1 + ">" + text1;


                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph3", 80);
                }
                else if (elapsedTime <= 61)
                {
                    string text2 = "80 score 45 sec.";
                    string weight1 = "#44";
                    textCompletion2.text = "<alpha=" + weight1 + ">" + text2;


                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph3", 50);

                }
                else
                {
                    string text3 = "50 score 60 sec.";
                    string weight1 = "#44";
                    textCompletion3.text = "<alpha=" + weight1 + ">" + text3;

                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph3", 0);
                }

                PlayerPrefs.Save();
            }
            else
            {
                timePh3 = timerTxt.text;
                PlayerPrefs.SetString("timebegginnerLevel1Ph3", timePh3);

                textCompleteph3.text = timePh3 + " PH3";

                scorePh3 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph3");

                int totalscore = scorePh1 + scorePh2 + scorePh3;

                textTotalScoreLevel1.text = totalscore.ToString();

                PlayerPrefs.SetInt("TotalscoreBegginnerLevel1", totalscore);

                PlayerPrefs.Save();

            }
        }
    }
}
