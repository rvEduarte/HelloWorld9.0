    using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
private void Start()
    {
        Time.timeScale = 1f;
    }

  public void GotoScene(string Category)
    {
        SceneManager.LoadSceneAsync(Category);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application Successfully Quit");
    }

}
