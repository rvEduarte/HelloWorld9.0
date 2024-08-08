using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BeginnerSubmitScore : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;
    public LevelUnlockScriptable Level;

    public SubmitLeaderBoardScript submitLead;

    public void UpdatePlayerProgression(int level)
    {
        if (LootlockerSceneProgress.Instance != null)
        {
            switch (level)
            {
                case 1:
                    Level.csharpBeginnerLevel2 = "Level2Beginner";
                    LootlockerSceneProgress.Instance.UpdatePlayerFile();
                    SubmitScoreBeginner(level);
                    Debug.Log(Level);
                    break;
                case 2:
                    Level.csharpBeginnerLevel3 = "Level3Beginner";
                    LootlockerSceneProgress.Instance.UpdatePlayerFile();
                    SubmitScoreBeginner(level);
                    Debug.Log(Level);
                    break;
                    //add more levels
            }
        }
        else
        {
            Debug.LogError("LevelSelectionLvl2 instance not found.");
        }
    }
    public void SubmitScoreBeginner(int level)
    {
        submitLead.GameManagerLevel(level);
        int scoreToSubmit = playerData.TotalScore;

        string timeTaken1 = playerData.timePhase1;
        string timeTaken2 = playerData.timePhase2;
        string timeTaken3 = playerData.timePhase3;

        float accuracyExercisePh2 = playerData.exerciseAccuracyPhase2;
        float accuracyExercisePh3 = playerData.exerciseAccuracyPhase3;
        float accuracyQuizPh3 = playerData.quizAccuracyPhase3;

        // submitLead.SubmitData(scoreToSubmit, timeTaken, accuracy);
        submitLead.SubmitData(scoreToSubmit, timeTaken1, timeTaken2, timeTaken3, accuracyExercisePh2, accuracyExercisePh3, accuracyQuizPh3);
    }
}
