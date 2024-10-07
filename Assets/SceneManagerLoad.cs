using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerLoad : MonoBehaviour
{
    public void GotoScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
