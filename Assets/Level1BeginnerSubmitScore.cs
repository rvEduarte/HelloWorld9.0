using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BeginnerSubmitScore : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;

    SubmitLeaderBoardScript submitLead;


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
