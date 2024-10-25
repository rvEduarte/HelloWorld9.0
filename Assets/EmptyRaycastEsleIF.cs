using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRaycastEsleIF : MonoBehaviour
{
    public ElsePlayerController playerController;
    public SpriteRenderer sprite;
    [SerializeField]private ElsePlayerAnimator playerAnimator;

    //[SerializeField] public float raycastDistance = 4f;  // Set this to how far the ray should cast
    //public LayerMask playerLayer;  // Layer for player detection

    //private bool playerDetectedLastFrame = false;  // Track if the player was detected last frame

    private bool _isJumping1;
    private bool _isFliping1;
    private void Start()
    {
        _isFliping1 = true;
        _isJumping1 = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GROUNDER"))
        {
            Debug.Log("Player ENTER");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GROUNDER"))
        {
            Debug.Log("Player Exit");
            if (Row2FirstSlotScript.Row2Empty && Row2SecondSlotScript.Row2Below && Row2ThirdSlotScript.Row2Flip)
            {

                playerAnimator.SetWallFlip(true);  // Notify the animator to stop flipping automatically

                if (!_isFliping1) return;
                Debug.Log("flip");
                StartCoroutine(HandleFlip());
            }
            else if (Row2FirstSlotScript.Row2Empty && Row2SecondSlotScript.Row2Below && Row2ThirdSlotScript.Row2Jump)
            {
                if (!_isJumping1) return;
                StartCoroutine(HandleJumpCoroutine());
            }
        }
    }
    private IEnumerator HandleJumpCoroutine()
    {
        _isJumping1 = false;  // Set jumping status to true
        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.001f);
        playerController.OnJumpButtonDown();  // Simulate pressing the jump button
        yield return new WaitForSeconds(1f);  // Hold the jump for 0.5 seconds

        playerController.OnJumpButtonUp();  // Simulate releasing the jump button

        _isJumping1 = true;  // Set jumping status to false after finishing the jump
    }

    private IEnumerator HandleFlip()
    {
        _isFliping1 = false;
        sprite.flipX = !sprite.flipX;  // Flip the sprite only when the player is detected for the first time
        yield return new WaitForSeconds(1);
        playerAnimator.SetWallFlip(false);
        _isFliping1 = true;
    }

    /*private void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);

    }*/
}
