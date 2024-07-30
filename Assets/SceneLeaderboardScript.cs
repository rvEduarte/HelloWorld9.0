using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLeaderboardScript : MonoBehaviour
{
    private string MainTite = "TEst2";
    public void goBackToMainMenu()
    {
        SceneManager.LoadScene(MainTite);
    }
}
