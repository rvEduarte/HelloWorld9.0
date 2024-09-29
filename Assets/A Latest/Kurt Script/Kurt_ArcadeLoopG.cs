using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_ArcadeLoopG : MonoBehaviour
{
    public List<Transform> connectionPoints; // Assign in the inspector, these are the points on the edges
    public float rotationAngle = 90f; // Rotation step in degrees
    public float connectionThreshold = 0.1f; // Distance threshold for considering two points connected

    private Kurt_ConnectionSequenceManager sequenceManager;

    private void Start()
    {
        // Find the sequence manager in the scene
        sequenceManager = FindObjectOfType<Kurt_ConnectionSequenceManager>();
    }

    public void OnMouseDown()
    {
        // Rotate the piece by a fixed angle when clicked
        transform.Rotate(0, 0, rotationAngle);
        CheckConnections();
    }

    private void CheckConnections()
    {
        foreach (Transform point in connectionPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, connectionThreshold);

            foreach (Collider2D collider in colliders)
            {
                // Make sure the collider isn't part of the same piece
                if (collider != null && collider.transform.parent != transform)
                {
                    Kurt_ArcadeLoopG otherPiece = collider.GetComponentInParent<Kurt_ArcadeLoopG>();

                    if (otherPiece != null)
                    {
                        // Check if either of this piece's connection points matches with the other piece's connection point
                        if (IsConnectionPointMatching(otherPiece, point))
                        {
                            if (sequenceManager != null && sequenceManager.IsCorrectPiece(this, otherPiece))
                            {
                                Debug.Log("Puzzle piece connected correctly in sequence!");
                            }
                            else
                            {
                                Debug.Log("Connection detected, but not in the correct sequence.");
                            }

                            return; // Exit after the first valid connection is found
                        }
                    }
                }
            }
        }
    }

    private bool IsConnectionPointMatching(Kurt_ArcadeLoopG otherPiece, Transform connectionPoint)
    {
        // Check if any of the other piece's connection points match this piece's connection point
        foreach (Transform otherPoint in otherPiece.connectionPoints)
        {
            if (Vector2.Distance(connectionPoint.position, otherPoint.position) < connectionThreshold)
            {
                // The connection points are aligned
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        // Draw gizmos to visualize connection points in the editor
        Gizmos.color = Color.red;
        foreach (Transform point in connectionPoints)
        {
            Gizmos.DrawSphere(point.position, connectionThreshold);
        }
    }
}
