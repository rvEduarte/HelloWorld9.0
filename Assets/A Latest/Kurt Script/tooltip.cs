using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipPanel;           // Reference to the tooltip panel GameObject
    public TextMeshProUGUI tooltipText;       // Reference to the TextMeshProUGUI component in the tooltip panel
    public string tooltipMessage;             // The message to display in the tooltip
    public float animationDuration = 0.2f;    // Duration of the fade animation
    public GameObject otherButton;            // Reference to the other button to hide

    private CanvasGroup canvasGroup;          // Reference to the CanvasGroup component on the tooltip panel
    private Coroutine currentAnimation;

    void Start()
    {
        // Ensure the tooltip has a CanvasGroup component and is initially invisible
        canvasGroup = tooltipPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = tooltipPanel.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;               // Tooltip starts as invisible
        tooltipPanel.SetActive(false);        // Set the tooltip panel inactive initially
    }

    // When the cursor enters the UI element area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the tooltip message
        tooltipText.text = tooltipMessage;

        // Activate the tooltip panel and start the fade-in animation
        tooltipPanel.SetActive(true);
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimateTooltip(0f, 1f, animationDuration));

        // Hide the other button
        if (otherButton != null)
        {
            otherButton.SetActive(false);
        }
    }

    // When the cursor exits the UI element area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Start the fade-out animation and deactivate the panel when done
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimateTooltip(1f, 0f, animationDuration, () =>
        {
            tooltipPanel.SetActive(false);

            // Show the other button again
            if (otherButton != null)
            {
                otherButton.SetActive(true);
            }
        }));
    }

    // Coroutine to animate the tooltip's alpha (fade in/out)
    private IEnumerator AnimateTooltip(float fromAlpha, float toAlpha, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = toAlpha;
        onComplete?.Invoke();
    }
}
