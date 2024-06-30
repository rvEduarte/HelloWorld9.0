using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LvlMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unLockedLevel = PlayerPrefs.GetInt("UnlockedLeve");
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }

        for(int i = 0; i < unLockedLevel; i++)
        {
            buttons[i].enabled = true;
        }
    }

    public void OpenLvl(int lvlId)
    {
        string lvlName = "level " + lvlId;
        SceneManager.LoadScene(lvlName);
    }
    
}
