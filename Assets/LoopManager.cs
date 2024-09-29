using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public List<Kurt_ArcadeLoopG> puzzlePieces; // List of all puzzle pieces in the scene

    public void CheckPuzzleComplete()
    {
        foreach (Kurt_ArcadeLoopG piece in puzzlePieces)
        {
            //if (!piece.isCorrectlyConnected)
            {
                Debug.Log("Puzzle is not yet complete.");
                return;
            }
        }

        Debug.Log("Puzzle complete!");
    }
}
