using System.Collections.Generic;
using UnityEngine;

public class EdgeState : MonoBehaviour
{
    [SerializeField] private Edge edge;

    public Edge Edge
    {
        get { return edge; }
        set { edge = value; }
    }

    public void ResetState()
    {
        edge.Visible = false;
    }
}
