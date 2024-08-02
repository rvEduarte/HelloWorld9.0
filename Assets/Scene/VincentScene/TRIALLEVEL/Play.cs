using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public LevelUnlockScriptable Level;

    public void PlayButton()
    {
        Level.csharpBeginnerLevel2 = "LevelBeginner2";
        //EventManager.LevelUnlock();
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
