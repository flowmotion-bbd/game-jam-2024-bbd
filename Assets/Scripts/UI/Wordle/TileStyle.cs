using System;
using UnityEngine;

[System.Serializable]
public class TileStyle
{
    [SerializeField]
    private Color fillColor;
    [SerializeField]
    private Color outlineColor;
    
    public TileStyle(Color fillColor, Color outlineColor)
    {
        this.fillColor = fillColor;
        this.outlineColor = outlineColor;
    }

    public Color FillColor { get => fillColor; set => fillColor = value; }
    public Color OutlineColor { get => outlineColor; set => outlineColor = value; }
}