using UnityEngine;

[System.Serializable]
public class Node
{
    [SerializeField] private NodeTypeEnum nodeType = NodeTypeEnum.Basic;
    [SerializeField] private bool visible = false;
    [SerializeField] private bool compromised = false;
    [SerializeField] private float compromisationTime = 5f;

    public NodeTypeEnum NodeType
    {
        get { return nodeType; }
    }

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    public bool Compromised
    {
        get { return compromised; }
        set { compromised = value; }
    }

    public float CompromisationTime
    {
        get { return compromisationTime; }
    }
}
