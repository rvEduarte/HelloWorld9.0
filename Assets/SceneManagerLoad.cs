using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerLoad : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void GotoScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
