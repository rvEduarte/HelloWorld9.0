using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawStart : MonoBehaviour
{
    public RectTransform below;
    public RectTransform empty;
    public RectTransform spike;
    // public RectTransform flipPanel;

    private void Start()
    {

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0);
        MovePanelToY(929);
    }
    public void MovePanelToY(float newYPosition)
    {
        // Get the current anchored position (X and Y) of the RectTransform
        Vector2 currentPos = below.anchoredPosition;
        Vector2 currentPos1 = empty.anchoredPosition;
        Vector2 currentPos2 = spike.anchoredPosition;
        // Vector2 currentPos1 = flipPanel.anchoredPosition;

        // Change only the Y position
        below.anchoredPosition = new Vector2(currentPos.x, newYPosition);
        empty.anchoredPosition = new Vector2(currentPos1.x, newYPosition);
        spike.anchoredPosition = new Vector2(currentPos2.x, newYPosition);
        // flipPanel.anchoredPosition = new Vector2(currentPos1.x, newYPosition);
    }
}
