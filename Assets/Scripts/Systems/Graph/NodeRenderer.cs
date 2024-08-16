using UnityEngine;

public class NodeRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selectedSpriteRenderer;

    public void ToggleVisibility(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    public void ToggleSelected(bool toggle, Color colour)
    {
        selectedSpriteRenderer.gameObject.SetActive(toggle);
        selectedSpriteRenderer.material.SetColor("_Color", colour);
    }
}
