using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DataPath
{
    [SerializeField] private Color colour;
    [SerializeField] private List<NodeState> path = new List<NodeState>();

    public Color Colour
    {
        get { return colour; }
    }

    public List<NodeState> Path
    {
        get { return path; }
    }

    public void AddNodeToPath(NodeState nodeState)
    {
        if (!path.Contains(nodeState))
        {
            path.Add(nodeState);
        }
    }

    public void RemoveNodeFromPath(NodeState nodeState)
    {
        int nodeIndex = path.IndexOf(nodeState);
        if (nodeIndex != -1)
        {
            path.RemoveRange(nodeIndex, path.Count - nodeIndex);
        }
    }

    public DataPath(DataPath dataPath)
    {
        this.colour = dataPath.colour;
        
        foreach (NodeState nodeState in dataPath.path)
        {
            this.path.Add(nodeState);
        }
    }

    public bool IsValid()
    {
        if (path.First().Node.NodeType == NodeTypeEnum.EncryptedData)
        {
            foreach (NodeState nodeState in path)
            {
                if (nodeState.Node.NodeType == NodeTypeEnum.DecryptionHub)
                {
                    return true;
                }
            }

            return false;
        }

        return true;
    }
}
