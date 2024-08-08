using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OfflineBeginnerLevel1UpdateProgress : MonoBehaviour
{
    public LevelUnlockScriptable Level;
    public PlayerScoreScriptableObject PlayerStats;
    public OfflineScriptableObject LeaderboardStat;
    public void UpdatePlayerProgression(int level)
    {
        switch (level)
        {
            case 2:
                Level.csharpBeginnerLevel2 = "Level2Beginner";
                UploadOfflinePlayerStats(level);
                break;
            case 3:
                Level.csharpBeginnerLevel3 = "Level3Beginner";
                UploadOfflinePlayerStats(level);
                break;
            default:
                // Handle other levels or invalid input
                break;
        }
    }

    public void UploadOfflinePlayerStats(int level)
    {
        if (level == 1)
        {
            UpdateLeaderboardStats(
                PlayerStats.TotalScore, 
                LeaderboardStat.TotalScore,
                ref LeaderboardStat.timePhase1, 
                ref LeaderboardStat.timePhase2, 
                ref LeaderboardStat.timePhase3,
                ref LeaderboardStat.exerciseAccuracyPhase2, 
                ref LeaderboardStat.exerciseAccuracyPhase3,
                ref LeaderboardStat.quizAccuracyPhase3, 
                ref LeaderboardStat.TotalScore);
        }
        else if (level == 2)
        {
            UpdateLeaderboardStats(
                PlayerStats.TotalScore, 
                LeaderboardStat.lvl2_TotalScore,
                ref LeaderboardStat.lvl2_timePhase1, 
                ref LeaderboardStat.lvl2_timePhase2, 
                ref LeaderboardStat.lvl2_timePhase3,
                ref LeaderboardStat.lvl2_exerciseAccuracyPhase2, 
                ref LeaderboardStat.lvl2_exerciseAccuracyPhase3,
                ref LeaderboardStat.lvl2_quizAccuracyPhase3, 
                ref LeaderboardStat.lvl2_TotalScore);
        }
    }

    private void UpdateLeaderboardStats(int playerScore, int leaderboardScore,
    ref string timePhase1, ref string timePhase2, ref string timePhase3,
    ref float exerciseAccuracyPhase2, ref float exerciseAccuracyPhase3,
    ref float quizAccuracyPhase3, ref int totalScore)
    {
        if (playerScore > leaderboardScore)
        {
            timePhase1 = PlayerStats.timePhase1;
            timePhase2 = PlayerStats.timePhase2;
            timePhase3 = PlayerStats.timePhase3;

            exerciseAccuracyPhase2 = PlayerStats.exerciseAccuracyPhase2;
            exerciseAccuracyPhase3 = PlayerStats.exerciseAccuracyPhase3;

            quizAccuracyPhase3 = PlayerStats.quizAccuracyPhase3;
            totalScore = playerScore;
        }
    }

    //pass the value to scriptableObjects of leaderboard
    public void UploadOfflinePlayerStatsLevel1()
    {
        if(PlayerStats.TotalScore > LeaderboardStat.TotalScore)
        {
            //Changed to new highestscore
            LeaderboardStat.timePhase1 = PlayerStats.timePhase1;
            LeaderboardStat.timePhase2 = PlayerStats.timePhase2;
            LeaderboardStat.timePhase3 = PlayerStats.timePhase3;

            LeaderboardStat.exerciseAccuracyPhase2 = PlayerStats.exerciseAccuracyPhase2;
            LeaderboardStat.exerciseAccuracyPhase3 = PlayerStats.exerciseAccuracyPhase3;

            LeaderboardStat.quizAccuracyPhase3 = PlayerStats.quizAccuracyPhase3;
            LeaderboardStat.TotalScore = PlayerStats.TotalScore;
        }
        else
        {
            //remain the highscore
        }
    }

    public void UploadOfflinePlayerStatsLevel2()
    {
        if (PlayerStats.TotalScore > LeaderboardStat.TotalScore)
        {
            //Changed to new highestscore
            LeaderboardStat.lvl2_timePhase1 = PlayerStats.timePhase1;
            LeaderboardStat.lvl2_timePhase2 = PlayerStats.timePhase2;
            LeaderboardStat.lvl2_timePhase3 = PlayerStats.timePhase3;

            LeaderboardStat.lvl2_exerciseAccuracyPhase2 = PlayerStats.exerciseAccuracyPhase2;
            LeaderboardStat.lvl2_exerciseAccuracyPhase3 = PlayerStats.exerciseAccuracyPhase3;

            LeaderboardStat.lvl2_quizAccuracyPhase3 = PlayerStats.quizAccuracyPhase3;
            LeaderboardStat.lvl2_TotalScore = PlayerStats.TotalScore;
        }
        else
        {
            //remain the highscore
        }
    }
}
