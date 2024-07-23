using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScoreScriptableObject", menuName = "ScriptableObjects/PlayerScoreBeginLevel1", order = 1)]
public class PlayerScoreScriptableObject : ScriptableObject
{
    public string timePhase1;
    public string timePhase2;
    public string timePhase3;

    public int scorePhase1;
    public int scorePhase2;
    public int scorePhase3;

    public float exerciseAccuracyPhase2;
    public float exerciseAccuracyPhase3;

    public float quizAccuracyPhase2;

    public int rawExercisePhase2;
    public int rawExercisePhase3;

    public int rawQuizPhase3;
}
