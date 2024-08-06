using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OfflineBeginnerLevel1UpdateProgress : MonoBehaviour
{
    public LevelUnlockScriptable Level;
    public PlayerScoreScriptableObject PlayerStats;
    public OfflineScriptableObject leaderboardstat;
    public void UpdatePlayerProgression()
    {
        Level.csharpBeginnerLevel2 = "Level2Beginner";
    }

    //pass the value to scriptableObjects of leaderboard
    public void UploadOfflinePlayerStats()
    {
        if(PlayerStats.TotalScore > leaderboardstat.TotalScore)
        {
            //Changed to new highestscore
            leaderboardstat.timePhase1 = PlayerStats.timePhase1;
            leaderboardstat.timePhase2 = PlayerStats.timePhase2;
            leaderboardstat.timePhase3 = PlayerStats.timePhase3;

            leaderboardstat.exerciseAccuracyPhase2 = PlayerStats.exerciseAccuracyPhase2;
            leaderboardstat.exerciseAccuracyPhase3 = PlayerStats.exerciseAccuracyPhase3;

            leaderboardstat.quizAccuracyPhase3 = PlayerStats.quizAccuracyPhase3;
            leaderboardstat.TotalScore = PlayerStats.TotalScore;
        }
        else
        {
            //remain the highscore
        }
    }
}
