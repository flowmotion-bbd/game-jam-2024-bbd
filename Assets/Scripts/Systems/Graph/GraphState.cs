using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphState : MonoBehaviour
{
    [SerializeField] private Transform nodesParent;
    [SerializeField] private Transform edgeParent;

    private List<IGraphStateObserver> observers = new List<IGraphStateObserver>();

    private List<Transform> nodes = new List<Transform>();
    private List<Transform> edges = new List<Transform>();

    // Start is called before the first frame update
    public void Start()
    {
        foreach (Transform node in nodesParent)
        {
            nodes.Add(node);
        }

        foreach (Transform edge in edgeParent)
        {
            edges.Add(edge);
        }
    }

    public void AddObserver(IGraphStateObserver graphStateObserver)
    {
        observers.Add(graphStateObserver);
    }
}
