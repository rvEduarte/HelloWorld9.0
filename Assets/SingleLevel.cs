using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleLevel : MonoBehaviour
{
    private int currentStarsNum = 0;
    public int levelIndex;

    [SerializeField]
    private PlayerLevelScriptable playerData;

    public void BackButton()
    {
        SceneManager.LoadScene("00_Level Selection");
    }

    public void PressStarsButton(int _starsNum)
    {
        currentStarsNum = _starsNum;

        LevelData levelData = playerData.levels.Find(level => level.levelIndex == levelIndex);

        if (levelData == null)
        {
            levelData = new LevelData { levelIndex = levelIndex, stars = 0 };
            playerData.levels.Add(levelData);
        }

        if (currentStarsNum > levelData.stars)
        {
            levelData.stars = currentStarsNum;
        }

        //MARKER Each level has saved their own stars number
        Debug.Log($"Level {levelIndex} Stars: {levelData.stars}");

        BackButton();
    }
}