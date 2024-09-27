using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Lower point (starting position)
    public Transform pointB; // Upper point (destination position)
    public float speed = 2.0f;

    private Vector3 targetPosition;
    private bool shouldMove = false;

    private void Start()
    {
        // Set the initial target position to point B (upper position)
        targetPosition = pointB.position;
    }

    private void Update()
    {
        if (shouldMove)
        {
            // Move the platform towards the target position (point B)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the platform has reached point B
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                // Stop the platform when it reaches point B
                shouldMove = false;
            }
        }
    }

    // Function to start the platform movement
    public void StartMoving()
    {
        shouldMove = true;
    }
}
