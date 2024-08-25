using System.Collections;
using UnityEngine;
using TMPro;

public class TextColourAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color targetColor = Color.red;
    [SerializeField] private float fadeDuration = 2f;

    private void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }

        // Start the breathing effect
        StartCoroutine(BreathingEffect());
    }

    private IEnumerator BreathingEffect()
    {
        while (true)
        {
            // Fade from startColor to targetColor
            yield return StartCoroutine(FadeTextColor(startColor, targetColor, fadeDuration));

            // Fade from targetColor back to startColor
            yield return StartCoroutine(FadeTextColor(targetColor, startColor, fadeDuration));
        }
    }

    private IEnumerator FadeTextColor(Color fromColor, Color toColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            textComponent.color = Color.Lerp(fromColor, toColor, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final color is set
        textComponent.color = toColor;
    }
}