using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject player;
    public CapsuleCollider2D playerCollider;
    public Rigidbody2D rb;
    [SerializeField] private float rotationSpeed = 2f; // Speed of the rotation
    [SerializeField] private float moveSpeed = 2f;     // Speed of the movement
    public ElsePlayerController playerController;
    [SerializeField] private float xPosition;
    [SerializeField] private float yPosition;

    public static bool isRotate;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            isRotate = true;
            // Disable freeze rotation to allow the player to rotate
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.None;

            Debug.Log("HIT");
            playerCollider.enabled = false;
            playerController.enabled = false;

            //LeanTween.rotate(player, new Vector3(0, 0, 90), 1f);
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.5f);
        isRotate = false;

        yield return new WaitForSeconds(0.5f);
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.transform.localPosition = new Vector2(xPosition, yPosition);
        playerCollider.enabled = true;
        playerController.enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Freeze rotation (Z axis)
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX; // Allow movement on X axis
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; // Allow movement on Y axis
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.transform.localPosition = new Vector2(99.61f, 21.11f);
        }
    }*/
}
