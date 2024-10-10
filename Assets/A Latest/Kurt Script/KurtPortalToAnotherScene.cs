using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KurtPortalToAnotherScene : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRb;
    Animation anim;
    public Kurt_LvlLoader levelLoader; // Reference to level loader
    public bool isStartingPortal; // To check if it's the starting point in the next scene (for "Portal Out")

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animation>();

        // If this is the starting portal in a new scene, play the "Portal Out" animation on scene load
        if (isStartingPortal)
        {
            StartCoroutine(PortalOut());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(TriggerSceneChange());
            }
        }
    }

    IEnumerator PortalIn()
    {
        // Disable player's physics to stop movement
        playerRb.simulated = false;

        // Play "Portal In" animation
        anim.Play("Portal In");

        // Wait for the animation to complete
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator PortalOut()
    {
        // Disable player's physics to stop movement
        playerRb.simulated = false;

        // Play "Portal Out" animation
        anim.Play("Portal Out");

        // Wait for the animation to complete
        yield return new WaitForSeconds(0.5f);

        // Re-enable player's physics
        playerRb.simulated = true;
    }

    IEnumerator TriggerSceneChange()
    {
        // Play "Portal In" animation before changing the scene
        yield return StartCoroutine(PortalIn());

        // Trigger the LevelLoader's scene transition logic
        levelLoader.LoadNextLevel(); // This loads the next scene based on the build index

        // After the transition starts, subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If this is the starting portal in the next scene, play the "Portal Out" animation
        if (isStartingPortal)
        {
            StartCoroutine(PortalOut());
        }

        // Unsubscribe to prevent multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
