using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class K_Pause : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI GameObject
    public Button resumeButton; // Reference to the Resume Button UI element
    public Button pauseButton; // Reference to the Pause Button UI element
    public Button restartButton; // Reference to the Restart Button UI element

    private bool isPaused = false; // Tracks whether the game is paused

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);

        // Add listeners to the buttons
        pauseButton.onClick.AddListener(Pause); // Listener for the Pause button
        resumeButton.onClick.AddListener(Resume); // Listener for the Resume button
        restartButton.onClick.AddListener(RestartGame); // Listener for the Restart button
    }

    void Update()
    {
        // Check if the player pressed the "Escape" key
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
        pauseMenuUI.SetActive(true); // Show the pause menu
        pauseButton.gameObject.SetActive(false); // Hide the pause button
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        pauseButton.gameObject.SetActive(true); // Show the pause button
        Time.timeScale = 1f; // Unpause the game
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
