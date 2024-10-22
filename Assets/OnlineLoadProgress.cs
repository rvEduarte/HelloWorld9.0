using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineLoadProgress : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress;

    private void Start()
    {
        progress.RegisterPlayerProgression();

        progress.completedLevels.Clear();
        PlayerPrefs.DeleteKey("CompletedLevels");
        progress.CheckXpFromOfflineToLootlocker(() =>
        {
            StartCoroutine(DelayAddValue());
        });

        Debug.LogError("ONLINE MODE");
        LevelProgressionLootlockerV2.isOnlineMode = true;

        /*if (PlayerPrefs.GetInt("CheckXp") == 1)
        {
            StartCoroutine(DelayAddValue());
        }
        else
        {

            Debug.LogError("CHECK XP");
            progress.CheckXpFromOfflineToLootlocker();
            PlayerPrefs.SetInt("CheckXp", 1);
        }

        Debug.LogError("ONLINE MODE");
        LevelProgressionLootlockerV2.isOnlineMode = true;*/
    }

    /*private IEnumerator DelayAddValue()
    {
        yield return new WaitForSeconds(1);
        Debug.LogError(" < ! > CHECK XP  ");
        progress.RegisterXpToLootLocker(); //register xp value from OFFLINE
    }*/

    private IEnumerator DelayAddValue()
    {
        yield return new WaitForSeconds(1);
        progress.RegisterXpFromOfflineToLootlocker();
    }
}
