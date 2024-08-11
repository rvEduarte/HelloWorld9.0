using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public RectTransform panel;  // Reference to the panel's RectTransform
    public Button toggleButton;  // Reference to the button
    public float expandPosition = -761.61F;  // Width when the panel is expanded
    public float expandWidth = 761.61f;
    public float collapsedPosition = -300f;  // Width when the panel is collapsed
    public float collapsedWidth = 300f;
    public float animationDuration = 0.5f;  // Duration of the animation

    private bool isExpanded = false;  // Track the panel's state

    void Start()
    {
        // Add listener to the button
        toggleButton.onClick.AddListener(TogglePanel);
    }

    void TogglePanel()
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
