using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawStart : MonoBehaviour
{
    public RectTransform below;
   // public RectTransform flipPanel;

    private void Start()
    {
        MovePanelToY(929);
    }
    public void MovePanelToY(float newYPosition)
    {
        // Get the current anchored position (X and Y) of the RectTransform
        Vector2 currentPos = below.anchoredPosition;

       // Vector2 currentPos1 = flipPanel.anchoredPosition;

        // Change only the Y position
        below.anchoredPosition = new Vector2(currentPos.x, newYPosition);
       // flipPanel.anchoredPosition = new Vector2(currentPos1.x, newYPosition);
    }
}
