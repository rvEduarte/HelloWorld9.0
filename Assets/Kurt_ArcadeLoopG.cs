using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_ArcadeLoopG : MonoBehaviour
{
    public List<Transform> connectionPoints; // List of connection points on the piece
    public bool isCorrectlyConnected = false; // To track if this piece is correctly connected
    private LoopManager loopManager;

    private void Start()
    {
        // Optionally, randomize the starting rotation
        int randomRotations = Random.Range(0, 4);
        transform.Rotate(0, 0, 90 * randomRotations);

        // Find and reference the LoopManager in the scene
        loopManager = FindObjectOfType<LoopManager>();
    }

    public void OnMouseDown()
    {
        // Rotate the piece 90 degrees clockwise when clicked
        transform.Rotate(0, 0, 90);
        CheckConnections();
        Debug.Log("Box clicked!"); // This will log a message when the box is clicked

        // Notify LoopManager to check the entire puzzle's state
        if (loopManager != null)
        {
            loopManager.CheckPuzzleComplete();
        }
    }

    private void CheckConnections()
    {
        isCorrectlyConnected = true; // Assume it's correct unless proven otherwise

        foreach (Transform point in connectionPoints)
        {
            bool pointIsConnected = false;

            // Check each connection point with a small overlap sphere to see if it touches another point
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point.position, 0.1f);
            Debug.Log($"Checking connections for point at {point.position}. Hit colliders: {hitColliders.Length}");

            foreach (Collider2D hitCollider in hitColliders)
            {
                // Avoid checking self
                if (hitCollider.gameObject == gameObject) continue;

                Kurt_ArcadeLoopG otherPiece = hitCollider.GetComponent<Kurt_ArcadeLoopG>();

                if (otherPiece != null)
                {
                    // Check if the hit collider is also one of the connection points of another piece
                    foreach (Transform otherPoint in otherPiece.connectionPoints)
                    {
                        if (hitCollider.transform == otherPoint)
                        {
                            pointIsConnected = true;
                            Debug.Log($"Connection point at {point.position} is correctly connected to {otherPoint.position}.");
                            break;
                        }
                    }
                }
            }

            // If any connection point is not properly connected, mark the piece as not correctly connected
            if (!pointIsConnected)
            {
                isCorrectlyConnected = false;
                Debug.Log($"No connection found for point at {point.position}.");
                break;
            }
        }

        // Log whether this piece is correctly connected after the check
        if (isCorrectlyConnected)
        {
            Debug.Log("Piece is correctly connected!");
        }
    }


    private void OnDrawGizmos()
    {
        // Draw gizmos for connection points to visualize them in the editor
        Gizmos.color = Color.green;
        foreach (Transform point in connectionPoints)
        {
            Gizmos.DrawWireSphere(point.position, 0.1f);
        }
    }
}
