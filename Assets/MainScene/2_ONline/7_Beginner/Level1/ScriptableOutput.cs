using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableOutput", menuName = "ScriptableOutput")]
public class ScriptableOutput : ScriptableObject 
{
    [Header("Output")]
    [Header("LEVEL 1 PH1")]
    public string FirstOutput;
    public string SecondOutput;
    public string LastOutput;

    public string FirstRunOutput;
    public string SecondRunOutput;
    public string LastRunOutput;
}
