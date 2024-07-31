using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{

    [SerializeField] private bool unlocked;//Default value is false;
    public Image unlockImage;
    public GameObject[] stars;

    public Sprite starSprite;

    [SerializeField]
    private PlayerLevelScriptable playerData;

    private void Start()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus()
    {
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        LevelData previousLevelData = playerData.levels.Find(level => level.levelIndex == previousLevelNum);

        if (previousLevelData != null && previousLevelData.stars > 0)
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
            foreach (var star in stars)
            {
                star.gameObject.SetActive(false);
            }
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
            foreach (var star in stars)
            {
                star.gameObject.SetActive(true);
            }

            LevelData levelData = playerData.levels.Find(level => level.levelIndex == int.Parse(gameObject.name));
            if (levelData != null)
            {
                for (int i = 0; i < levelData.stars; i++)
                {
                    stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
                }
            }
        }
    }

    public void PressSelection(string _LevelName)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(_LevelName);
        }
    }

}
