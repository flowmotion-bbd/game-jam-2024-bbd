using System.Collections.Generic;
using UnityEngine;

public class EdgeState : MonoBehaviour
{
    [SerializeField] private Edge edge;

    private List<IObserver<EdgeState>> observers = new List<IObserver<EdgeState>>();

    public Edge Edge
    {
        get { return edge; }
        set { edge = value; }
    }
}
