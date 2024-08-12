using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] public float expandPosition = -668.461f;  // Width when the panel is expanded
    [SerializeField] public float expandWidth = 668.461f;
    [SerializeField] public float collapsedPosition = -282.0381f;  // Width when the panel is collapsed
    [SerializeField] public float collapsedWidth = 282.0381f;
    public float animationDuration = 0.5f;  // Duration of the animation

    private bool isExpanded = false;  // Track the panel's state

    private RectTransform panel;

    public void SetScrollViewRectTransform(RectTransform rectTransform)
    {
        panel = rectTransform;
    }

    public void TogglePanel()
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
