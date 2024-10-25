using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBelowRaycastsELSEIF : MonoBehaviour
{
    public ElsePlayerController playerController;
    private ElsePlayerAnimator playerAnimator;
    public SpriteRenderer sprite;

    [SerializeField] public float raycastDistance = 4f;  // Set this to how far the ray should cast
    public LayerMask playerLayer;  // Layer for player detection

    private bool playerDetectedLastFrame = false;  // Track if the player was detected last frame

    private bool _isJumping = true;
    private bool _isFliping = true;

    public GameObject raycast;

    private void Awake()
    {
        playerAnimator = FindObjectOfType<ElsePlayerAnimator>();  // Assuming there's only one animator
    }

    private void Update()
    {
        if (!StartElseController.isStart) return;
        // Cast a ray from the wall to detect if the player is nearby
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, playerLayer);

        bool playerDetectedThisFrame1 = hit.collider != null && hit.collider.CompareTag("GROUNDER");

        // If the player was detected last frame but is no longer detected, the player has left the raycast
        if (!playerDetectedThisFrame1 && playerDetectedLastFrame)
        {
            Debug.Log("Player left the raycast");
            playerAnimator.SetWallFlip(false);  // Reset flip behavior when the player leaves
        }

        // If the player was just detected this frame and was not detected in the last frame, trigger the flip
        if (playerDetectedThisFrame1 && !playerDetectedLastFrame)
        {
            if (Row2FirstSlotScript.Row2Wall && Row2SecondSlotScript.Row2Below && Row2ThirdSlotScript.Row2Flip)
            {
                Debug.Log("Player detected by BELOW wall raycast FLIP ROW2");
                if (!_isFliping) return;
                StartCoroutine(HandleFlipCoroutine());
            }
            else if (Row2FirstSlotScript.Row2Wall && Row2SecondSlotScript.Row2Below && Row2ThirdSlotScript.Row2Jump)
            {
                Debug.Log("Player detected by BELOW wall raycast JUMP");
                if (!_isJumping) return;
                StartCoroutine(HandleJumpCoroutine());
            }
            // WALK
            else if (Row2FirstSlotScript.Row2Wall && Row2SecondSlotScript.Row2Below && Row2ThirdSlotScript.Row2Walk && sprite.flipX == true) // MOVE LEFT
            {
                Debug.Log("Player detected by BELOW wall raycast WALK LEFT");
                playerController.OnLeftButtonDown();
            }
            else if (Row2FirstSlotScript.Row2Wall && Row2SecondSlotScript.Row2Below && Row2ThirdSlotScript.Row2Walk && sprite.flipX == false) // MOVE RIGHT
            {
                Debug.Log("Player detected by BELOW wall raycast WALK RIGHT");
                playerController.OnRightButtonDown();
            }
            else
            {
                return;
            }
        }
        // Update player detection state for the next frame
        playerDetectedLastFrame = playerDetectedThisFrame1;
    }
    private IEnumerator HandleJumpCoroutine()
    {

        _isJumping = false;  // Set jumping status to true

        playerController.OnJumpButtonDown();  // Simulate pressing the jump button
        yield return new WaitForSeconds(1f);  // Hold the jump for 0.5 seconds

        playerController.OnJumpButtonUp();  // Simulate releasing the jump button

        _isJumping = true;  // Set jumping status to false after finishing the jump
    }

    private IEnumerator HandleFlipCoroutine()
    {
        Debug.Log("FLIP - COROUTINE");
        _isFliping = false;
        raycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        playerAnimator.SetWallFlip(true);  // Notify the animator to stop flipping automatically

        sprite.flipX = !sprite.flipX;  // Flip the sprite only when the player is detected for the first time
        yield return new WaitForSeconds(1f);  // Hold the jump for 0.5 seconds

        raycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        _isFliping = true;
        playerAnimator.SetWallFlip(false);
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * raycastDistance);

    }
}
