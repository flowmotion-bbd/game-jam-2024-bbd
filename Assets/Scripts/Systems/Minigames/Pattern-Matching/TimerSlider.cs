using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    [SerializeField] Image fillImage = null;
    public float startWidth = 300f; // Width b (initial width)
    public float targetWidth = 100f; // Width a (final width)
    public float duration = 2f; // Time in seconds for the transition

    private RectTransform rectTransform;
    private float elapsedTime = 0f;
    private bool isTransitioning = false;

    private void Start()
    {
        if (fillImage != null)
        {
            rectTransform = fillImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(startWidth, rectTransform.sizeDelta.y);
        }
    }

    private IEnumerator WidthTransitionCoroutine(float fromWidth, float toWidth, float transitionDuration)
    {
        float elapsedTime = 0f;
        float initialXPosition = rectTransform.anchoredPosition.x;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            float newWidth = Mathf.Lerp(fromWidth, toWidth, t);
            float widthDifference = fromWidth - newWidth;
            rectTransform.anchoredPosition = new Vector2(initialXPosition - (widthDifference / 2), rectTransform.anchoredPosition.y);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            yield return null;
        }

        rectTransform.sizeDelta = new Vector2(toWidth, rectTransform.sizeDelta.y);
        rectTransform.anchoredPosition = new Vector2(initialXPosition - ((fromWidth - toWidth) / 2), rectTransform.anchoredPosition.y);
        gameObject.SetActive(false);
    }

    public void StartTimerSliderTransition(float transitionDuration)
    {
        StartCoroutine(WidthTransitionCoroutine(startWidth, targetWidth, transitionDuration));
    }
}
