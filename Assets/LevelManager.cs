using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelProgressionLootlockerV2 progress;
    public UnlockLevel[] levelButtons;

    private void Start()
    {
        // Load completed levels and then unlock levels when done
        progress.CheckXp(() =>
        {
            // Loop through each button and unlock levels based on progression
            foreach (var levelButton in levelButtons)
            {
                levelButton.UnlockLevels();
            }
        });
    }
}
