using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawStarterPosition : MonoBehaviour
{
    public RectTransform belowPanel;
    public RectTransform EmptyPanel;
    public RectTransform SpikesPanel;

    private void Awake()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0);
        MovePanelToY(929);
    }
    public void MovePanelToY(float newYPosition)
    {
        // Get the current anchored position (X and Y) of the RectTransform
        Vector2 currentPos = belowPanel.anchoredPosition;

        Vector2 currentPos1 = EmptyPanel.anchoredPosition;

        Vector2 currentPos2 = SpikesPanel.anchoredPosition;

        // Change only the Y position
        belowPanel.anchoredPosition = new Vector2(currentPos.x, newYPosition);
        EmptyPanel.anchoredPosition = new Vector2(currentPos1.x, newYPosition);
        SpikesPanel.anchoredPosition = new Vector2(currentPos2.x, newYPosition);
    }
}
