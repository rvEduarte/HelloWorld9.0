using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerSubmitScore : MonoBehaviour
{
    public LootlockerSceneProgress progressData;

    public PlayerScoreScriptableObject playerData;
    public LevelUnlockScriptable Level;
    public OfflineScriptableObject LeaderboardStat;

    public SubmitLeaderBoardScript submitLead;

    public void UpdatePlayerProgression(int level)
    {
        switch (level)
        {
            case 1:
                Level.csharpBeginnerLevel2 = "Level2Beginner";
                progressData.UpdatePlayerFile();
                UploadOnlinePlayerStats(level);
                Debug.Log(Level);
                break;
            case 2:
                Level.csharpBeginnerLevel3 = "Level3Beginner";
                progressData.UpdatePlayerFile();
                UploadOnlinePlayerStats(level);
                Debug.Log(Level);
                break;
                //add more levels
        }
    }
    public void UploadOnlinePlayerStats(int level)
    {
        if (level == 1)
        {
            UpdateLeaderboardStats(
                playerData.TotalScore,
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
                playerData.TotalScore,
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
    //pass the value to scriptableObjects of leaderboard
    private void UpdateLeaderboardStats(int playerScore, int leaderboardScore,
    ref string timePhase1, ref string timePhase2, ref string timePhase3,
    ref float exerciseAccuracyPhase2, ref float exerciseAccuracyPhase3,
    ref float quizAccuracyPhase3, ref int totalScore)
    {
        if (playerScore > leaderboardScore)
        {
            timePhase1 = playerData.timePhase1;
            timePhase2 = playerData.timePhase2;
            timePhase3 = playerData.timePhase3;

            exerciseAccuracyPhase2 = playerData.exerciseAccuracyPhase2;
            exerciseAccuracyPhase3 = playerData.exerciseAccuracyPhase3;

            quizAccuracyPhase3 = playerData.quizAccuracyPhase3;
            totalScore = playerScore;

            submitLead.SubmitData(totalScore, timePhase1, timePhase2, timePhase3, exerciseAccuracyPhase2, exerciseAccuracyPhase3, quizAccuracyPhase3);
        }
    }
}
