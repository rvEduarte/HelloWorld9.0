using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ImageTransitionWithTypewriter : MonoBehaviour
{
    public List<Image> images;  // List to hold all images
    public List<TextMeshProUGUI> texts; // List to hold TextMeshPro elements for each image
    public float fadeDuration = 1f;  // Duration for each fade
    public string nextSceneName;  // Name of the next scene to load
    public float typingSpeed = 0.05f;  // Speed of the typewriter effect

    private int currentImageIndex = 0;  // Track the currently displayed image
    private bool isTransitioning = false;  // To avoid multiple transitions at once
    private bool isTyping = false;  // Flag to track whether the text is being typed

    void Start()
    {
        // Check that the lists are of equal length to avoid out of range errors
        if (images.Count != texts.Count)
        {
            Debug.LogError("The number of images and texts must be the same.");
            return;
        }

        // Initially set all images except the first one to be transparent
        for (int i = 1; i < images.Count; i++)
        {
            SetImageAlpha(images[i], 0f);
        }

        // Set all texts to be empty initially
        foreach (var text in texts)
        {
            if (text != null)
                text.text = "";  // Clear text for typewriter effect later
        }

        // Display first image and wait for typewriter activation
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

    // Function to start the typewriter effect for the current image's text
    public void StartTypewriter()
    {
        if (!isTyping && texts[currentImageIndex] != null)
        {
            StartCoroutine(TypeWriterEffect(texts[currentImageIndex], texts[currentImageIndex].text));
        }
    }

    private IEnumerator TypeWriterEffect(TextMeshProUGUI textComponent, string fullText)
    {
        isTyping = true;
        textComponent.text = "";  // Clear the text initially
        string currentText = "";

        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;  // Typewriting complete
    }
}
