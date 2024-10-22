using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerOffline : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress; 

    public UnlockLevel[] levelButtons;

    private void Start()
    {
        // Load completed levels before unlocking levels
        progress.LoadCompletedLevels();

        // Loop through each button and unlock levels based on progression
        foreach (var levelButton in levelButtons)
        {
            levelButton.UnlockLevels();
        }
    }
}
