using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerBeginnerLevel1 : MonoBehaviour
{
    public LevelUnlockScriptable Level;

    public void UpdatePlayerProgression(int level)
    {
        if(LootlockerSceneProgress.Instance != null)
        {
            switch (level)
            {
                case 1:
                    Level.csharpBeginnerLevel2 = "Level2Beginner";
                    LootlockerSceneProgress.Instance.UpdatePlayerFile();
                    Debug.Log(Level);
                    break;
                case 2:
                    Level.csharpBeginnerLevel3 = "Level3Beginner";
                    LootlockerSceneProgress.Instance.UpdatePlayerFile();
                    Debug.Log(Level);
                    break;
                //add more levels
            }
        }
        else
        {
            Debug.LogError("LevelSelectionLvl2 instance not found.");
        }        
    }
}
