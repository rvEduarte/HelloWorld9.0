using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionLevel1 : MonoBehaviour
{
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Text progressText;

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
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
