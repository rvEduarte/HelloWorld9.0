using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public float transitionTime;
    
    void Update()
    {
       if (Input.GetMouseButtonDown(0)) {
            //LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // start transition
        transition.SetTrigger("Start");

        // waiting state
        yield return new WaitForSeconds(1);

        // loading scene
        SceneManager.LoadScene(levelIndex);
    }
    
}
