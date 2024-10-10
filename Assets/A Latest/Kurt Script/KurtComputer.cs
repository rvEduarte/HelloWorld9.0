using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFader : MonoBehaviour
{
    public Tilemap tilemap;                  // Reference to the Tilemap
    public float fadeDuration = 1.0f;        // Duration of the fade in/out
    public bool fadeInOnStart = true;        // Whether to start with a fade-in effect

    private TilemapRenderer tilemapRenderer; // Renderer for the Tilemap
    private Color tilemapColor;              // The color of the Tilemap material

    private void Start()
    {
        // Get the TilemapRenderer and its current color
        tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
        tilemapColor = tilemapRenderer.material.color;

        // Optionally start with a fade-in effect
        if (fadeInOnStart)
        {
            StartCoroutine(FadeInTilemap());
        }
    }

    // Coroutine to fade the Tilemap in
    public IEnumerator FadeInTilemap()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            SetTilemapAlpha(alpha);
            yield return null;
        }
        SetTilemapAlpha(1); // Ensure full opacity at the end
    }

    // Coroutine to fade the Tilemap out
    public IEnumerator FadeOutTilemap()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            SetTilemapAlpha(alpha);
            yield return null;
        }
        SetTilemapAlpha(0); // Ensure full transparency at the end
    }

    // Helper function to set the Tilemap alpha
    private void SetTilemapAlpha(float alpha)
    {
        tilemapColor.a = alpha;
        tilemapRenderer.material.color = tilemapColor;
    }
}
