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
        // Load the saved state
        if (PlayerPrefs.GetInt("Clicked", 0) == 1)
        {
            clicked = true;
        }
        else
        {
            clicked = false;
        }
    }
    public void PlayButton(string name)
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
            PlayerPrefs.SetInt("Clicked", 1); // 1 means true
            PlayerPrefs.Save(); // Make sure changes are saved to disk

            clicked = true;

            SceneManager.LoadScene(name);
            Debug.Log("ISA LANG");
        }

        SceneManager.LoadScene(name);
        Debug.Log("YES");
    }
}
