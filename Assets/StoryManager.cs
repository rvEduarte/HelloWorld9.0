using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private string storyScene, LanguageSelection;
    public bool skipStory;
    void Start()
    {
        if (PlayerPrefs.GetInt("SkipStory") == 1)
        {
            skipStory = true;
        }
        else
        {
            skipStory= false;
        }
    }

    public void PlayButton()
    {
        if(skipStory)
        {
            SceneManager.LoadScene(LanguageSelection);
        }
        else
        {
            SceneManager.LoadScene(storyScene);
            PlayerPrefs.SetInt("SkipStory", 1);
        }
    }
}
