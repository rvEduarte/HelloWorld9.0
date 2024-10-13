using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_OrDownPlatform : MonoBehaviour
{
    public Transform positionA;  // Position A (for upward movement)
    public Transform positionB;  // Position B (for downward movement)
    public float speed = 2.0f;   // Speed at which the platform will move
    private bool shouldMoveUp = false;  // Flag to control upward movement
    private bool shouldMoveDown = false; // Flag to control downward movement

    private void Update()
    {
        // Check for input and move the platform
        if (shouldMoveUp)
        {
            MovePlatform(positionA);
        }
        else if (shouldMoveDown)
        {
            MovePlatform(positionB);
        }
    }

    public void MoveUp()
    {
        shouldMoveUp = true;
        shouldMoveDown = false;  // Ensure downward movement is disabled
    }

    public void MoveDown()
    {
        shouldMoveDown = true;
        shouldMoveUp = false;  // Ensure upward movement is disabled
    }

    private void MovePlatform(Transform targetPosition)
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

        // Stop moving if the platform reaches the target position
        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            shouldMoveUp = false;  // Stop moving up
            shouldMoveDown = false;  // Stop moving down
        }
    }
}
