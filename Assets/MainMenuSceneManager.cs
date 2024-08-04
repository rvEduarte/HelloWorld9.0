using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] public bool clicked; //FALSE
    public LevelUnlockScriptable levelUnlockScriptable;
    private void Start()
    {
        Time.timeScale = 1f;

        // Load the saved state
        if (PlayerPrefs.GetInt("uploadPlayer") == 1)
        {
            clicked = true;
        }
        else
        {
            clicked = false;
        }
    }
    public void PlayButtonOnline(string name)
    {
        if (!clicked)
        {
            if (LootlockerSceneProgress.Instance != null)
            {
                LootlockerSceneProgress.Instance.UploadFileFromPath(levelUnlockScriptable);
                Debug.Log(levelUnlockScriptable);
            }
            else
            {
                Debug.LogError("LevelSelectionLvl2 instance not found.");
            }

            // Mark the action as performed and save the state
            PlayerPrefs.SetInt("uploadPlayer", 1); // 1 means true
            PlayerPrefs.Save(); // Make sure changes are saved to disk

            clicked = true;

            SceneManager.LoadScene(name);
            Debug.Log("ISA LANG");
        }

        SceneManager.LoadScene(name);
        Debug.Log("YES");
    }
    public void PlayButtonOffline(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void GotoScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application Successfully Quit");
    }
}
