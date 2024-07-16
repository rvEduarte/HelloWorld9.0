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

    private void Start()
    {
        if (RunningTimer.Instance != null)
        {
            timePh1 = RunningTimer.Instance.timebegginnerLevel1Ph1;
        }
        else
        {
            Debug.LogError("RunningTimer instance not found");
        }
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
                textComplete.text = timerTxt.text;
                timebegginnerLevel1Ph2 = textComplete.text;
            }
        }
    }
}
