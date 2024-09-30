using UnityEngine;
using UnityEngine.UI;

public class Kurt_ArcadeButton : MonoBehaviour
{
    public GameObject targetObject; // The GameObject to activate
    public float animationDuration = 0.5f; // Duration of the animation

    private void Start()
    {
        // Ensure the targetObject is inactive at the start
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }

        // Add listener to button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // Activate the target object
        targetObject.SetActive(true);
        StartCoroutine(AnimateScale(targetObject));
    }

    private System.Collections.IEnumerator AnimateScale(GameObject obj)
    {
        Vector3 originalScale = obj.transform.localScale;
        Vector3 targetScale = originalScale * 1.1f; // Lean effect (1.1x)

        // Scale up
        float time = 0;
        while (time < animationDuration)
        {
            obj.transform.localScale = Vector3.Lerp(originalScale, targetScale, time / animationDuration);
            time += Time.deltaTime;
            yield return null;
        }

        // Scale down back to original
        time = 0;
        while (time < animationDuration)
        {
            obj.transform.localScale = Vector3.Lerp(targetScale, originalScale, time / animationDuration);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure it ends at original scale
        obj.transform.localScale = originalScale;
    }
}
