using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GraphRenderer : MonoBehaviour
{
    [SerializeField] private Color defaultEdgeColor;
    private GraphState graphState;

    void Start()
    {
        graphState = GetComponent<GraphState>();
    }

    void Update()
    {
        UpdateGraph();
    }

    private void UpdateGraph()
    {
        foreach (EdgeState edgeState in graphState.Graph.Edges)
        {
            EdgeRenderer edgeRenderer = edgeState.GetComponent<EdgeRenderer>();
            edgeRenderer.ToggleVisibility(edgeState.Edge.Visible);
            edgeRenderer.UpdateEdgeColour(defaultEdgeColor);
        }

        foreach (NodeState nodeState in graphState.Graph.Nodes)
        {
            NodeRenderer nodeRenderer = nodeState.GetComponent<NodeRenderer>();
            nodeRenderer.ToggleVisibility(nodeState.Node.Visible);
        }

        foreach (DataPath dataPath in graphState.DataPaths)
        {
            for (int i = 0; i < dataPath.Path.Count - 1; i++)
            {
                EdgeState edgeState = graphState.RetrieveEdge(dataPath.Path[i], dataPath.Path[i + 1]);

                if (edgeState != null)
                {
                    EdgeRenderer edgeRenderer = edgeState.GetComponent<EdgeRenderer>();

                    Color colour = dataPath.Colour;
                    colour.a = 0.6f;

                    edgeRenderer.UpdateEdgeColour(colour);
                }
            }
        }

        DataPath currentDataPath = graphState.DataPaths[GetComponent<GraphController>().CurrentDataPathIndex];
        for (int i = 0; i < currentDataPath.Path.Count - 1; i++)
        {
            EdgeState edgeState = graphState.RetrieveEdge(currentDataPath.Path[i], currentDataPath.Path[i + 1]);

            if (edgeState != null)
            {
                EdgeRenderer edgeRenderer = edgeState.GetComponent<EdgeRenderer>();
                edgeRenderer.UpdateEdgeColour(currentDataPath.Colour);
            }
        }
    }
}
