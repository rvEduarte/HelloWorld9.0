using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtPortalToAnotherScene : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRb;
    public Kurt_LvlLoader levelLoader;  // Reference to LevelLoader script

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
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

    IEnumerator TriggerSceneChange()
    {
        // Disable player's movement while fading/transitioning
        playerRb.simulated = false;

        // Wait before starting the scene transition to simulate a smooth effect (adjust delay as needed)
        yield return new WaitForSeconds(0.5f);

        // Trigger the LevelLoader's scene transition logic
        levelLoader.LoadNextLevel();

        // Enable player's movement again after the scene starts loading (if needed)
        playerRb.simulated = true;
    }
}
