using System.Collections.Generic;
using UnityEngine;

public class NodeState : MonoBehaviour, IObservable<NodeState>
{
    [SerializeField] private Node node;

    private List<IObserver<NodeState>> observers = new List<IObserver<NodeState>>();

    public Node Node {  
        get { return node; }
        set { 
            node = value; 
            NotifySubscribers(); 
        }
    }

    public void NotifySubscribers()
    {
        foreach (IObserver<NodeState> observer in observers)
        {
            observer.StateChanged(this);
        }
    }

    public void Subscribe(IObserver<NodeState> observer)
    {
        observers.Add(observer);
    }

    void Start()
    {
        NotifySubscribers();
    }
}
