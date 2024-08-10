using UnityEngine;

[CreateAssetMenu(fileName = "OfflineScoreScriptableObject", menuName = "ScriptableObjects/OfflineScore")]

public class OfflineScriptableObject : ScriptableObject
{
    [Header("OFFLINE DATA")]
    [Header("LEVEL 1")]
    public string timePhase1 = "00:00";
    public string timePhase2 = "00:00";
    public string timePhase3 = "00:00";
    public float exerciseAccuracyPhase2;
    public float exerciseAccuracyPhase3;
    public float quizAccuracyPhase3;
    public int TotalScore;

    [Header("")]
    [Header("LEVEL 2")]
    public string lvl2_timePhase1 = "00:00";
    public string lvl2_timePhase2 = "00:00";
    public string lvl2_timePhase3 = "00:00";
    public float lvl2_exerciseAccuracyPhase2;
    public float lvl2_exerciseAccuracyPhase3;
    public float lvl2_quizAccuracyPhase3;
    public int lvl2_TotalScore;
}
