using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoginManager : MonoBehaviour
{
    public static AutoLoginManager Instance;

    // References to the two GameObjects in the second scene you want to deactivate
    public GameObject object1;
    public GameObject object2;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to deactivate the two GameObjects in the second scene
    public void DeactivateGameObjects()
    {
        if (object1 != null && object2 != null)
        {
            object1.SetActive(false); // Deactivate object1
            object2.SetActive(true); // Deactivate object2
            Debug.Log("Objects Deactivated");
        }
        else
        {
            Debug.LogError("One or both GameObjects are not assigned in the Inspector.");
        }
    }
}
