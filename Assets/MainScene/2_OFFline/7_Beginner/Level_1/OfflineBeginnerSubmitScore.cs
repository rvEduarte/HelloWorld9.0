using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class OfflineBeginnerSubmitScore : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData; // Assuming this remains the same
    public LeaderboardPersistentDataScriptable leaderboardData; // Use the new scriptable object

    private void Start()
    {
        leaderboardData.LoadData(); // Load the leaderboard data
    }
    public void UploadOfflinePlayerStats(int level)
    {
        int index = level - 1; // Get the index for the level (0 for level 1, 1 for level 2)

        UpdateLeaderboardStats(
            playerData.TotalScore,
            leaderboardData.levelData[index].totalScore,
            ref leaderboardData.levelData[index].timePhase1,
            ref leaderboardData.levelData[index].timePhase2,
            ref leaderboardData.levelData[index].timePhase3,
            ref leaderboardData.levelData[index].exerciseAccuracyPhase3,
            ref leaderboardData.levelData[index].quizAccuracyPhase3,
            level); // Pass the level as an argument
    }

    // Pass the value to scriptableObjects of leaderboard
    private void UpdateLeaderboardStats(int playerScore, int leaderboardScore,
    ref string timePhase1, ref string timePhase2, ref string timePhase3,
    ref float exerciseAccuracyPhase3,
    ref float quizAccuracyPhase3, int level) // Add level as a parameter
    {
        if (playerScore > leaderboardScore)
        {
            Debug.LogError("NAG ADD");
            // Update values only if player's score is higher
            timePhase1 = playerData.timePhase1;
            timePhase2 = playerData.timePhase2;
            timePhase3 = playerData.timePhase3;

            exerciseAccuracyPhase3 = playerData.exerciseAccuracyPhase3;
            quizAccuracyPhase3 = playerData.quizAccuracyPhase3;

            // Update total score based on the current level index
            leaderboardData.levelData[level - 1].totalScore = playerScore; // Update total score based on the current level
            leaderboardData.SaveData(); // Save updated leaderboard data
        }
    }
}
