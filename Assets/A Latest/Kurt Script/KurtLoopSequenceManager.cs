using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_ConnectionSequenceManager : MonoBehaviour
{
    public List<Kurt_ArcadeLoopG> expectedSequence; // Expected order of pieces to connect
    public GameObject targetObject; // The object to deactivate after the puzzle is fully connected
    public float scaleDuration = 1.0f; // Duration of the scale animation

    private int currentIndex = 0;
    private HashSet<Kurt_ArcadeLoopG> connectedPieces = new HashSet<Kurt_ArcadeLoopG>(); // To track pieces already connected

    public bool IsCorrectPiece(Kurt_ArcadeLoopG piece, Kurt_ArcadeLoopG otherPiece)
    {
        // Check if the current piece is the expected piece in the sequence
        if (currentIndex < expectedSequence.Count && piece == expectedSequence[currentIndex] && !connectedPieces.Contains(otherPiece))
        {
            // Make sure the next expected piece is the one being connected to
            if (otherPiece == expectedSequence[currentIndex + 1])
            {
                connectedPieces.Add(piece);
                currentIndex++;

                if (currentIndex >= expectedSequence.Count - 1) // Because we are comparing pairs of connections
                {
                    Debug.Log("Puzzle is fully connected in the correct sequence!");
                    OnPuzzleComplete();
                }
                return true;
            }
        }

        return false;
    }

    private void OnPuzzleComplete()
    {
        if (targetObject != null)
        {
            // Smooth scale down to zero using LeanTween
            LeanTween.scale(targetObject, Vector3.zero, scaleDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    // Debug log to confirm completion
                    Debug.Log("Puzzle completed! Deactivating target object.");
                    // Deactivate the target object after the scale animation completes
                    targetObject.SetActive(false);
                });

            // Optional: Log the initial state of the target object
            Debug.Log($"Target object {targetObject.name} will be scaled down and deactivated.");
        }
        else
        {
            Debug.LogWarning("Target object is not assigned in the ConnectionSequenceManager.");
        }
    }

}
