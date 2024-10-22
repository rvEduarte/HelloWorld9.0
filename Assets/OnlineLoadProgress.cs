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
            StartCoroutine(DelayAddValue());
        }
        else
        {
            progress.completedLevels.Clear();
            PlayerPrefs.DeleteKey("CompletedLevels");
            Debug.LogError("CHECK XP");
            progress.CheckXpFromOfflineToLootlocker();
            PlayerPrefs.SetInt("CheckXp", 1);
        }

        Debug.LogError("ONLINE MODE");
        LevelProgressionLootlockerV2.isOnlineMode = true;
    }

    private IEnumerator DelayAddValue()
    {
        yield return new WaitForSeconds(1);
        Debug.LogError(" < ! > CHECK XP  ");
        progress.RegisterXpToLootLocker(); //register xp value from OFFLINE
    }
}
