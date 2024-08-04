using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineBeginnerLevel1UpdateProgress : MonoBehaviour
{
    public LevelUnlockScriptable Level;

    public void UpdatePlayerProgression()
    {
        Level.csharpBeginnerLevel2 = "Level2Beginner";
    }
}
