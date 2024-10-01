using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    public GameObject player;
    public CapsuleCollider2D playerCollider;
    public Rigidbody2D rb;
    [SerializeField] private float rotationSpeed = 2f; // Speed of the rotation
    [SerializeField] private float moveSpeed = 2f;     // Speed of the movement
    public ElsePlayerController playerController;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            // Disable freeze rotation to allow the player to rotate
            rb.constraints = RigidbodyConstraints2D.None;
            Debug.Log("HIT");
            Vector3 targetRotation = new Vector3(0, 0, 90); // Rotate 90 degrees on Z-axis
            Vector3 targetPosition = player.transform.position + new Vector3(2, 2, 0); // Move 10 units up on Y-axis
            StartCoroutine(SmoothRotateAndMove(player.transform, targetRotation, targetPosition, rotationSpeed));
            playerCollider.enabled = false;
            playerController.enabled = false;
        }
    }

    private IEnumerator SmoothRotateAndMove(Transform target, Vector3 targetRotation, Vector3 targetPosition, float duration)
    {
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        Vector3 startPosition = target.position;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            // Smoothly interpolate rotation
            target.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / duration);
            // Smoothly interpolate position
            target.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            yield return null;
        }

        // Ensure final rotation and position are exactly at the target values
        target.rotation = endRotation;
        target.position = targetPosition;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.transform.localPosition = new Vector2(99.61f, 21.11f);
        }
    }*/
}
