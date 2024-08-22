using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineMainMenu : MonoBehaviour
{
    public LootlockerSceneProgress progressData;
    [Header("Player name")]
    public TextMeshProUGUI playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        playerNameText.text = PlayerPrefs.GetString("PlayerName");
    }
    public void GotoScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
    public void QuitApp()
    {
        progressData.SaveToLocalFile();
        Application.Quit();
        Debug.Log("Application Successfully Quit");
    }
}
