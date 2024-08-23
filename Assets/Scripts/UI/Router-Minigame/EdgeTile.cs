using UnityEngine;
using UnityEngine.UI;
using System;

public class EdgeTile : MonoBehaviour
{
    private Image fill;
    private Outline outline;
    private TileStyle style;

    private void Awake()
    {
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
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
}