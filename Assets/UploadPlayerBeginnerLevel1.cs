using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadPlayerBeginnerLevel1 : MonoBehaviour
{
    public LevelUnlockScriptable Level;

    public void PlayButton()
    {
        Level.csharpBeginnerLevel2 = "LevelBeginner2";

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
