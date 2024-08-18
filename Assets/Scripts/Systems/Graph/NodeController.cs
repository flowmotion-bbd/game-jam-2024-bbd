using UnityEngine;

public abstract class NodeController : MonoBehaviour
{
    public NodeState nodeState;
    public LevelManager levelManager;

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        nodeState = GetComponent<NodeState>();
    }

    public abstract void OnMouseDown();
}
