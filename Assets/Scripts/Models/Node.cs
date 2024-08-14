using UnityEngine;

[System.Serializable]
public class Node
{
    [SerializeField] private NodeTypeEnum nodeType = NodeTypeEnum.Basic;
    [SerializeField] private bool visible = false;

    public NodeTypeEnum NodeType
    {
        get { return nodeType; }
    }

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }
}
