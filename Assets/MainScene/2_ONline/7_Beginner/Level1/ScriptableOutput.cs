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

    [Header("Output")]
    [Header("LEVEL 1 PH2")]
    public string FirstOutputPH2;
    public string FirstOutputPH2TWO;

    public string SecondOutputPH2;
    public string SecondOutputPH2TWO;

    public string LastOutputPH2;

    public string FirstRunOutputPH2;
    public string SecondRunOutputPH2;
}
