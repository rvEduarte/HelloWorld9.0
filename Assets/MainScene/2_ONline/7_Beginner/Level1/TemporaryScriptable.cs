using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TemporaryScriptable", menuName = "ScriptableObjects/TemporaryScriptable")]
public class TemporaryScriptable : ScriptableObject
{
    public List<LevelData2> levelData = new List<LevelData2>();
}
