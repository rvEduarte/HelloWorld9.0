using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSubmit : MonoBehaviour
{
    //public LeaderBoardscript submitScoreScript;
    SubmitLeaderBoardScript submitLead;
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
    }
    public void titeSubmit4()
    {
        int scoreToSubmit = 2000;
        float timeTaken = 5000f;
        float accuracy = 900f;
        submitLead.titeSubmit(scoreToSubmit, timeTaken, accuracy);
    }
}
