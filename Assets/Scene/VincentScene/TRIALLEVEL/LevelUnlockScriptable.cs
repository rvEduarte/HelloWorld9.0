using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUnlockScriptable", menuName = "ScriptableObjects/LevelUnlockScriptable", order = 1)]
public class LevelUnlockScriptable : ScriptableObject
{
    public string csharpBeginnerLevel2;
    public string csharpBeginnerLevel3;
    public string csharpBeginnerLevel4;

    public string javaBeginnerLevel2;
    public string javaBeginnerLevel3;
    public string javaBeginnerLevel4;

    public void UpdateFromJson(string jsonData)
    {
        JsonUtility.FromJsonOverwrite(jsonData, this);
    }
}
