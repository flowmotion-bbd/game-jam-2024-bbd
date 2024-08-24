using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSheetAnimator : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float frameRate = 24f; 

    private int currentFrame;
    private float timer;

    private void Start()
    {
        if (frameRate <= 0)
        {
            frameRate = 1f;
        }
        image = gameObject.GetComponent<Image>();
        if (sprites.Length > 0)
        {
            image.sprite = sprites[0];
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1/frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % sprites.Length;
            image.sprite = sprites[currentFrame];
        }
    }
}
