using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuPanel;  

    public bool pause;

    private void Start()
    {
        //Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }

        if (pause)
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            pauseMenuPanel.SetActive(false);    
            Time.timeScale = 1;       
        }
    }

    public void PauseResumeRestart(int id)
    {
        if (id == 0)
        {
            pause = true;
        }
        else if (id == 1)
        {
            pause = false;
        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}