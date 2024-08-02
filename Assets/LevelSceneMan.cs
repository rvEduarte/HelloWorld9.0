using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneMan : MonoBehaviour
{
    public void GoToScene(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }
}
