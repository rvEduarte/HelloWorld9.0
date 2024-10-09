using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComicManager : MonoBehaviour
{
    // List of images to fade in sequence
    public Image[] images;

    // Corresponding TextMeshPro or UI Text to fade along with each image
    public TMP_Text[] texts;

    // Duration for the fade effect
    public float fadeDuration = 1.5f;

    void Start()
    {
        // Start the fade sequence
        StartCoroutine(FadeImagesAndTextsInSequence());
    }

    // Coroutine to fade in each image and text sequentially
    private IEnumerator FadeImagesAndTextsInSequence()
    {
        for (int i = 0; i < images.Length; i++)
        {
            // Set initial alpha of both image and text to 0 (transparent)
            SetAlpha(images[i], 0);
            SetAlpha(texts[i], 0);

            // Start the fade in process for both the image and the text
            StartCoroutine(FadeIn(images[i], texts[i]));

            // Wait 1.5 seconds before starting the next fade
            yield return new WaitForSeconds(1.5f);
        }
    }

    // Coroutine to fade in an individual image and text
    private IEnumerator FadeIn(Image img, TMP_Text txt)
    {
        float elapsedTime = 0;

        // Gradually increase alpha value from 0 to 1 over fadeDuration for both image and text
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            SetAlpha(img, newAlpha);
            SetAlpha(txt, newAlpha);

            yield return null;
        }
    }

    // Helper method to set the alpha of an image
    private void SetAlpha(Image img, float alpha)
    {
        Color imgColor = img.color;
        imgColor.a = alpha;
        img.color = imgColor;
    }

    // Helper method to set the alpha of a TextMeshPro text
    private void SetAlpha(TMP_Text txt, float alpha)
    {
        Color txtColor = txt.color;
        txtColor.a = alpha;
        txt.color = txtColor;
    }
}
