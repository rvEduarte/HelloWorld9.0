using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineLoadProgress : MonoBehaviour
{
    private void Start()
    {
        Debug.LogError("OFFLINE MODE");
        LevelProgressionLootlockerV2.isOnlineMode = false;
    }
}
