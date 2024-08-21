using UnityEngine;

[System.Serializable]
public class Edge
{
    [SerializeField] private NodeState nodeStateA;
    [SerializeField] private NodeState nodeStateB;
    [SerializeField] private bool visible = false;

    public NodeState NodeStateA
    {
        get { return nodeStateA; }
        set{ nodeStateA = value; }
    }

    public NodeState NodeStateB
    {
        get { return nodeStateB; }
        set { nodeStateB = value; }
    }

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    public bool ContainsNode(NodeState nodeState)
    {
        return nodeStateA == nodeState || nodeStateB == nodeState;
    }
}