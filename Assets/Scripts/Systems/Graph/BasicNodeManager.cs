using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BasicNodeController : NodeController
{
    private NodeState nodeState;
    private GraphController graphController;

    void Awake()
    {
        graphController = FindAnyObjectByType<GraphController>();
        nodeState = GetComponent<NodeState>();
    }

    // This method is called when the sprite is clicked
    void OnMouseDown()
    {
        graphController.AddNodeToDataPath(nodeState);
    }

}
