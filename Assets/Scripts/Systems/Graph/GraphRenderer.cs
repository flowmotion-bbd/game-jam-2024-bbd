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

    public void UpdateGraph(int currentDataPathIndex)
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
            nodeRenderer.ToggleSelected(false, Color.clear);
        }

        for (int i = 0; i < graphState.DataPaths.Count; i++)
        {
            var dataPath = graphState.DataPaths[i];

            //dataPath.Path.First().GetComponent<NodeRenderer>().ToggleSelected(true, dataPath.Colour);
            dataPath.Path.First().GetComponent<NodeRenderer>().SetSelectKeyText(i+1);
            for (int j = 0; j < dataPath.Path.Count - 1; j++)
            {
                EdgeState edgeState = graphState.RetrieveEdge(dataPath.Path[j], dataPath.Path[j + 1]);

                if (edgeState != null)
                {
                    EdgeRenderer edgeRenderer = edgeState.GetComponent<EdgeRenderer>();

                    Color colour = dataPath.Colour;
                    colour.a = 0.6f;

                    edgeRenderer.UpdateEdgeColour(colour);
                }
            }
        }

        DataPath currentDataPath = graphState.DataPaths[currentDataPathIndex];

        var startDataNodeRenderer = currentDataPath.Path.First().GetComponent<NodeRenderer>();
        startDataNodeRenderer.ToggleSelected(true, currentDataPath.Colour);

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
