using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelScriptable", menuName = "ScriptableObjects/PlayerLevelScriptable", order = 1)]
public class PlayerLevelScriptable : ScriptableObject
{
    public List<LevelData> levels;
}
