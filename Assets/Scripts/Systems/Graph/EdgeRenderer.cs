using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private BoxCollider2D boxCollider;

    public void UpdateLine()
    {
        EdgeState edgeState = GetComponent<EdgeState>();
        Edge edge = edgeState.Edge;

        if (lineRenderer != null && edge != null)
        {
            if (edge.NodeStateA != null && edge.NodeStateB != null)
            {
                lineRenderer.SetPosition(0, edge.NodeStateA.transform.position);
                lineRenderer.SetPosition(1, edge.NodeStateB.transform.position);
                UpdateCollider();
            }
        }
    }
    void UpdateCollider()
    {
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        Vector3 center = (startPoint + endPoint) / 2f;

        boxCollider.offset = transform.InverseTransformPoint(center);
        boxCollider.size = new Vector3(Vector3.Distance(startPoint, endPoint), lineRenderer.startWidth, lineRenderer.startWidth);

        boxCollider.transform.rotation = Quaternion.FromToRotation(Vector3.right, endPoint - startPoint);
    }

    public void ToggleVisibility(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    public void UpdateEdgeColour(Color colour)
    {
        lineRenderer.startColor = colour;
        lineRenderer.endColor = colour;
    }
}
