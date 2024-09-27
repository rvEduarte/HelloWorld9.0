using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtUpwardPlatform : MonoBehaviour
{
    public Rigidbody2D body;

    public float speed = 2.0f;  // Speed at which the platform will move
    public Transform positionA;  // Position A
    public Transform positionB;  // Position B

    private bool shouldMove = false;  // Controls when the platform should start moving
    private bool movingToPositionB = true;  // Direction in which the platform is moving

    private void Update()
    {
        // If the platform is set to move
        if (shouldMove)
        {
            MovePlatform();
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
    }

    private void MovePlatform()
    {
        if (movingToPositionB)
        {
            // Move towards Position B
            transform.position = Vector3.MoveTowards(transform.position, positionB.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, positionB.position) < 0.01f)
            {
                movingToPositionB = false;
            }
        }
        else
        {
            // Move towards Position A
            transform.position = Vector3.MoveTowards(transform.position, positionA.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, positionA.position) < 0.01f)
            {
                movingToPositionB = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player is on the platform, parent the player to the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            body.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When the player leaves the platform, un-parent them
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            body.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
