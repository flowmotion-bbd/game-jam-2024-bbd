using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphState : MonoBehaviour
{
    [SerializeField] private NodeTypeEnum endNodeType;

    private Graph graph = new Graph();
    [SerializeField] private List<DataPath> dataPaths = new List<DataPath>();

    List<DataPath> origDataPaths = new List<DataPath>();

    public Graph Graph
    {
        get { return graph; }
    }

    public List<DataPath> DataPaths
    {
        get { return dataPaths; }
    }

    public void Start()
    {
        InitializeSate();
    }

    void InitializeSate()
    {
        foreach (NodeState nodeState in FindObjectsByType<NodeState>(FindObjectsSortMode.None))
        {
            graph.AddNode(nodeState);

            if (nodeState.Node.NodeType == endNodeType)
            {
                graph.AddEndNode(nodeState);
            }
        }

        foreach (EdgeState edgeState in FindObjectsByType<EdgeState>(FindObjectsSortMode.None))
        {
            graph.AddEdge(edgeState);
        }

        foreach(DataPath dataPath in dataPaths)
        {
            origDataPaths.Add(new DataPath(dataPath));
        }
    }

    public void AddNodeToDataPath(int dataPathIndex, NodeState nodeState)
    {
        if (dataPaths[dataPathIndex].Enabled)
        {
            Debug.Log("ADDING NODE");
            dataPaths[dataPathIndex].AddNodeToPath(nodeState);
        }
    }

    public void RemoveNodeFromDataPath(int dataPathIndex, NodeState nodeState)
    {
        if (dataPaths[dataPathIndex].Enabled)
        {
            dataPaths[dataPathIndex].RemoveNodeFromPath(nodeState);
        }
    }

    public EdgeState RetrieveEdge(NodeState node1, NodeState node2)
    {
        foreach (EdgeState edgeState in graph.Edges)
        {
            if (
                (edgeState.Edge.NodeStateA == node1 && edgeState.Edge.NodeStateB == node2) ||
                (edgeState.Edge.NodeStateA == node2 && edgeState.Edge.NodeStateB == node1)
            )
            {
                return edgeState;
            }
        }

        return null;
    }

    public IEnumerable<EdgeState> RetrieveEdgesWithNode(NodeState node)
    {
        return graph.Edges.Where(edgeState => edgeState.Edge.ContainsNode(node));
    }

    public void ResetState()
    {
        dataPaths = new List<DataPath>();
        foreach (DataPath dataPath in origDataPaths)
        {
            dataPaths.Add(new DataPath(dataPath));
        }

        foreach (EdgeState edgeState in graph.Edges)
        {
            edgeState.ResetState();
        }

        foreach (NodeState nodeState in graph.Nodes)
        {
            nodeState.ResetState();
        }
    }

    public void RemoveEdge(EdgeState edge)
    {
        graph.Edges.Remove(edge);
    }
}
