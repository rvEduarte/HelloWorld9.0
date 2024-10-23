using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OfflineLeaderboardScriptable", menuName = "ScriptableObjects/OfflineLeaderboardScriptable", order = 1)]
public class LeaderboardOfflineScriptable : ScriptableObject
{
    public string timePhase1 = "00:00";
    public string timePhase2 = "00:00";
    public string timePhase3 = "00:00";
    public float exerciseAccuracyPhase3;
    public float quizAccuracyPhase3;
    public int TotalScore;
}
