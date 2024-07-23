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
}
