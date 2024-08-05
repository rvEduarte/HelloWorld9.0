using UnityEngine;

[CreateAssetMenu(fileName = "OfflineScoreScriptableObject", menuName = "ScriptableObjects/OfflineScore")]

public class OfflineScriptableObject : ScriptableObject
{
    [Header("LEVEL 1")]
    public string timePhase1;
    public string timePhase2;
    public string timePhase3;
    public float exerciseAccuracyPhase2;
    public float exerciseAccuracyPhase3;
    public float quizAccuracyPhase3;
    public int TotalScore;

    [Header("")]
    [Header("LEVEL 2")]
    public string lvl2timePhase1;
}
