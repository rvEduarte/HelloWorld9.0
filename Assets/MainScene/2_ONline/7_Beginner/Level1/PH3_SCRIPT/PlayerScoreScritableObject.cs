using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScoreScriptableObject", menuName = "ScriptableObjects/PlayerScoreBeginLevel1")]
public class PlayerScoreScriptableObject : ScriptableObject
{

    [Header("Phase1")]
    public string timePhase1;
    public int scorePhase1;

    [Header("Phase2")]
    public string timePhase2;
    public int scorePhase2;
    public float exerciseAccuracyPhase2;
    public int rawExercisePhase2;

    [Header("Phase3")]
    public string timePhase3;
    public int scorePhase3;
    public float exerciseAccuracyPhase3;
    public int rawExercisePhase3;
    [Header("")]
    public float quizAccuracyPhase3;
    [Header("")]
    public int wrongQuizPhase3;
    public int scoreQuizPhase3;

    [Header("TOTAL SCORE")]
    public int TotalScore;


    public void ResetAllValues()
    {
        timePhase1 = string.Empty;
        timePhase2 = string.Empty;
        timePhase3 = string.Empty;

        scorePhase1 = 0;
        scorePhase2 = 0;
        scorePhase3 = 0;

        exerciseAccuracyPhase2 = 0;
        exerciseAccuracyPhase3 = 0;

        rawExercisePhase2 = 1;
        rawExercisePhase3 = 0;

        quizAccuracyPhase3 = 0;

        wrongQuizPhase3 = 0;
        scoreQuizPhase3 = 0;

        TotalScore = 0;
    }

    public void ResetPh2Values()
    {
        timePhase2 = string.Empty;
        scorePhase2 = 0;
        exerciseAccuracyPhase2 = 0;
        rawExercisePhase2 = 1;
    }

    public void ResetPh3Values()
    {
        timePhase3 = string.Empty;
        scorePhase3 = 0;
        exerciseAccuracyPhase3 = 0;
        rawExercisePhase3 = 0;
        wrongQuizPhase3 = 0;
        scoreQuizPhase3 = 0;
        TotalScore = 0;

    }
}
