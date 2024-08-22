using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PatternMatchTile : MonoBehaviour, IPointerClickHandler
{
    public event Action<PatternMatchTile> OnTileClicked;

    private Image fill;
    private Outline outline;
    private PatternMatchTileStyle style;
    private PatternMatchTileStyle inactiveStyle;

    private void Awake()
    {
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    public PatternMatchTileStyle Style
    {
        get => style;
        set
        {
            style = value;
            fill.color = value.FillColor;
            outline.effectColor = value.OutlineColor;
        }
    }

    public PatternMatchTileStyle InactiveStyle { get => inactiveStyle; set => inactiveStyle = value; }

    public void Hide()
    {
        if (!this.enabled) return;

        fill.color = inactiveStyle.FillColor;
        outline.effectColor = inactiveStyle.OutlineColor;
    }

    public void Show()
    {
        if (!this.enabled) return;

        fill.color = style.FillColor;
        outline.effectColor = style.OutlineColor;
    }

    private void OnDisable()
    {
        fill.color = inactiveStyle.FillColor;
        outline.effectColor = inactiveStyle.FillColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null)
            OnTileClicked(this);
    }
}