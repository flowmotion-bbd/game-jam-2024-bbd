using UnityEngine;
using System.Collections.Generic;

public class GraphRenderer : MonoBehaviour, IGraphStateObserver
{
    public void StateChanged(List<Transform> nodes, List<Transform> edges)
    {
        throw new System.NotImplementedException();
    }
}
