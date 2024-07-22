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
    float exerciseAccuracyPh2;
    float exerciseAccuracyPh3;
    float quizAccuracyPh3;

    private void Start()
    {
        //PlayerPrefs.DeleteKey("time_beginnerLevel1Ph1");
        //PlayerPrefs.DeleteKey("time_beginnerLevel1Ph1");

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

        //Get the TOTAL SCORE OF LEVEL 1
        score = PlayerPrefs.GetInt("Totalscore_beginnerLevel1");

        //Get the Exercise ACCURACY of PHASE 2
        exerciseAccuracyPh2 = PlayerPrefs.GetFloat("accuracyPercentage_beginnerLevel1Ph2");        
        
        //Get the Exercise ACCURACY of PHASE 3
        exerciseAccuracyPh3 = PlayerPrefs.GetFloat("exerciseAccuracyPercentage_beginnerLevel1Ph2");

        //Get the Quiz ACCURACY of PHASE 3
        quizAccuracyPh3 = PlayerPrefs.GetFloat("quizAccuracyPercentage_beginnerLevel1Ph2");


    }
    public void SubmitScoreBeginner()
    {
        submitLead.GameManagerLevel1();
        int scoreToSubmit = score;

        string timeTaken1 = ph1Time;
        string timeTaken2 = ph2Time;
        string timeTaken3 = ph3Time;

        float accuracyExercisePh2 = exerciseAccuracyPh2;
        float accuracyExercisePh3 = exerciseAccuracyPh3;
        float accuracyQuizPh3 = quizAccuracyPh3;

        // submitLead.titeSubmit(scoreToSubmit, timeTaken, accuracy);
        submitLead.titeSubmit(scoreToSubmit, timeTaken1, timeTaken2, timeTaken3, accuracyExercisePh2, accuracyExercisePh3, accuracyQuizPh3);
    }
}
