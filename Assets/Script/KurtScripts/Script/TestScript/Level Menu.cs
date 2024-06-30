using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    public Button[] buttons;
    public GameObject levelButtons;

    // For loadinng screen
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Text progressText;


    private void Awake()
    {
        ButtonsArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

        public void OpenLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        void ButtonsArray()
        {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];

        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    // Loading Screen

    public void LoadLevelBtn(string levelLoad)
    {
        StartCoroutine(LoadLevelAsync(levelLoad));
    }

    IEnumerator LoadLevelAsync(string levelLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelLoad);
        loadingScreen.SetActive(true);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            progressText.text = progressValue * 100f + "%";
            yield return null;
        }
    }
}
