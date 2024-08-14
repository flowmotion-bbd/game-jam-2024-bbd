using UnityEngine;

public class NodeRenderer : MonoBehaviour
{
    public void ToggleVisibility(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
