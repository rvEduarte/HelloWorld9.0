using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProgressCSharp : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress;
    void Start()
    {
        progress.LoadCompletedLevels();
    }
}
