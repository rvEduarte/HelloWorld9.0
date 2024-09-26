using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtUpwardPlatform : MonoBehaviour
{
    // Speed at which the platform will move
    public float speed = 2.0f;

    // Position A and Position B
    public Transform positionA;  // Reference to Position A
    public Transform positionB;  // Reference to Position B

    // A flag to control when the platform should start moving
    private bool shouldMove = false;

    // Direction in which the platform is moving
    private bool movingToPositionB = true;

    private void Update()
    {
        // If the platform is set to move
        if (shouldMove)
        {
            MovePlatform();
        }
    }

    // Function to start moving the platform
    public void StartMoving()
    {
        shouldMove = true;
    }

    // Function to move the platform between Position A and Position B
    private void MovePlatform()
    {
        if (movingToPositionB)
        {
            // Move towards Position B
            transform.position = Vector3.MoveTowards(transform.position, positionB.position, speed * Time.deltaTime);

            // Check if the platform has reached Position B
            if (Vector3.Distance(transform.position, positionB.position) < 0.01f)
            {
                movingToPositionB = false;
            }
        }
        else
        {
            // Move towards Position A
            transform.position = Vector3.MoveTowards(transform.position, positionA.position, speed * Time.deltaTime);

            // Check if the platform has reached Position A
            if (Vector3.Distance(transform.position, positionA.position) < 0.01f)
            {
                movingToPositionB = true;
            }
        }
    }

    // Optional: Function to reset the platform to Position A
    public void ResetPlatform()
    {
        transform.position = positionA.position;
        shouldMove = false;
        movingToPositionB = true;
    }
}
