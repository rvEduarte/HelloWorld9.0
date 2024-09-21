using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class K_Pause : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    public Button resumeButton; 
    public Button pauseButton; 
    public Button restartButton; 

    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);

        // Add listeners to the buttons
        pauseButton.onClick.AddListener(Pause); 
        resumeButton.onClick.AddListener(Resume); 
        restartButton.onClick.AddListener(RestartGame); 
    }

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); 
        pauseButton.gameObject.SetActive(false); 
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); 
        pauseButton.gameObject.SetActive(true); 
        Time.timeScale = 1f; // Unpause the game
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
