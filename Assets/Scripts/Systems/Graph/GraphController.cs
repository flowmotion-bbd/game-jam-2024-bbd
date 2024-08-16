using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    private GraphState graphState;
    private int currentDataPathIndex = 0;

    public int CurrentDataPathIndex
    {
        get { return currentDataPathIndex; }
        set
        {
            if (currentDataPathIndex >= 0 && currentDataPathIndex < graphState.DataPaths.Count)
            {
                currentDataPathIndex = value;
            }
        }
    }

    void Start()
    {
        graphState = GetComponent<GraphState>();
    }

    public void AddNodeToDataPath(NodeState nodeState)
    {
        if (graphState.RetrieveEdge(graphState.DataPaths[currentDataPathIndex].Path.Last(), nodeState)  != null)
        {
            graphState.AddNodeToDataPath(currentDataPathIndex, nodeState);
        }
    }

    public void RemoveEdgeFromDataPath(EdgeState edgeState)
    {
        NodeState nodeStateToRemove = edgeState.Edge.NodeStateA;
        int nodeIndex = graphState.DataPaths[currentDataPathIndex].Path.IndexOf(nodeStateToRemove);

        if (nodeIndex != -1 && graphState.DataPaths[currentDataPathIndex].Path.Count > 1)
        {
            if (nodeIndex == 0)
            {
                nodeStateToRemove = edgeState.Edge.NodeStateB;
            } else if (graphState.DataPaths[currentDataPathIndex].Path[nodeIndex - 1] != edgeState.Edge.NodeStateB)
            {
                nodeStateToRemove = edgeState.Edge.NodeStateB;
            }
            
            graphState.RemoveNodeFromDataPath(currentDataPathIndex, nodeStateToRemove);
        }
    }

    void Update()
    {
        UpdateNodes();
        UpdateEdges();
    }

    void UpdateNodes()
    {
        IEnumerable<EdgeState> visibleEdges = graphState.Graph.Edges.Where(edge => edge.Edge.Visible);
        foreach (EdgeState visibleEdge in visibleEdges)
        {
            visibleEdge.Edge.NodeStateA.Node.Visible = true;
            visibleEdge.Edge.NodeStateB.Node.Visible = true;
        }

        foreach (DataPath dataPath in graphState.DataPaths)
        {
            foreach (NodeState nodeState in dataPath.Path)
            {
                nodeState.Node.Compromised = true;
            }
        }
    }

    void UpdateEdges()
    {
        foreach (DataPath dataPath in graphState.DataPaths)
        {
            foreach (NodeState nodeState in dataPath.Path)
            {
                IEnumerable<EdgeState> connectedEdges = graphState.RetrieveEdgeWithNode(nodeState);

                foreach (EdgeState connectedEdge in connectedEdges)
                {
                    connectedEdge.Edge.Visible = true;
                }
            }
        }
    }

    public void ResetState()
    {
        currentDataPathIndex = 0;
        graphState.ResetState();
    }
}
