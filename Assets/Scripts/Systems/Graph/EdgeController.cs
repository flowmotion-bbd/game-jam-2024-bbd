using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EdgeController : MonoBehaviour
{

    private EdgeState edgeState;
    private GraphController graphController;

    void Awake()
    {
        graphController = FindAnyObjectByType<GraphController>();
        edgeState = GetComponent<EdgeState>();
    }

    void OnMouseDown()
    {
        graphController.RemoveEdgeFromDataPath(edgeState);
    }
}
