using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerSubmitScore : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;
    public LeaderboardPersistentDataScriptable leaderboardData; // Updated to new scriptable object
    public SubmitLeaderBoardScript submitLead;

    private void Start()
    {
        leaderboardData.LoadData();
    }

    public void UpdatePlayerProgression(int level)
    {
        switch (level)
        {
            case 1:
                submitLead.GameManagerLevel(level);
                UploadOnlinePlayerStats(level);
                break;
            case 2:
                submitLead.GameManagerLevel(level);
                UploadOnlinePlayerStats(level);
                break;
                // Add more levels as needed
        }
    }

    public void UploadOnlinePlayerStats(int level)
    {
        if (level == 1)
        {
            UpdateLeaderboardStats(
                playerData.TotalScore,
                leaderboardData.levelData[0].totalScore, // Access level data from the new structure
                ref leaderboardData.levelData[0].timePhase1,
                ref leaderboardData.levelData[0].timePhase2,
                ref leaderboardData.levelData[0].timePhase3,
                ref leaderboardData.levelData[0].exerciseAccuracyPhase3,
                ref leaderboardData.levelData[0].quizAccuracyPhase3,
                ref leaderboardData.levelData[0].totalScore);
        }
        else if (level == 2)
        {
            UpdateLeaderboardStats(
                playerData.TotalScore,
                leaderboardData.levelData[1].totalScore, // Access level data from the new structure
                ref leaderboardData.levelData[1].timePhase1,
                ref leaderboardData.levelData[1].timePhase2,
                ref leaderboardData.levelData[1].timePhase3,
                ref leaderboardData.levelData[1].exerciseAccuracyPhase3,
                ref leaderboardData.levelData[1].quizAccuracyPhase3,
                ref leaderboardData.levelData[1].totalScore);
        }
        else if (level == 3)
        {
            UpdateLeaderboardStats(
                playerData.TotalScore,
                leaderboardData.levelData[2].totalScore, // Access level data from the new structure
                ref leaderboardData.levelData[2].timePhase1,
                ref leaderboardData.levelData[2].timePhase2,
                ref leaderboardData.levelData[2].timePhase3,
                ref leaderboardData.levelData[2].exerciseAccuracyPhase3,
                ref leaderboardData.levelData[2].quizAccuracyPhase3,
                ref leaderboardData.levelData[2].totalScore);
        }
        else if (level == 4)
        {
            UpdateLeaderboardStats(
                playerData.TotalScore,
                leaderboardData.levelData[3].totalScore, // Access level data from the new structure
                ref leaderboardData.levelData[3].timePhase1,
                ref leaderboardData.levelData[3].timePhase2,
                ref leaderboardData.levelData[3].timePhase3,
                ref leaderboardData.levelData[3].exerciseAccuracyPhase3,
                ref leaderboardData.levelData[3].quizAccuracyPhase3,
                ref leaderboardData.levelData[3].totalScore);
        }
    }

    // Pass the value to scriptableObjects of leaderboard
    private void UpdateLeaderboardStats(int playerScore, int leaderboardScore,
    ref string timePhase1, ref string timePhase2, ref string timePhase3,
    ref float exerciseAccuracyPhase3,
    ref float quizAccuracyPhase3, ref int totalScore)
    {
        if (playerScore > leaderboardScore)
        {
            Debug.LogError("NAG ADD");
            timePhase1 = playerData.timePhase1;
            timePhase2 = playerData.timePhase2;
            timePhase3 = playerData.timePhase3;

            exerciseAccuracyPhase3 = playerData.exerciseAccuracyPhase3;
            quizAccuracyPhase3 = playerData.quizAccuracyPhase3;
            totalScore = playerScore;

            leaderboardData.SaveData(); // Save persistent data
            submitLead.SubmitData(totalScore, timePhase1, timePhase2, timePhase3, exerciseAccuracyPhase3, quizAccuracyPhase3);
        }
        Debug.LogError("HINDI NAG ADD");
    }
}
