using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerBeginnerLevel1 : MonoBehaviour
{
    public LevelUnlockScriptable Level;

    public void UpdatePlayerProgression()
    {
        Level.csharpBeginnerLevel2 = "Level2Beginner";

        if (LootlockerSceneProgress.Instance != null)
        {
            LootlockerSceneProgress.Instance.UpdatePlayerFile();
            Debug.Log(Level);
        }
        else
        {
            Debug.LogError("LevelSelectionLvl2 instance not found.");
        }
    }
}
