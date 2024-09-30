 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRaycastIF : MonoBehaviour
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, playerLayer);

        bool playerDetectedThisFrame2 = hit.collider != null && hit.collider.CompareTag("GROUNDER");

        // If the player was detected last frame but is no longer detected, the player has left the raycast
        if (!playerDetectedThisFrame2 && playerDetectedLastFrame)
        {
            Debug.Log("Player left the raycast");
            if (FirstSlotScript.Row1Empty && SecondSlotScript.Row1Below && ThirdSlotScript.Row1Flip)
            {
                playerAnimator.SetWallFlip(true);  // Notify the animator to stop flipping automatically

                StartCoroutine(HandleFlip());
            }
            else if (FirstSlotScript.Row1Empty && SecondSlotScript.Row1Below && ThirdSlotScript.Row1Jump)
            {
                if (!_isJumping) return;
                StartCoroutine(HandleJumpCoroutine());
            }
            else
            {
                //playerAnimator.SetWallFlip(false);
                //return;
            }
        }

        // If the player was just detected this frame and was not detected in the last frame, trigger the flip
        if (playerDetectedThisFrame2 && !playerDetectedLastFrame)
        {
            Debug.Log("PUMASOK");
        }
        // Update player detection state for the next frame
        playerDetectedLastFrame = playerDetectedThisFrame2;
    }
    private IEnumerator HandleJumpCoroutine()
    {
        _isJumping = false;  // Set jumping status to true

        playerController.OnJumpButtonDown();  // Simulate pressing the jump button
        yield return new WaitForSeconds(1f);  // Hold the jump for 0.5 seconds

        playerController.OnJumpButtonUp();  // Simulate releasing the jump button

        _isJumping = true;  // Set jumping status to false after finishing the jump
    }

    private IEnumerator HandleFlip()
    {
        sprite.flipX = !sprite.flipX;  // Flip the sprite only when the player is detected for the first time
        yield return new WaitForSeconds(1f);
        playerAnimator.SetWallFlip(false);
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);

    }
}
