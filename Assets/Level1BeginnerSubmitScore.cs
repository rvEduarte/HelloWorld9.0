using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BeginnerSubmitScore : MonoBehaviour
{
    //public LeaderBoardscript submitScoreScript;
    SubmitLeaderBoardScript submitLead;

    string ph1Time;
    string ph2Time;
    string ph3Time;

    int score;
    private void Start()
    {
        // Ensure the SubmitLeaderboardScore script is attached to a GameObject in the scene
        if (submitLead == null)
        {
            submitLead = FindObjectOfType<SubmitLeaderBoardScript>();
        }

        // Check if submitLead is still null after trying to find it
        if (submitLead == null)
        {
            Debug.LogError("SubmitLeaderboardScore component not found in the scene.");
        }

        //Get PlayerPrefs value of the previous saved elapsed time.
        ph1Time = PlayerPrefs.GetString("time_beginnerLevel1Ph1");
        ph2Time = PlayerPrefs.GetString("time_beginnerLevel1Ph2");
        ph3Time = PlayerPrefs.GetString("time_beginnerLevel1Ph3");

        score = PlayerPrefs.GetInt("Totalscore_beginnerLevel1");



    }
    public void SubmitScoreBeginner()
    {
        submitLead.GameManagerLevel1();
        int scoreToSubmit = score;

        string timeTaken1 = ph1Time;
        string timeTaken2 = ph2Time;
        string timeTaken3 = ph3Time;

        // submitLead.titeSubmit(scoreToSubmit, timeTaken, accuracy);
        submitLead.titeSubmit(scoreToSubmit, timeTaken1, timeTaken2, timeTaken3);
    }
}
