using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipPanel;          // Reference to the tooltip panel GameObject
    public TextMeshProUGUI tooltipText;      // Reference to the TextMeshProUGUI component in the tooltip panel
    public string tooltipMessage;            // The message to display in the tooltip
    public float animationDuration = 0.2f;   // Duration of the scale animation
    public Vector3 startScale = Vector3.zero; // Start scale of the tooltip (invisible)
    public Vector3 endScale = Vector3.one;   // End scale of the tooltip (fully visible)
    public GameObject otherButton;           // Reference to the other button to hide

    private Coroutine currentAnimation;

    void Start()
    {
        // Ensure the tooltip is initially inactive and at the start scale
        tooltipPanel.SetActive(false);
        tooltipPanel.transform.localScale = startScale;
    }

    // When the cursor enters the UI element area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the tooltip message
        tooltipText.text = tooltipMessage;

        // Activate the tooltip panel and start the scaling animation
        tooltipPanel.SetActive(true);
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimateTooltip(startScale, endScale, animationDuration));

        // Hide the other button
        if (otherButton != null)
        {
            otherButton.SetActive(false);
        }
    }

    // When the cursor exits the UI element area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Start the reverse scaling animation and deactivate the panel when done
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimateTooltip(endScale, startScale, animationDuration, () =>
        {
            tooltipPanel.SetActive(false);

            // Show the other button again
            if (otherButton != null)
            {
                otherButton.SetActive(true);
            }
        }));
    }

    // Coroutine to animate the tooltip's scale
    private IEnumerator AnimateTooltip(Vector3 fromScale, Vector3 toScale, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            tooltipPanel.transform.localScale = Vector3.Lerp(fromScale, toScale, elapsed / duration);
            yield return null;
        }

        tooltipPanel.transform.localScale = toScale;
        onComplete?.Invoke();
    }
}
