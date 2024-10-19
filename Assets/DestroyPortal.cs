using System.Collections;
using UnityEngine;

public class DestroyPortal : MonoBehaviour
{
    public ParticleSystem portalOut;      // Reference to the particle system
    public float fadeDuration = 2f;       // Duration for the fade-out effect (in seconds)

    private ParticleSystem.MainModule mainModule;   // Main module to control particle color

    void Start()
    {
        if (portalOut != null)
        {
            // Get the main module of the particle system
            mainModule = portalOut.main;
        }
        else
        {
            Debug.LogError("Particle system not assigned!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeOutParticle());
        }
    }

    IEnumerator FadeOutParticle()
    {
        float elapsedTime = 0f;

        // Get the current start color of the particle system (including its alpha value)
        Color originalColor = mainModule.startColor.color;

        // Gradually reduce the alpha value to 0 over `fadeDuration`
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the new alpha value based on the elapsed time
            float newAlpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);

            // Set the new color with the updated alpha value
            mainModule.startColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null;  // Wait until the next frame
        }

        // Once the fade-out is complete, stop the particle system and disable the object
        portalOut.Stop();
        portalOut.gameObject.SetActive(false);
    }
}
