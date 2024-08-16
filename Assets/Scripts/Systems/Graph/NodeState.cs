using System.Collections.Generic;
using UnityEngine;

public class NodeState : MonoBehaviour
{
    [SerializeField] private Node node;

    public Node Node {  
        get { return node; }
        set { node = value; }
    }

    public void ResetState()
    {
        node.Visible = false;
        node.Compromised = false;
    }
}
