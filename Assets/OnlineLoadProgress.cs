using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineLoadProgress : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress;

    private bool checkXp;
    private void Start()
    {
        if (PlayerPrefs.GetInt("CheckXp") == 1)
        {
            Debug.LogError(" < ! > CHECK XP  ");
        }
        else
        {
            Debug.LogError("CHECK XP");
            progress.CheckXp();
            progress.LoadCompletedLevels();
            PlayerPrefs.SetInt("CheckXp", 1);
        }

        Debug.LogError("ONLINE MODE");
        LevelProgressionLootlockerV2.isOnlineMode = true;

        progress.RegisterPlayerProgression();
        progress.RegisterXpToLootLocker(); //register xp value from OFFLINE
        
    }
}
