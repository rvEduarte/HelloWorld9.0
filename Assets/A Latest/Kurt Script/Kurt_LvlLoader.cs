using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kurt_LvlLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;  // Time for the fade transition

    public void LoadNextLevel()
    {
        // Start the coroutine to load the next level with a transition
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Start the transition animation (fade out)
        transition.SetTrigger("Start");

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionTime);

        // Load the new scene
        SceneManager.LoadScene(levelIndex);
    }
}
