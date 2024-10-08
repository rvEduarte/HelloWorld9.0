using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtPortalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    Animation anim;
    Rigidbody2D playerRb;
    Collider2D destinationPortalCollider;

    public float teleportCooldown = 2.0f; // Cooldown time to prevent immediate re-entry

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();
        destinationPortalCollider = destination.GetComponent<Collider2D>(); // Get the destination portal's collider
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
            }
        }
    }

    IEnumerator PortalIn()
    {
        // Disable player's physics to stop movement
        playerRb.simulated = false;

        // Play "Portal In" animation
        anim.Play("Portal In");

        // Move player into the portal
        StartCoroutine(MoveInPortal());

        // Wait for the animation or transition to complete
        yield return new WaitForSeconds(0.5f);

        // Teleport the player to the destination portal
        player.transform.position = destination.position;

        // Play "Portal Out" animation
        anim.Play("Portal Out");

        // Disable the destination portal's collider to prevent immediate re-entry
        destinationPortalCollider.enabled = false;

        // Wait for a bit before re-enabling physics (allow the player to fall or land)
        yield return new WaitForSeconds(0.5f);

        // Re-enable player's physics
        playerRb.simulated = true;

        // Wait for cooldown before re-enabling the destination portal's collider
        yield return new WaitForSeconds(teleportCooldown);
        destinationPortalCollider.enabled = true; // Re-enable destination portal's collider
    }

    IEnumerator MoveInPortal()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
