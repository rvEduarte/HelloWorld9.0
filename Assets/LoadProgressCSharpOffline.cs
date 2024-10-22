using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProgressCSharpOffline : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress;
    void Start()
    {
        progress.LoadCompletedLevels();
    }
}
