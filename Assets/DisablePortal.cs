using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisablePortal : MonoBehaviour
{
    public float delay = 1f; // Delay before destruction
    private bool isTransitioning = false; // Flag to check if the scene is transitioning

    void Start()
    {
        // Subscribe to the scene unloading event
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene unloading event to prevent memory leaks
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // Check if the scene is transitioning
        if (!isTransitioning)
        {
            isTransitioning = true; // Set the flag to true
            StartCoroutine(DestroyWithDelay());
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        Destroy(gameObject); // Destroy this GameObject
    }
}
