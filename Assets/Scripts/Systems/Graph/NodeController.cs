using UnityEngine;
using System.Collections;

public abstract class NodeController : MonoBehaviour
{
    public NodeState nodeState;
    public LevelManager levelManager;
    public NodeRenderer nodeRenderer;

    private DataPath currentDataPath;

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        nodeState = GetComponent<NodeState>();
        nodeRenderer = GetComponent<NodeRenderer>();
    }

    public abstract void OnMouseDown();

    IEnumerator StartCompromise()
    {
        currentDataPath.Enabled = false;

        while (nodeState.Node.CompromisationTime > 0f)
        {
            Debug.Log("COMPROMISING NODE");
            nodeState.Node.CompromisationTime  -= Time.deltaTime;
            nodeRenderer.SetCompromiseRadial(nodeState.Node.CompromisationTime / nodeState.Node.InitCompromisationTime);

            yield return null;
        }

        nodeState.Node.Compromised = true;
        currentDataPath.Enabled = true;
    }
    
    public void CompromiseNode(DataPath dataPath)
    {
        currentDataPath = dataPath;
        StartCoroutine("StartCompromise");
    }
}
