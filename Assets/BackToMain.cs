using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMain : MonoBehaviour
{
    private string MainTite = "Scene 1";
    public void goBackToLogin()
    {
        SceneManager.LoadScene(MainTite);
    }
}
