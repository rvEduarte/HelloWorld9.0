using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressionLootlockerV2 : MonoBehaviour
{
    string progressionKey = "levelprogress";

    public HashSet<int> completedLevels= new HashSet<int>();

    public static bool isOnlineMode;

    int valueToAdd;

    private void Start()
    {
        LoadCompletedLevels();
        CheckXp();
    }
    public void AddXp(int currentLevel)
    {

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
    private void SaveCompletedLevels()
    {
        HashSet<int> allCompletedLevels = new HashSet<int>(completedLevels);
        //allCompletedLevels.UnionWith(completedLevels);

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

    public void CheckXp(System.Action onCompleted = null)
    {
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
                        SaveCompletedLevels();
                    }
                }
                onCompleted?.Invoke(); // Notify that XP check is done
            }
            else
            {
                Debug.Log("Failed: " + response.errorData);
            }
        });
    }
}
