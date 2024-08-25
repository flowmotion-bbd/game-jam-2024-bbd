using System.Collections;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color[] colors; 
    [SerializeField] private float colorTransitionDuration = 2f;
    [SerializeField] private float alpha = 1f;

    private int currentColorIndex = 0;
    private bool isAnimating = false;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (colors.Length > 0)
        {
            StartCoroutine(AnimateColors());
        }
    }

    private IEnumerator AnimateColors()
    {
        isAnimating = true;

        while (isAnimating)
        {
            Color startColor = colors[currentColorIndex];
            Color nextColor = colors[(currentColorIndex + 1) % colors.Length];

            float elapsedTime = 0f;

            while (elapsedTime < colorTransitionDuration)
            {
                Color currentColor = Color.Lerp(startColor, nextColor, elapsedTime / colorTransitionDuration);
                spriteRenderer.color = currentColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = nextColor;

            currentColorIndex = (currentColorIndex + 1) % colors.Length;

            yield return null;
        }
    }

    public void SetColorTransitionDuration(float newDuration)
    {
        colorTransitionDuration = newDuration;
    }
}
