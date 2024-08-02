using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUnlockScriptable", menuName = "ScriptableObjects/LevelUnlockScriptable", order = 1)]
public class LevelUnlockScriptable : ScriptableObject
{
    public string Level2Key;
    public string Level3Key;
    public string Level4Key;

    public void UpdateFromJson(string jsonData)
    {
        JsonUtility.FromJsonOverwrite(jsonData, this);
    }
}
