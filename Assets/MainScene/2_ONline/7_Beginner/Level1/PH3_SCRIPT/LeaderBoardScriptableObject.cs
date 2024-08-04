using UnityEngine;

[CreateAssetMenu(fileName = "LeaderBoardScriptableObject", menuName = "ScriptableObjects/LeaderBoardScriptableObject")]
public class LeaderBoardScriptableObject : ScriptableObject
{
    public string timePh1;
    public string timePh2;
    public string timePh3;

    public float exerciseAccuracyPh2;
    public float exerciseAccuracyPh3;

    public float quizAccuracyPh3;
}
