using UnityEngine;
using System.Collections;

public abstract class NodeController : MonoBehaviour
{
    public NodeState nodeState;
    public LevelManager levelManager;
    public NodeRenderer nodeRenderer;

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        nodeState = GetComponent<NodeState>();
        nodeRenderer = GetComponent<NodeRenderer>();
    }

    public abstract void OnMouseDown();

    IEnumerator StartCompromise(DataPath dataPath)
    {
        dataPath.Enabled = false;

        while (nodeState.Node.CompromisationTime > 0f)
        {
            if (!levelManager.IsTiming)
            {
                yield break;
            }

            nodeState.Node.CompromisationTime  -= Time.deltaTime;
            nodeRenderer.SetCompromiseRadial(nodeState.Node.CompromisationTime / nodeState.Node.InitCompromisationTime);

            yield return null;
        }

        nodeState.Node.Compromised = true;
        dataPath.Enabled = true;
    }
    
    public void CompromiseNode(DataPath dataPath)
    {
        if (!nodeState.Node.Compromised)
        {
            StartCoroutine(StartCompromise(dataPath));
        }
    }
}
