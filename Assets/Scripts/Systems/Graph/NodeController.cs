using UnityEngine;

public abstract class NodeController : MonoBehaviour
{
    protected NodeState nodeState;
    protected LevelManager levelManager;

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        nodeState = GetComponent<NodeState>();
    }

    public abstract void OnMouseDown();
}
