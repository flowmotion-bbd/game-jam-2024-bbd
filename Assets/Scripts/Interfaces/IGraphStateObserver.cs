using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGraphStateObserver : IObserver
{
    public void StateChanged(List<Transform> nodes, List<Transform> edges);
}
