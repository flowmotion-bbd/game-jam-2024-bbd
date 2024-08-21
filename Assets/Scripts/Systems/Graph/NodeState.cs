using System.Collections.Generic;
using UnityEngine;

public class NodeState : MonoBehaviour
{
    [SerializeField] private Node node;

    public Node Node {  
        get { return node; }
        set { node = value; }
    }

    void Start()
    {
        node.CompromisationTime = node.InitCompromisationTime;
    }

    public void ResetState()
    {
        node.Visible = false;
        node.CompromisationTime = node.InitCompromisationTime;
    }
}
