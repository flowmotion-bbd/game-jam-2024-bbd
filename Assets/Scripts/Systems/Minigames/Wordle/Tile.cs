using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private Image fill;
    private Outline outline;
    private TextMeshProUGUI text;
    private char letter;
    private TileStyle style;

    private void Awake()
    {
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public char Letter
    {
        get => letter;
        set
        {
            letter = value;
            text.text = value.ToString();
        }
    }

    public TileStyle Style
    {
        get => style;
        set
        {
            style = value;
            fill.color = value.FillColor;
            outline.effectColor = value.OutlineColor;
        }
    }

    public void ClearLetter()
    {
        this.Letter = '\0';
    }
}