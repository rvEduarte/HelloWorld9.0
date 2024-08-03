using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLeaderboardScript : MonoBehaviour
{
    public void goBackToMainMenu(string name)
    {
        SceneManager.LoadScene(name);
    }
}
