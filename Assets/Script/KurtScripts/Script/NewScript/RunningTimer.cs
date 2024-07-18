using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class RunningTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI textComplete;
    [SerializeField] TextMeshProUGUI textCompletion1;
    [SerializeField] TextMeshProUGUI textCompletion2;
    [SerializeField] TextMeshProUGUI textCompletion3;

    [SerializeField] private Pause pauseMenu;

    float elapsedTime;
    public bool isPicked = false;
    //string pekpek;
    [Header("PH1 ELAPSED TIME")]
    [SerializeField] string timebegginnerLevel1Ph1;

    [Header("PH1 SCORE")]
    [SerializeField] int scorebegginnerLevel1Ph1;

    private void Start()
    {
        timebegginnerLevel1Ph1 = PlayerPrefs.GetString("timebegginnerLevel1Ph1");
        scorebegginnerLevel1Ph1 = PlayerPrefs.GetInt("scorebegginnerLevel1Ph1");
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

                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph1", 100);
                    PlayerPrefs.Save();
                }

                else if (timerTxt.text == string.Format("{0:00}:{0:46}", minutes, seconds))
                {
                    string text2 = "80 score 45 sec.";
                    string weight1 = "#44";
                    textCompletion2.text = "<alpha=" + weight1 + ">" + text2;

                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph1", 80);
                    PlayerPrefs.Save();
                }

                else if (trialText == "01:00")   //timerTxt.text == string.Format("{0}:{0:60}", minutes, seconds)
                {
                    Debug.Log("tite");
                    string text3 = "50 score 60 sec.";
                    string weight1 = "#44";
                    textCompletion3.text = "<alpha=" + weight1 + ">" + text3;

                    PlayerPrefs.SetInt("scorebegginnerLevel1Ph1", 50);
                    PlayerPrefs.Save();
                }

            }
            else 
            {
                textComplete.text = timerTxt.text;
                PlayerPrefs.SetString("timebegginnerLevel1Ph1", textComplete.text);
                PlayerPrefs.Save();
            }         
        }
    }
}