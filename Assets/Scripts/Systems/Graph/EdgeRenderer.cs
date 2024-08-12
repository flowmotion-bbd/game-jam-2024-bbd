using UnityEditor;
using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{
    public Transform startNode;
    public Transform endNode;

    [SerializeField] private LineRenderer lineRenderer;

    public void UpdateLine()
    {
        if (lineRenderer != null)
        {
            if (startNode != null && endNode != null)
            {
                Undo.RecordObject(lineRenderer, "Update Line Renderer");
                lineRenderer.SetPosition(0, startNode.position);
                lineRenderer.SetPosition(1, endNode.position);
            }
        }
    }
}
