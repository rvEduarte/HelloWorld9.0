using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }
    public void GotoScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application Successfully Quit");
    }
}
