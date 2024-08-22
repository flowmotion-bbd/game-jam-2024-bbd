using System.Collections.Generic;

[System.Serializable]
public class Graph
{
    private List<NodeState> nodes = new List<NodeState>();
    private List<NodeState> startNodes = new List<NodeState>();
    private List<NodeState> endNodes = new List<NodeState>();
    private List<EdgeState> edges = new List<EdgeState>();

    public List<NodeState> Nodes
    {
        get { return nodes; }
    }
    public List<NodeState> StartNodes
    {
        get { return startNodes; }
    }

    public List<NodeState> EndNodes
    {
        get { return endNodes; }
    }

    public List<EdgeState> Edges
    {
        get { return edges; }
    }

    public void AddNode(NodeState nodeState)
    {
        nodes.Add(nodeState);
    }
    public void AddStartNode(NodeState nodeState)
    {
        startNodes.Add(nodeState);
    }

    public void AddEndNode(NodeState nodeState)
    {
        endNodes.Add(nodeState);
    }

    public void AddEdge(EdgeState edgeState)
    {
        edges.Add(edgeState);
    }
}
