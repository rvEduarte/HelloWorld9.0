using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageTransition : MonoBehaviour
{
    public List<Image> images;  // List to hold all images
    public float fadeDuration = 1f;  // Duration for each fade
    public string nextSceneName;  // Name of the next scene to load
    private int currentImageIndex = 0;  // Track the currently displayed image
    private bool isTransitioning = false;  // To avoid multiple transitions at once

    void Start()
    {
        // Initially set all images except the first one to be transparent
        for (int i = 1; i < images.Count; i++)
        {
            SetImageAlpha(images[i], 0f);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning)  // Trigger on left-click
        {
            if (currentImageIndex < images.Count - 1)
            {
                StartCoroutine(FadeInNextImage());  // Transition to next image
            }
            else
            {
                // All images have been displayed, transition to the next scene
                StartCoroutine(TransitionToNextScene());
            }
        }
    }

    private void SetImageAlpha(Image img, float alpha)
    {
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }

    private IEnumerator FadeInNextImage()
    {
        isTransitioning = true;

        int nextImageIndex = currentImageIndex + 1;  // Get the next image index
        float elapsedTime = 0f;

        // Fade in the next image, but leave the current one fully visible
        while (elapsedTime < fadeDuration)
        {
            float alpha2 = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);  // Fade in next image
            SetImageAlpha(images[nextImageIndex], alpha2);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the next image is fully visible
        SetImageAlpha(images[nextImageIndex], 1f);

        currentImageIndex = nextImageIndex;  // Update the current image index
        isTransitioning = false;
    }

    // Coroutine to transition to the next scene
    private IEnumerator TransitionToNextScene()
    {
        // Optional: Add a small delay before transitioning to the next scene
        yield return new WaitForSeconds(1f);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
