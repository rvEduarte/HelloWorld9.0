using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressionLootlockerV2 : MonoBehaviour
{
    public TMP_Text text1;
    string progressionKey = "levelprogress";

    public HashSet<int> completedLevels= new HashSet<int>();

    public static bool isOnlineMode;

    int valueToAdd;

    public void RegisterPlayerProgression()
    {
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
    private void SaveCompletedLevels()
    {
        HashSet<int> allCompletedLevels = new HashSet<int>(completedLevels);
        allCompletedLevels.UnionWith(completedLevels);

        string completedLevelsString = string.Join(",", allCompletedLevels);
        PlayerPrefs.SetString("CompletedLevels", completedLevelsString);
        PlayerPrefs.Save();
    }
    public void LoadCompletedLevels()
    {
        string completedLevelsString = PlayerPrefs.GetString("CompletedLevels", "");
        if (string.IsNullOrEmpty(completedLevelsString))
        {
            Debug.Log("No completed levels found in PlayerPrefs.");
            return;
        }

        string[] completedLevelsArray = completedLevelsString.Split(',');
        foreach (string level in completedLevelsArray)
        {
            if (int.TryParse(level, out int parsedLevel))
            {
                completedLevels.Add(parsedLevel);
                Debug.Log("Loaded completed level: " + parsedLevel);
            }
        }
    }
    public void AddXp(int currentLevel)
    {
        LoadCompletedLevels();
        if (completedLevels.Contains(currentLevel))
        {
            Debug.Log("XP for this level has already been added online. Skipping XP submission.");
        }
        else
        {
            completedLevels.Add(currentLevel);
            SaveCompletedLevels();
            int pointsAmount = 1;

            int currentXp = PlayerPrefs.GetInt("XpValue", 0);

            currentXp += pointsAmount;

            PlayerPrefs.SetInt("XpValue", currentXp);

            Debug.Log("Added " + pointsAmount + " XP. Total XP is now: " + currentXp);
        }

        if(isOnlineMode == true)
        {    
            RegisterXpToLootLocker();
        }
    }

    public void RegisterXpToLootLocker()
    {
        valueToAdd = PlayerPrefs.GetInt("XpValue");
        Debug.Log("Value to add: " + valueToAdd);
        LootLockerSDKManager.AddPointsToPlayerProgression(progressionKey, (ulong)valueToAdd, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Points added to progression");
                PlayerPrefs.DeleteKey("XpValue");

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
    public void CheckXpFromOfflineToLootlocker(System.Action onCompleted1 = null)
    {
        Debug.LogError("CheckXpFromOfflineToLootlocker called");
        LoadCompletedLevels();
        LootLockerSDKManager.GetPlayerProgression(progressionKey, response =>
        {
            if (response.success)
            {
                Debug.Log("The player's XP is currently: " + response.points.ToString());

                int totalPoints = (int)response.points;
                for (int i = 1; i <= totalPoints; i++)
                {
                    if (!completedLevels.Contains(i))
                    {
                        completedLevels.Add(i);
                        Debug.Log("Added completed level: " + i);
                        
                    }
                }
                SaveCompletedLevels();
                onCompleted1?.Invoke(); // Notify that XP check is done
            }
            else
            {
                Debug.Log("Failed: " + response.errorData);
            }
        });
        
    }
    public void RegisterXpFromOfflineToLootlocker()
    {
        int totalValue = completedLevels.Count;
        valueToAdd = PlayerPrefs.GetInt("XpValue");

        Debug.Log("COMPLETED LEVEL: " + totalValue + " OFFLINEVALUE: " + valueToAdd);
        if(valueToAdd > totalValue)     //Add xp from OFFline to ONline
        {
            Debug.Log("WILL ADD XP VALUE FROM OFFLINE");
            int totalValueToAdd = valueToAdd - totalValue;
            Debug.LogError(totalValueToAdd);

            LootLockerSDKManager.AddPointsToPlayerProgression(progressionKey, (ulong)totalValueToAdd, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Points added to progression");
                    PlayerPrefs.DeleteKey("XpValue");

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
            Debug.Log("WILL NOT ADD XP VALUE FROM OFFLINE");
            PlayerPrefs.DeleteKey("XpValue");
        }
    }
    public void CHECKCOMPLETEDLEVELS()
    {
        LoadCompletedLevels();
        Debug.Log("PUMASOK");
        Debug.Log("Completed levels count: " + completedLevels.Count);
        foreach (int level in completedLevels)
        {
            text1.text += level.ToString() + "\n";
        }
    }
    public void CheckXp(System.Action onCompleted = null)
    {
        Debug.LogError("CheckXp called");
        LootLockerSDKManager.GetPlayerProgression(progressionKey, response =>
        {
            if (response.success)
            {
                Debug.Log("The player's XP is currently: " + response.points.ToString());

                int totalPoints = (int)response.points;
                for (int i = 1; i <= totalPoints; i++)
                {
                    if (!completedLevels.Contains(i))
                    {
                        completedLevels.Add(i);
                        Debug.Log("Added completed level: " + i);
                        
                    }
                }
                SaveCompletedLevels();
                onCompleted?.Invoke(); // Notify that XP check is done
            }
            else
            {
                Debug.Log("Failed: " + response.errorData);
            }
        });
    }
}