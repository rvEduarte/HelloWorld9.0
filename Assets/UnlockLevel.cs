using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevel : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress;

    [SerializeField] private int value;

    [SerializeField] private GameObject Unlock, Locked;
    private void Start()
    {
        UnlockLevels();
    }

    public void UnlockLevels()
    {
        Debug.Log("Checking level: " + progress.completedLevels.Contains(value));

        // Check if the level is in the completedLevels set
        if (progress.completedLevels.Contains(value))
        {
            // The level has been completed
            Debug.Log("LEVEL UNLOCKED: " + value);
            Locked.SetActive(false);
            Unlock.SetActive(true);
        }
        else
        {
            // The level is not completed
            Debug.Log("LEVEL LOCKED: " + value);
            Locked.SetActive(true);
            Unlock.SetActive(false);
        }
    }
}
