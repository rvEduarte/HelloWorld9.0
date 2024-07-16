using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSubmitTWO : MonoBehaviour
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
    /*public void titeSubmitTWO()
    {
        submitLead.GameManagerLevel2();
        int scoreToSubmit = 3000;
        float timeTaken = 3000f;
        float accuracy = 3000f;
        submitLead.titeSubmit(scoreToSubmit, timeTaken, accuracy);
    }*/
}
