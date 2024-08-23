using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeRenderer : MonoBehaviour
{
    [SerializeField] TMP_Text selectKeyText = null;
    [SerializeField] private SpriteRenderer selectedSpriteRenderer;
    [SerializeField] private Image compromiseRadialImage;

    public void ToggleVisibility(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    public void ToggleSelected(bool toggle, Color colour)
    {
        selectedSpriteRenderer.gameObject.SetActive(toggle);
        selectedSpriteRenderer.material.SetColor("_Color", colour);
    }

    public void SetCompromiseRadial(float radialFillAmount)
    {
        if (radialFillAmount < 0f)
        {
            radialFillAmount = 0f;
        }

        compromiseRadialImage.fillAmount = radialFillAmount;
    }

    public void SetSelectKeyText(int index)
    {
        selectKeyText.gameObject.SetActive(true);
        selectKeyText.text = index.ToString();
    }
}
