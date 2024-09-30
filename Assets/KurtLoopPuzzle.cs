using UnityEngine;
using UnityEngine.UI;

public class KurtLoopPuzzlePiece : MonoBehaviour
{
    [SerializeField] private Image pieceImage;
    [SerializeField] private bool[] connections = new bool[4]; // Top, Right, Bottom, Left
    [SerializeField] private KurtLoopPuzzlePiece[] neighbors = new KurtLoopPuzzlePiece[4]; // Neighboring pieces (Top, Right, Bottom, Left)

    [SerializeField] private GameObject completePuzzleObject; // GameObject to activate when the puzzle is complete

    private int currentRotation = 0;

    void Start()
    {
        if (pieceImage == null)
        {
            pieceImage = GetComponent<Image>();
        }

        UpdateNeighbors();

        // Ensure the complete puzzle object is inactive at the start
        if (completePuzzleObject != null)
        {
            completePuzzleObject.SetActive(false);
        }
    }

    public void OnPieceClick()
    {
        RotatePiece();
        UpdateConnections();

        if (IsCompleteLoop())
        {
            Debug.Log("The puzzle is fully connected!");

            // Activate the complete puzzle object if all pieces are connected
            if (completePuzzleObject != null)
            {
                completePuzzleObject.SetActive(true);
            }
        }
        else
        {
            // Deactivate the complete puzzle object if the puzzle is not fully connected
            if (completePuzzleObject != null)
            {
                completePuzzleObject.SetActive(false);
            }
        }
    }

    private void RotatePiece()
    {
        currentRotation = (currentRotation + 1) % 4;
        transform.Rotate(Vector3.forward, -90f);
    }

    private void UpdateConnections()
    {
        // Rotate connections based on the current rotation
        bool[] rotatedConnections = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            rotatedConnections[(i + currentRotation) % 4] = connections[i];
        }
        connections = rotatedConnections;
    }

    private void UpdateNeighbors()
    {
        // Assuming pieces are arranged in a grid, determine neighbors
        // In an actual implementation, you might need to manually assign neighbors
        // or use a specific logic to detect them based on position.
    }

    private bool IsConnectedCorrectly()
    {
        for (int i = 0; i < 4; i++)
        {
            KurtLoopPuzzlePiece neighbor = neighbors[i];
            if (neighbor != null)
            {
                bool isMatching = connections[i] == neighbor.connections[(i + 2) % 4];
                if (!isMatching)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsCompleteLoop()
    {
        KurtLoopPuzzlePiece[] allPieces = FindObjectsOfType<KurtLoopPuzzlePiece>();

        foreach (KurtLoopPuzzlePiece piece in allPieces)
        {
            if (!piece.IsConnectedCorrectly())
            {
                return false;
            }
        }
        return true;
    }

    public void SetGlowEffect(bool enableGlow)
    {
        if (pieceImage != null)
        {
            pieceImage.color = enableGlow ? Color.yellow : Color.white;
        }
    }

    private void SetGlowEffectForAll(bool enableGlow)
    {
        KurtLoopPuzzlePiece[] allPieces = FindObjectsOfType<KurtLoopPuzzlePiece>();

        foreach (KurtLoopPuzzlePiece piece in allPieces)
        {
            piece.SetGlowEffect(enableGlow);
        }
    }
}
