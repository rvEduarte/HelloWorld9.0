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
    [SerializeField] string timebegginnerLevel1Ph3;

    [Header("LevelCompletion")]
    [SerializeField] TextMeshProUGUI textCompleteph1;
    [SerializeField] TextMeshProUGUI textCompleteph2;
    [SerializeField] TextMeshProUGUI textCompleteph3;

    private void Start()
    {
        timePh1 = PlayerPrefs.GetString("timebegginnerLevel1Ph1");
        timePh2 = PlayerPrefs.GetString("timebegginnerLevel1Ph2");
        timebegginnerLevel1Ph3 = PlayerPrefs.GetString("timebegginnerLevel1Ph3");

        textCompleteph1.text = timePh1 + "PH1";
        textCompleteph2.text = timePh2 + "PH2";
        textCompleteph3.text = timebegginnerLevel1Ph3 + "PH3";
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

                if (timerTxt.text == string.Format("{0:00}:{0:31}", minutes, seconds))
                {
                    string text1 = "100 score 30 sec.";
                    string weight1 = "#44";
                    textCompletion1.text = "<alpha=" + weight1 + ">" + text1;         //textCompletion1.text = "<font-weight=" +"\""+ weight1 +"\""+ ">" + text1 + "</font-weight>";
                }

                else if (timerTxt.text == string.Format("{0:00}:{0:46}", minutes, seconds))
                {
                    string text2 = "80 score 45 sec.";
                    string weight1 = "#44";
                    textCompletion2.text = "<alpha=" + weight1 + ">" + text2;
                }

                else if (trialText == "01:00")   //timerTxt.text == string.Format("{0}:{0:60}", minutes, seconds)
                {
                    Debug.Log("tite");
                    string text3 = "50 score 60 sec.";
                    string weight1 = "#44";
                    textCompletion3.text = "<alpha=" + weight1 + ">" + text3;
                }
            }
            else
            {
                textCompleteph3.text = timerTxt.text;
                PlayerPrefs.SetString("timebegginnerLevel1Ph3", textCompleteph3.text);
                PlayerPrefs.Save();
            }
        }
    }
}
