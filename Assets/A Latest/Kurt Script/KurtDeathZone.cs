using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtDeathZone : MonoBehaviour
{
    // The position to which the player will respawn.
    public Transform respawnPoint;

    // This method is called when the player enters a trigger collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the DeadZone.
        if (other.CompareTag("DeadZone"))
        {
            // Respawn the player to the respawn point.
            Respawn();
        }
    }

    // Method to respawn the player.
    private void Respawn()
    {
        // Set the player's position to the respawn point.
        transform.position = respawnPoint.position;
    }
}
