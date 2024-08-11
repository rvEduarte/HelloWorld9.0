using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandPanel : MonoBehaviour
{
    [SerializeField] public float expandPosition = -668.461f;  // Width when the panel is expanded
    [SerializeField] public float expandWidth = 668.461f;
    [SerializeField] public float collapsedPosition = -282.0381f;  // Width when the panel is collapsed
    [SerializeField] public float collapsedWidth = 282.0381f;
    public float animationDuration = 0.5f;  // Duration of the animation

    private bool isExpanded = false;  // Track the panel's state

    public void ExpandCollapsePanel(RectTransform panel)
    {
        if (isExpanded)
        {
            // Collapse the panel
            LeanTween.size(panel, new Vector2(collapsedWidth, panel.sizeDelta.y), animationDuration).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.moveX(panel, collapsedPosition, animationDuration).setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            // Expand the panel
            LeanTween.size(panel, new Vector2(expandWidth, panel.sizeDelta.y), animationDuration).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.moveX(panel, expandPosition, animationDuration).setEase(LeanTweenType.easeInOutQuad);
        }

        // Toggle the state
        isExpanded = !isExpanded;
    }
}
