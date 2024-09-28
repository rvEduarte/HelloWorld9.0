using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElseWallController : MonoBehaviour
{
    public ElsePlayerController playerController;
    public SpriteRenderer sprite;
    private ElsePlayerAnimator playerAnimator;

    [SerializeField] public float raycastDistance = 4f;  // Set this to how far the ray should cast
    public LayerMask playerLayer;  // Layer for player detection

    private bool playerDetectedLastFrame = false;  // Track if the player was detected last frame

    private bool _isJumping = true;

    private void Awake()
    {
        playerAnimator = FindObjectOfType<ElsePlayerAnimator>();  // Assuming there's only one animator
    }

    private void Update()
    {
        // Cast a ray from the wall to detect if the player is nearby
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance, playerLayer);

        bool playerDetectedThisFrame = hit.collider != null && hit.collider.CompareTag("Player");

        // If the player was detected last frame but is no longer detected, the player has left the raycast
        if (!playerDetectedThisFrame && playerDetectedLastFrame)
        {
            Debug.Log("Player left the raycast");
            playerAnimator.SetWallFlip(false);  // Reset flip behavior when the player leaves
        }

        // If the player was just detected this frame and was not detected in the last frame, trigger the flip
        if (playerDetectedThisFrame && !playerDetectedLastFrame)
        {
            Debug.Log("PUMASOK");
            // Check if all three conditions are true before proceeding
            if (Row2FirstSlotScript.Row2Wall && Row2SecondSlotScript.Row2Ahead && Row2ThirdSlotScript.Row2Flip)
            {
                Debug.Log("Player detected by wall raycast FLIP ROW2");
                playerAnimator.SetWallFlip(true);  // Notify the animator to stop flipping automatically

                sprite.flipX = !sprite.flipX;  // Flip the sprite only when the player is detected for the first time
            }
            else if (Row2FirstSlotScript.Row2Wall && Row2SecondSlotScript.Row2Ahead && Row2ThirdSlotScript.Row2Jump)
            {
                Debug.Log("Player detected by wall raycast JUMP ROW2");
                if (!_isJumping) return;
                StartCoroutine(HandleJumpCoroutine());
            }
            else if (FirstSlotScript.Row1Wall && SecondSlotScript.Row1Ahead && ThirdSlotScript.Row1Flip)
            {
                Debug.Log("Player detected by wall raycast FLIP ROW1");
                playerAnimator.SetWallFlip(true);  // Notify the animator to stop flipping automatically

                sprite.flipX = !sprite.flipX;  // Flip the sprite only when the player is detected for the first time
            }
            else if (FirstSlotScript.Row1Wall && SecondSlotScript.Row1Ahead && ThirdSlotScript.Row1Jump)
            {
                Debug.Log("Player detected by wall raycast JUMP ROW1");
                if (!_isJumping) return;
                StartCoroutine(HandleJumpCoroutine());
            }
            else
            {
                return;
            }
        }
        // Update player detection state for the next frame
        playerDetectedLastFrame = playerDetectedThisFrame;
    }
    private IEnumerator HandleJumpCoroutine()
    {
        _isJumping = false;  // Set jumping status to true

        playerController.OnJumpButtonDown();  // Simulate pressing the jump button
        yield return new WaitForSeconds(1f);  // Hold the jump for 0.5 seconds

        playerController.OnJumpButtonUp();  // Simulate releasing the jump button

        _isJumping = true;  // Set jumping status to false after finishing the jump
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * raycastDistance);

    }

    /*public SpriteRenderer sprite;
    private ElsePlayerAnimator playerAnimator;

    private void Awake()
    {
        playerAnimator = FindObjectOfType<ElsePlayerAnimator>();  // Assuming there's only one animator
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Row2ThirdSlotScript.Row2Flip) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Pumasok");
            playerAnimator.SetWallFlip(true);  // Notify the animator to stop flipping automatically

            sprite.flipX = !sprite.flipX;  // Flip the sprite
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Row2ThirdSlotScript.Row2Flip) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            playerAnimator.SetWallFlip(false);  // Allow normal flipping when the player exits the wall
        }
    }*/
}
