using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelProgressionLootlocker : MonoBehaviour
{
    public TMP_Text level;
    public TMP_Text EXP;

    string progressionKey = "levelprogress";

    private HashSet<int> completedLevelsOnline = new HashSet<int>();
    private HashSet<int> completedLevelsOffline = new HashSet<int>();

    private const string CompletedLevelsKey = "CompletedLevels";
    private const string OfflineXPKey = "OfflineXP";

    public bool isOnlineMode = true; // Toggle based on game mode
    private int offlineXP = 0; // Tracks offline XP

    private void Start()
    {
        LoadCompletedLevels();
        LoadOfflineXP();

        if (isOnlineMode)
        {
            // Sync any offline XP when coming online
            SyncOfflineXpToOnline();

            RegisterPlayerProgression();
            CheckXp();
        }
    }

    public void CheckXp()
    {
        if (!isOnlineMode) return;

        LootLockerSDKManager.GetPlayerProgression(progressionKey, response =>
        {
            if (response.success)
            {
                Debug.Log("The player is currently level " + response.step.ToString());
                level.text = "Level: " + response.step.ToString();
                EXP.text = "Xp: " + ((long)response.points).ToString();

                if (!completedLevelsOnline.Contains((int)response.step))
                {
                    completedLevelsOnline.Add((int)response.step);
                }

                if (response.next_threshold != null)
                {
                    long nextThreshold = (long)response.next_threshold;
                    long points = (long)response.points;

                    Debug.Log("Points needed to reach next level: " + (nextThreshold - points).ToString());
                }
            }
            else
            {
                Debug.Log("Failed: " + response.errorData);
            }
        });
    }

    private void RegisterPlayerProgression()
    {
        if (!isOnlineMode) return;

        LootLockerSDKManager.RegisterPlayerProgression(progressionKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Progression registered");
            }
            else
            {
                Debug.Log("Error registering progression");
            }
        });
    }

    public void GiveXP(int currentLevel)
    {
        int pointsAmount = 1;

        if (isOnlineMode)
        {
            if (completedLevelsOnline.Contains(currentLevel))
            {
                Debug.Log("XP for this level has already been added online. Skipping XP submission.");
                return;
            }

            LootLockerSDKManager.AddPointsToPlayerProgression(progressionKey, (ulong)pointsAmount, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Points added to progression");
                    completedLevelsOnline.Add(currentLevel);
                    SaveCompletedLevels();

                    if (response.awarded_tiers.Count > 0)
                    {
                        Debug.Log("Player leveled up");
                    }
                }
                else
                {
                    Debug.Log("Error adding points to progression");
                }
            });
        }
        else
        {
            if (completedLevelsOnline.Contains(currentLevel) || completedLevelsOffline.Contains(currentLevel))
            {
                Debug.Log("XP for this level has already been added (online or offline). Skipping XP submission.");
                return;
            }

            Debug.Log("Adding XP offline.");
            offlineXP += pointsAmount;
            completedLevelsOffline.Add(currentLevel);
            SaveCompletedLevels();
            SaveOfflineXP();
        }
    }

    // Save and Load Completed Levels (Shared Between Online and Offline)
    private void LoadCompletedLevels()
    {
        string completedLevelsString = PlayerPrefs.GetString(CompletedLevelsKey, "");
        if (string.IsNullOrEmpty(completedLevelsString)) return;

        string[] completedLevelsArray = completedLevelsString.Split(',');
        foreach (string level in completedLevelsArray)
        {
            if (int.TryParse(level, out int parsedLevel))
            {
                if (isOnlineMode)
                {
                    completedLevelsOnline.Add(parsedLevel);
                }
                else
                {
                    completedLevelsOffline.Add(parsedLevel);
                }
            }
        }
    }

    private void SaveCompletedLevels()
    {
        HashSet<int> allCompletedLevels = new HashSet<int>(completedLevelsOnline);
        allCompletedLevels.UnionWith(completedLevelsOffline);

        string completedLevelsString = string.Join(",", allCompletedLevels);
        PlayerPrefs.SetString(CompletedLevelsKey, completedLevelsString);
        PlayerPrefs.Save();
    }

    // Offline XP Handling
    private void LoadOfflineXP()
    {
        offlineXP = PlayerPrefs.GetInt(OfflineXPKey, 0);
    }

    private void SaveOfflineXP()
    {
        PlayerPrefs.SetInt(OfflineXPKey, offlineXP);
        PlayerPrefs.Save();
    }

    // Sync offline XP to online progression when going back online
    private void SyncOfflineXpToOnline()
    {
        if (offlineXP > 0)
        {
            LootLockerSDKManager.AddPointsToPlayerProgression(progressionKey, (ulong)offlineXP, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Offline XP synced to online successfully.");
                    offlineXP = 0; // Reset offline XP after syncing
                    PlayerPrefs.SetInt(OfflineXPKey, 0); // Clear saved offline XP
                    PlayerPrefs.Save();
                }
                else
                {
                    Debug.Log("Error syncing offline XP to online: " + response.errorData);
                }
            });
        }
    }
}
