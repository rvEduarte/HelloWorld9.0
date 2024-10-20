using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineLoadProgress : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress;

    private bool checkXp;
    private void Start()
    {
        progress.RegisterPlayerProgression();

        if (PlayerPrefs.GetInt("CheckXp") == 1)
        {
            Debug.LogError(" < ! > CHECK XP  ");
            progress.RegisterXpToLootLocker(); //register xp value from OFFLINE
        }
        else
        {
            Debug.LogError("CHECK XP");
            progress.CheckXpFromOfflineToLootlocker();
            progress.RegisterXpFromOfflineToLootlocker();   // register xp value when comefrom logout
            progress.LoadCompletedLevels();
            PlayerPrefs.SetInt("CheckXp", 1);
        }

        Debug.LogError("ONLINE MODE");
        LevelProgressionLootlockerV2.isOnlineMode = true;
    }
}
